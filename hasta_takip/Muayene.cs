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
     
    public partial class Muayene : Form
    {
        public static int kimlik_no=0;
        public static int p_no = 0;
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
        DataGridView dgv = new DataGridView();
        DataGridView dgv2 = new DataGridView();

        public Muayene()
        {
            connection.Open();
            sqCom.Connection = connection;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//muayene için hasta sorgulama
        {
            String ad=textBox1.Text;
            String soyad=textBox2.Text;
            int kimlik_no=0;
            if (textBox3.Text != "")
            {
                kimlik_no = Int32.Parse(textBox3.Text);
            }
            dgv.Size = new Size(392, 160);
            dgv.Location = new Point(34, 124);
            sqCom.CommandText = @"SELECT Hasta_Kimlik_No , Hasta_Adi ,Hasta_Soyadi , Dogum_Tarihi 
                                  FROM Hasta
                                  WHERE Hasta_Adi='"+ad+"' OR Hasta_Soyadi='"+soyad+"'OR Hasta_Kimlik_No='"+kimlik_no+"'";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqDa.Fill(dtProd);
            dgv.DataSource = dtProd;
            this.Controls.Add(dgv);
            dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//hastaların önceden oldukları muayene ekranı
        {
            String str = dgv.CurrentRow.Cells[0].Value.ToString();
            int hasta_kimlik_no = Int32.Parse(str);
            kimlik_no = hasta_kimlik_no;
            button11.Enabled = true;

            dgv2.Size = new Size(392, 150);
            dgv2.Location = new Point(34, 312);
            sqCom.CommandText = @"SELECT Muayene_Tarih,Protokol_No FROM Muayene WHERE Hasta_Kimlik_No='"+hasta_kimlik_no+"'";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqDa.Fill(dtProd);
            dgv2.DataSource = dtProd;
            this.Controls.Add(dgv2);
        }
        private void Muayene_Load(object sender, EventArgs e)//muayene ekranı görüntüleme anı
        {
            button11.Enabled = false;
        }
        private void button11_Click(object sender, EventArgs e)//muayene başlat
        {
            SqlDataReader dataReader=null;
            String pr_no=null;
            String today = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Today);//bugünün tarihi
            sqCom.CommandText = @"INSERT INTO Muayene(Hasta_Kimlik_No,Muayene_Tarih) 
                                       VALUES('" + kimlik_no  + "','" +today+"')";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            
            sqCom.CommandText = @"SELECT Protokol_No FROM Muayene";
            sqCom.CommandType = CommandType.Text;
            dataReader = sqCom.ExecuteReader();
            while (dataReader.Read())
            {
                try
                {
                    pr_no = dataReader["Protokol_No"].ToString();
                }
                catch { }
            }
            p_no = Int32.Parse(pr_no);
            textBox3.Text = kimlik_no.ToString();//textbox 3 otomatik olarak yazdır
            textBox2.Text = dgv.CurrentRow.Cells[2].Value.ToString();//textbox 3 otomatik olarak yazdır
            textBox1.Text = dgv.CurrentRow.Cells[1].Value.ToString();//textbox 3 otomatik olarak yazdır
            label4.Text = p_no+" PROTOKOL NOLU MUAYENE-MUAYENE AÇIK-";
            Muayene.ActiveForm.Text="ADI:" + textBox1.Text + " SOYADI:" + textBox2.Text + " TC NO:" + textBox3.Text;
            button11.Enabled = false;
            button1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled=false;
            dgv.Enabled = false;
            dgv2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)//şikayet alt modülü
        {
            Sikayet sikayet = new Sikayet();
            sikayet.k_no = kimlik_no;
            sikayet.p_no = p_no;
            sikayet.Show();
        }
        private void button3_Click(object sender, EventArgs e)//bulgu alt modülü
        {
            Bulgu bulgu = new Bulgu();
            bulgu.p_no = p_no;
            bulgu.k_no = kimlik_no;
            bulgu.Show();
        }
        private void button4_Click(object sender, EventArgs e)//tani alt modülü
        {
            Tani tani = new Tani();
            tani.p_no = p_no;
            tani.k_no = kimlik_no;
            tani.Show();
        }
    }
}
