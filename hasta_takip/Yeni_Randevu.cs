using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace hasta_takip
{
    public partial class Yeni_Randevu : Form
    {
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
        DataGridView dgv = new DataGridView();//datagridview
        Form HastaSec = new Form();
        DataTable dtProd = new DataTable();
        SqlDataAdapter sqDa = new SqlDataAdapter();

        public Yeni_Randevu()
        {           
                connection.Open();//baglantı kurulur
                sqCom.Connection = connection;
                InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                button1.Enabled = false;
                HastaSec.Size = new Size(481,350);
                dgv.Size = new Size(240, 249);
                dgv.Location = new Point(89, 29);
                HastaSec.Controls.Add(dgv);
                HastaSec.Show();
                
                sqCom.CommandText = @"SELECT Hasta_Kimlik_No , Hasta_Adi ,Hasta_Soyadi ,Dogum_Tarihi,Cinsiyet,Telefon FROM Hasta ";
                sqCom.CommandType = CommandType.Text;
                sqCom.ExecuteScalar(); 
                sqDa.SelectCommand = sqCom;
                sqDa.Fill(dtProd);
                dgv.DataSource = dtProd;
                dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            
        }
        private void dataGridView1_CellDoubleClick( object sender, DataGridViewCellEventArgs e)
        {
            
               textBox1.Text = dgv.CurrentRow.Cells[0].Value.ToString();
               textBox2.Text = dgv.CurrentRow.Cells[1].Value.ToString();
               textBox3.Text = dgv.CurrentRow.Cells[2].Value.ToString();
               textBox4.Text = dgv.CurrentRow.Cells[3].Value.ToString().Substring(0,10);
               textBox5.Text = dgv.CurrentRow.Cells[4].Value.ToString(); 
               HastaSec.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int hasta_kimlik_no = Int32.Parse(textBox1.Text);
            DateTime dt = new DateTime();
            String s1 = dateTimePicker1.Text;
            dt = Convert.ToDateTime(s1);
            String s3;
            s3 = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt); // datetime formats

            sqCom.CommandText = @"INSERT INTO Randevu(Hasta_Kimlik_No,Randevu_Tarih) 
                                   VALUES('" + hasta_kimlik_no + "','"+s3+"')";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            MessageBox.Show("İşleminiz kaydedildi");
            this.Close();

        }
    }
}
