using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace final_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Loadd();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-2PLFE08\\SQLEXPRESS; Initial Catalog = student; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader reader;

        string id;
        bool Mode = true;
        string Sql;

        public void Loadd()
        {
            try
            {
                Sql = "select * from student";
                cmd = new SqlCommand(Sql, con);
                con.Open();
                reader = cmd.ExecuteReader();
                dgv1.Rows.Clear();

                while (reader.Read())
                {
                    dgv1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void getId(string id)
        {
            Sql = "select * from student where id = '" + id + "'";
            cmd = new SqlCommand(Sql, con);
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtFName.Text = reader[1].ToString();
                txtLName.Text = reader[2].ToString();
                txtDep.Text = reader[3].ToString();
                txtCourse.Text = reader[4].ToString();
                txtFee.Text = reader[5].ToString();
           
            }
        }
        //mode = true means add otherwise it's update
        private void btnSave_Click(object sender, EventArgs e)
        {
            String FName = txtFName.Text;
            String LName = txtLName.Text;
            String Dep = txtDep.Text;
            String Course = txtCourse.Text;
            String Fee = txtFee.Text;

            if (Mode == true)
            {
                Sql = "insert into Student (FName,LName,Dep,Course,Fee) values (@FName,@LName,@Dep,@Course,@Fee)";
                con.Open();
                cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@FName", FName);
                cmd.Parameters.AddWithValue("@LName", LName);
                cmd.Parameters.AddWithValue("@Dep", Dep);
                cmd.Parameters.AddWithValue("@Course", Course);
                cmd.Parameters.AddWithValue("@Fee", Fee);
                MessageBox.Show("Student Added Successfuly!");
                cmd.ExecuteNonQuery();
                txtFName.Clear();
                txtLName.Clear();
                txtDep.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtFName.Focus();
            }
            else
            {
                id = dgv1.CurrentRow.Cells[0].Value.ToString();
                Sql = "update Student set FName= @Fname, LName = @LName, Dep =@Dep, Course = @Course, Fee = @Fee where id = @id";
                con.Open();
                cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@FName", FName);
                cmd.Parameters.AddWithValue("@LName", LName);
                cmd.Parameters.AddWithValue("@Dep", Dep);
                cmd.Parameters.AddWithValue("@Course", Course);
                cmd.Parameters.AddWithValue("@Fee", Fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record updated Successfuly!");
                cmd.ExecuteNonQuery();
                txtFName.Clear();
                txtLName.Clear();
                txtDep.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtFName.Focus();
                btnSave.Text = "Save";
                Mode = true;
            }
            con.Close();
        }
        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dgv1.CurrentRow.Cells[0].Value.ToString();
                getId(id);
                btnSave.Text = "Edit";
                Mode = true;
            }
            else if (e.ColumnIndex == dgv1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dgv1.CurrentRow.Cells[0].Value.ToString();
                Sql = "delete from student where id = @id";
                con.Open();
                cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully!");
                con.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Loadd();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFName.Clear();
            txtLName.Clear();
            txtDep.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtFName.Focus();
            btnSave.Text = "Save";
            Mode = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Loadd();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtFName.Clear();
            txtLName.Clear();
            txtDep.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtFName.Focus();
            btnSave.Text = "Save";
            Mode = true;
        }
    }
}
    

