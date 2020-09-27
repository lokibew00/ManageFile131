Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    Private Sub GenOutput()
        Dim ws As New wsFlatten.WS_FLATTEN

    End Sub
    Private Function getData()

        Return ""
    End Function
    Private Sub CopyAll(ByVal diSource As DirectoryInfo, ByVal diTarget As DirectoryInfo)

        Dim dao As New TB_MAIN_DATA_DRIVE
        'dao.GETDATA_PVNCD()
        'INPUT
        'For Each row In dao.Details
        '    For Each fi In diSource.GetFiles(row.TRANSACTION_NO)
        '        If fi.Name.Contains(row.TRANSACTION_NO) Then
        '            diTarget.CreateSubdirectory(row.TRANSACTION_NO)
        '            fi.CopyTo(Path.Combine(diTarget.FullName, row.TRANSACTION_NO, fi.Name), True)
        '        End If
        '    Next
        'Next
        'OUTPUTT
        dao.GETDATA_PVNCD_OUTPUT()
        For Each row In dao.Details
            Try
                For Each fi In diSource.GetFiles(row.FDPDTNO & ".pdf")
                    If fi.Name.Contains(row.FDPDTNO) Then
                        diTarget.CreateSubdirectory(row.FDPDTNO)
                        fi.CopyTo(Path.Combine(diTarget.FullName, row.FDPDTNO, fi.Name), True)
                    End If
                Next
            Catch ex As Exception

            End Try

        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim diSource As New DirectoryInfo("H:\path\FOOD\PDF_OUTPUT")
        Dim diTarget As New DirectoryInfo("H:\path\FOOD_MVC\PVNCD71\OUTPUT")
        CopyAll(diSource, diTarget)

    End Sub
End Class
Public Class connection
    Public db As New LINQ_FOODDataContext
    Public datas
End Class
Public Class TB_MAIN_DATA_DRIVE
    Inherits connection
    Public fields As New MAIN_DATA_DRIVE

    Private _Details As New List(Of MAIN_DATA_DRIVE)
    Public Property Details() As List(Of MAIN_DATA_DRIVE)
        Get
            Return _Details
        End Get
        Set(ByVal value As List(Of MAIN_DATA_DRIVE))
            _Details = value
        End Set
    End Property

    Private Sub AddDetails()
        Details.Add(fields)
        fields = New MAIN_DATA_DRIVE
    End Sub
    Public Sub GETDATA_PVNCD()
        datas = (From p In db.MAIN_DATA_DRIVEs Select p Where p.PVNCD = 71 And p.PROCESS_ID = 7)
        For Each Me.fields In datas
            AddDetails()
        Next
    End Sub
    Public Sub GETDATA_PVNCD_OUTPUT()
        datas = (From p In db.MAIN_DATA_DRIVEs Select p Where p.PVNCD = 71 And p.PROCESS_ID = 7 And p.STATUS_ID = 8)
        For Each Me.fields In datas
            AddDetails()
        Next
    End Sub
    Public Sub GETDATA_TRANSACTION(ByVal TR_ID As String)
        datas = (From p In db.MAIN_DATA_DRIVEs Select p Where p.TRANSACTION_NO = TR_ID)
        For Each Me.fields In datas
        Next
    End Sub
End Class
