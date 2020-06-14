Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace CustomerOrdersLib
    Public Class CustOrdersLib
        Dim DB_Conn As String = My.Settings.CustomerAPI_DB_Conn

        Public Function CheckCustomerExists(ByVal username As String, ByVal password As String) As String
            Dim customerExists As Boolean = False

            If String.IsNullOrEmpty(username) And String.IsNullOrEmpty(password) Then
            Else
                customerExists = True
            End If

            Return customerExists
        End Function

        Public Function GetCustomerInfoByUsername(ByVal username As String, ByVal password As String) As CustomerInfoResponse
            Dim CustInfoResponse As New CustomerInfoResponse
            Dim dataSet As New DataSet
            Dim sqlConnection As New SqlConnection With {
                .ConnectionString = DB_Conn
            }

            Dim queryStringBuilder As New StringBuilder()
            queryStringBuilder.Append("SELECT QFC.[Company name]")
            queryStringBuilder.Append(", QFC.[First name]")
            queryStringBuilder.Append(", QFC.[Last name]")
            queryStringBuilder.Append(", QFC.[Email]")
            queryStringBuilder.Append(", QFC.[Customer ID]")
            queryStringBuilder.Append(" FROM [BLYNKCOREAPP].[dbo].[QF_Customers] QFC INNER JOIN")
            queryStringBuilder.Append(" [BLYNKCOREAPP].[dbo].[SF_Users] SFU ON SFU.ParentId = QFC.ID")
            queryStringBuilder.Append(" WHERE SFU.Username ='{0}' and SFU.Password = '{1}'")


            Dim getCustInfoCmd As String = String.Format(queryStringBuilder.ToString(), username, password)
            'Dim logger As ILog = Nothing)
            Using cn As New SqlConnection(sqlConnection.ConnectionString)
                Try
                    Using cmd As New SqlCommand
                        With cmd
                            .Connection = cn
                            .Connection.Open()
                            .CommandText = getCustInfoCmd
                            .CommandType = CommandType.Text
                        End With

                        dataSet.Tables.Clear()
                        Dim da = New SqlDataAdapter(cmd)
                        da.Fill(dataSet, "QF_Customers")

                        Dim dataRow As DataRow

                        If dataSet.Tables(0).Rows.Count > 0 Then
                            For Each dataRow In dataSet.Tables(0).Rows
                                CustInfoResponse.companyName = dataRow(0)
                                CustInfoResponse.firstName = dataRow(1)
                                CustInfoResponse.lastName = dataRow(2)
                                CustInfoResponse.email = dataRow(3)
                                CustInfoResponse.customerId = dataRow(4)
                            Next
                        Else
                            CustInfoResponse = Nothing
                        End If

                    End Using
                Catch ex As Exception
                    CustInfoResponse = Nothing
                    'logger.Error("GetCustomerInfo , error occurred on " & ex.Message)
                End Try
            End Using
            Return CustInfoResponse
        End Function

        Public Function GetCustomerInfoById(ByVal Id As Guid) As CustomerInfoResponse
            Dim CustInfoResponse As New CustomerInfoResponse
            Dim dataSet As New DataSet
            Dim sqlConnection As New SqlConnection With {
                .ConnectionString = DB_Conn
            }

            If Not Id = Guid.Empty Then
                Dim queryStringBuilder As New StringBuilder()
                queryStringBuilder.Append("SELECT QFC.[Company name]")
                queryStringBuilder.Append(", QFC.[First name]")
                queryStringBuilder.Append(", QFC.[Last name]")
                queryStringBuilder.Append(", QFC.[Email]")
                queryStringBuilder.Append(", QFC.[Customer ID]")
                queryStringBuilder.Append(" FROM [BLYNKCOREAPP].[dbo].[QF_Customers] QFC")
                queryStringBuilder.Append(" WHERE QFC.ID ='{0}'")


                Dim getCustInfoCmd As String = String.Format(queryStringBuilder.ToString(), Id)
                'Dim logger As ILog = Nothing)
                Using cn As New SqlConnection(sqlConnection.ConnectionString)
                    Try
                        Using cmd As New SqlCommand
                            With cmd
                                .Connection = cn
                                .Connection.Open()
                                .CommandText = getCustInfoCmd
                                .CommandType = CommandType.Text
                            End With

                            dataSet.Tables.Clear()
                            Dim da = New SqlDataAdapter(cmd)
                            da.Fill(dataSet, "QF_Customers")

                            Dim dataRow As DataRow

                            If dataSet IsNot Nothing Then
                                For Each dataRow In dataSet.Tables(0).Rows
                                    CustInfoResponse.companyName = dataRow(0)
                                    CustInfoResponse.firstName = dataRow(1)
                                    CustInfoResponse.lastName = dataRow(2)
                                    CustInfoResponse.email = dataRow(3)
                                    CustInfoResponse.customerId = dataRow(4)
                                Next
                            Else
                                CustInfoResponse = Nothing
                            End If

                        End Using
                    Catch ex As Exception
                        'logger.Error("GetCustomerInfo , error occurred on " & ex.Message)
                    End Try
                End Using
            End If

            Return CustInfoResponse
        End Function

        Public Function CreateCustomer(ByVal CreateCustomerRequest As CreateCustomerRequest) As CustomerInfoResponse
            Dim CustInfoResponse As New CustomerInfoResponse
            Dim dataSet As New DataSet
            Dim sqlConnection As New SqlConnection With {
                .ConnectionString = DB_Conn
            }

            'Dim logger As ILog = Nothing)
            Using cn As New SqlConnection(sqlConnection.ConnectionString)
                Try
                    Using cmd As New SqlCommand("usp_Create_Customer", cn)
                        Dim newCustomerUniqueId As SqlParameter = New SqlParameter("@newCustomerUniqueId", SqlDbType.UniqueIdentifier)
                        newCustomerUniqueId.Direction = ParameterDirection.Output

                        With cmd
                            .Parameters.AddWithValue("@employeeCount", CreateCustomerRequest.employeeCount)
                            .Parameters.AddWithValue("@companyName", CreateCustomerRequest.companyName)
                            .Parameters.AddWithValue("@location", CreateCustomerRequest.location)
                            .Parameters.AddWithValue("@firstName", CreateCustomerRequest.firstName)
                            .Parameters.AddWithValue("@lastName", CreateCustomerRequest.lastName)
                            .Parameters.AddWithValue("@email", CreateCustomerRequest.email)
                            .Parameters.AddWithValue("@address", CreateCustomerRequest.address)
                            .Parameters.AddWithValue("@contactNo", CreateCustomerRequest.contactNumber)
                            .Parameters.AddWithValue("@customerId", CreateCustomerRequest.customerId)
                            .Parameters.AddWithValue("@companyLogo", CreateCustomerRequest.companyLogo)
                            .Parameters.Add(newCustomerUniqueId)
                            .CommandType = CommandType.StoredProcedure
                            .Connection.Open()
                        End With

                        cmd.ExecuteNonQuery()

                        If newCustomerUniqueId.Value IsNot DBNull.Value Then
                            CustInfoResponse = GetCustomerInfoById(newCustomerUniqueId.Value)
                        Else
                            CustInfoResponse = Nothing
                        End If

                    End Using
                Catch ex As Exception
                    'logger.Error("GetCustomerInfo , error occurred on " & ex.Message)
                    CustInfoResponse = Nothing
                End Try
            End Using
            Return CustInfoResponse
        End Function

        Public Function CreateCustomerOrder(ByVal CreateCustOrderRequest As CreateCustomerOrderRequest) As CustomerOrderResponse
            Dim CustOrderResponse As New CustomerOrderResponse
            Dim ID As Guid = Guid.Empty
            Dim dataSet As New DataSet
            Dim sqlConnection As New SqlConnection With {
                .ConnectionString = DB_Conn
            }

            'Dim logger As ILog = Nothing)
            Using cn As New SqlConnection(sqlConnection.ConnectionString)
                Try
                    Using cmd As New SqlCommand("usp_Create_CustomerOrder", cn)
                        Dim newOrderUniqueId As SqlParameter = New SqlParameter("@newOrderUniqueId", SqlDbType.UniqueIdentifier)
                        newOrderUniqueId.Direction = ParameterDirection.Output

                        With cmd
                            .Parameters.AddWithValue("@parentId", Guid.Parse(CreateCustOrderRequest.parentId))
                            .Parameters.AddWithValue("@orderId", CreateCustOrderRequest.orderId)
                            .Parameters.AddWithValue("@dateOrder", CreateCustOrderRequest.dateOfOrder)
                            .Parameters.AddWithValue("@byoDomain", CreateCustOrderRequest.byoDomain)
                            .Parameters.AddWithValue("@domainName", CreateCustOrderRequest.domainName)
                            .Parameters.AddWithValue("@gSuitPackage", CreateCustOrderRequest.gSuitePackage)
                            .Parameters.AddWithValue("@noOfUsers", CreateCustOrderRequest.numOfUsers)
                            .Parameters.AddWithValue("@adminEmail", CreateCustOrderRequest.adminEmail)
                            .Parameters.AddWithValue("@adminFirstname", CreateCustOrderRequest.adminFirstName)
                            .Parameters.AddWithValue("@adminLastName", CreateCustOrderRequest.adminLastName)
                            .Parameters.AddWithValue("@billCompanyName", CreateCustOrderRequest.billCompanyName)
                            .Parameters.AddWithValue("@billCompanyRegNo", CreateCustOrderRequest.billCompanyRegNo)
                            .Parameters.AddWithValue("@billCompanyAddress", CreateCustOrderRequest.billCompanyAddress)
                            .Parameters.AddWithValue("@billContactPerson", CreateCustOrderRequest.billContactPerson)
                            .Parameters.AddWithValue("@billEmail", CreateCustOrderRequest.billEmail)
                            .Parameters.AddWithValue("@status", CreateCustOrderRequest.status)
                            .Parameters.AddWithValue("@paymentStatus", CreateCustOrderRequest.paymentStatus)
                            .Parameters.Add(newOrderUniqueId)
                            .CommandType = CommandType.StoredProcedure
                            .Connection.Open()
                        End With

                        cmd.ExecuteNonQuery()

                        If newOrderUniqueId.Value IsNot DBNull.Value Then
                            CustOrderResponse.orderUniqueId = newOrderUniqueId.Value
                        Else
                            CustOrderResponse = Nothing
                        End If

                    End Using
                Catch ex As Exception
                    'logger.Error("GetCustomerInfo , error occurred on " & ex.Message)
                    CustOrderResponse = Nothing
                End Try
            End Using
            Return CustOrderResponse
        End Function

        Public Function IsValidEmail(email As String) As Boolean

            If String.IsNullOrWhiteSpace(email) Then Return False

            ' Use IdnMapping class to convert Unicode domain names.
            Try
                'Examines the domain part of the email and normalizes it.
                Dim DomainMapper =
                    Function(match As Match) As String

                        'Use IdnMapping class to convert Unicode domain names.
                        Dim idn = New IdnMapping

                        'Pull out and process domain name (throws ArgumentException on invalid)
                        Dim domainName As String = idn.GetAscii(match.Groups(2).Value)

                        Return match.Groups(1).Value & domainName

                    End Function

                'Normalize the domain
                email = Regex.Replace(email, "(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200))

            Catch e As RegexMatchTimeoutException
                Return False

            Catch e As ArgumentException
                Return False

            End Try

            Try
                Return Regex.IsMatch(email,
                                     "^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                     "(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                     RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))

            Catch e As RegexMatchTimeoutException
                Return False

            End Try

        End Function
    End Class
End Namespace


