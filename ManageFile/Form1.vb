Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    Inherits connection_db
    Private Sub GenOutput()
        Dim ws As New wsFlatten.WS_FLATTEN

    End Sub
    Private Function getData()

        Return ""
    End Function
    Private Sub CopyAll(ByVal diSource As DirectoryInfo, ByVal diTarget As DirectoryInfo)
        For Each fi In diSource.GetFiles
            If fi.Name.Contains("FOOD-7") Or fi.Name.Contains("FA-7") Then
                Console.WriteLine("Copying {0}\{1}", diTarget.FullName, fi.Name)
                fi.CopyTo(Path.Combine(diTarget.FullName, fi.Name), True)
            End If

        Next
        For Each diSourceSubDir In diSource.GetDirectories()
            Dim nextTargetSubDir = diTarget.CreateSubdirectory(diSourceSubDir.Name)
            CopyAll(diSourceSubDir, nextTargetSubDir)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim diSource As New DirectoryInfo("C:\path\FOOD_MVC\PDF_UPLOAD")
        Dim diTarget As New DirectoryInfo("C:\path\FOOD_MVC\TEST")
        'CopyAll(diSource, diTarget)
        Dim dt As New DataTable
        dt = QuerydsFoods("select * from main_data_drive")
    End Sub
End Class

Public Class connection_db
    Public Function QuerydsFoods(ByVal Commands As String) As DataTable

        Dim dt As New DataTable
        Try
            Dim MyConnection As SqlConnection = New SqlConnection("Data Source=10.111.28.130;Initial Catalog=FDA_FOOD_ANGULAR;User ID=fusion;Password=P@ssw0rd")
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
        Catch ex As Exception

        End Try

        Return dt
    End Function
End Class
