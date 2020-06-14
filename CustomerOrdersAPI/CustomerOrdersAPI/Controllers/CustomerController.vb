Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json.Linq

Namespace Controllers
    <RoutePrefix("api/Customer")>
    Public Class CustomerController
        Inherits ApiController
        Dim custLibrary As New CustomerOrdersLib.CustOrdersLib

        <HttpPost>
        <Route("CheckCustomerExists")>
        Public Function CheckCustomerExists(<FromBody> ByVal data As CustInfoRequest) As IHttpActionResult
            Dim custInfo As New CustomerInfoResponse
            Try
                Dim results = custLibrary.GetCustomerInfoByUsername(data.username, data.password)

                If results Is Nothing Then
                    Return BadRequest("Customer Not found")
                Else
                    custInfo = results
                End If
            Catch ex As Exception
                Return BadRequest(ex.Message)
            End Try

            Return Json(custInfo)
        End Function


        <HttpPost>
        <Route("CreateCustomer")>
        Public Function CreateCustomer(<FromBody> ByVal createCustRequest As CreateCustomerRequest) As IHttpActionResult
            Dim custInfo As New CustomerInfoResponse
            Try
                If custLibrary.IsValidEmail(createCustRequest.email) = False Then
                    Return BadRequest("Invalid Email Address")
                End If

                Dim results = custLibrary.CreateCustomer(createCustRequest)

                If results Is Nothing Then
                    Return BadRequest("Unable to create Customer")
                Else
                    custInfo = results
                End If
            Catch ex As Exception
                Return BadRequest(ex.Message)
            End Try

            Return Json(custInfo)
        End Function

        <HttpPost>
        <Route("CreateCustomerOrder")>
        Public Function CreateCustomerOrder(<FromBody> ByVal createCustOrderRequest As CreateCustomerOrderRequest) As IHttpActionResult
            Dim custOrder As New CustomerOrderResponse
            Try
                If custLibrary.IsValidEmail(createCustOrderRequest.adminEmail) = False Then
                    Return BadRequest("Invalid Admin Email")
                End If

                If custLibrary.IsValidEmail(createCustOrderRequest.billEmail) = False Then
                    Return BadRequest("Invalid Billing Email")
                End If

                Dim results = custLibrary.CreateCustomerOrder(createCustOrderRequest)

                If results Is Nothing Then
                    Return BadRequest("Unable to create Customer Order")
                Else
                    custOrder = results
                End If
            Catch ex As Exception
                Return BadRequest(ex.Message)
            End Try

            Return Json(custOrder)
        End Function
        ' GET: api/Customer
        'Public Function GetValues() As IEnumerable(Of String)
        '    Return New String() {"value1", "value2"}
        'End Function

        '' GET: api/Customer/5
        'Public Function GetValue(ByVal id As Integer) As String
        '    Return "value"
        'End Function

        '' POST: api/Customer
        'Public Sub PostValue(<FromBody()> ByVal value As String)

        'End Sub

        '' PUT: api/Customer/5
        'Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        'End Sub

        '' DELETE: api/Customer/5
        'Public Sub DeleteValue(ByVal id As Integer)

        'End Sub
    End Class
End Namespace