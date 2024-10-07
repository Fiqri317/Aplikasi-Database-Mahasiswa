Imports System
Imports System.Data
Imports System.Data.OleDb
Public Class Form1
    Dim _koneksiString As String
    Dim _koneksi As New OleDbConnection
    Dim komandambil As New OleDbCommand
    Dim datatabelku As New DataTable
    Dim dataadapterku As New OleDbDataAdapter
    Dim x As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _koneksiString = "Provider=Microsoft.Jet.OleDb.4.0;" + "Data Source=D:\Campus\Semester V\Kecerdasan Komputasi\Aplikasi Database Mahasiswa\database\Mahasiswa.mdb;"
        _koneksi.ConnectionString = _koneksiString
        _koneksi.Open()

        komandambil.Connection = _koneksi
        komandambil.CommandType = CommandType.Text

        komandambil.CommandText = "SELECT * FROM TMhs"
        dataadapterku.SelectCommand = komandambil
        dataadapterku.Fill(datatabelku)
        Bs_Coba.DataSource = datatabelku
        dgv_coba.DataSource = Bs_Coba
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim cmdTambah As New OleDbCommand
        Dim tanya As String
        Dim x As DataRow
        cmdTambah.Connection = _koneksi
        cmdTambah.CommandText = "INSERT INTO " + "TMhs (NIM, Nama, Jurusan)" + "VALUES ('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + " ')"
        tanya = MessageBox.Show("Data Ini di Simpan ?", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            cmdTambah.ExecuteNonQuery()
            x = datatabelku.NewRow
            x("NIM") = TextBox1.Text
            x("Nama") = TextBox2.Text
            x("Jurusan") = TextBox3.Text
            datatabelku.Rows.Add(x)
            Bs_Coba.DataSource = Nothing
            Bs_Coba.DataSource = datatabelku

            dgv_coba.Refresh()
            Bs_Coba.MoveFirst()
        End If
    End Sub

    Private Sub dgv_coba_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_coba.CellContentClick
        TextBox1.Text = dgv_coba.CurrentRow.Cells(0).Value.ToString
        TextBox2.Text = dgv_coba.CurrentRow.Cells(1).Value.ToString
        TextBox3.Text = dgv_coba.CurrentRow.Cells(2).Value.ToString
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim cmdHapus As New OleDbCommand
        cmdHapus.Connection = _koneksi
        cmdHapus.CommandType = CommandType.Text
        x = MessageBox.Show("Yakin Data Akan di Hapus ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If x = vbYes Then
            cmdHapus.CommandText = "DELETE FROM " + "TMhs WHERE NIM=" + TextBox1.Text
            cmdHapus.ExecuteNonQuery()
        End If
        Bs_Coba.RemoveCurrent()
        dgv_coba.Refresh()

        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        datatabelku.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        komandambil.Connection = _koneksi
        komandambil.CommandType = CommandType.Text
        komandambil.CommandText = "SELECT * FROM " + "TMhs WHERE NIM LIKE '%" + TextBox4.Text + "%'"

        dataadapterku.SelectCommand = komandambil
        dataadapterku.Fill(datatabelku)

        dgv_coba.Refresh()
        Bs_Coba.DataSource = datatabelku
        dgv_coba.DataSource = Bs_Coba
        Bs_Coba.MoveFirst()
    End Sub
End Class
