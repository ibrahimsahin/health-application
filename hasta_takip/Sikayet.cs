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
    public partial class Sikayet : Form
    {
        public static Muayene muayene;
        public  int k_no;
        public int p_no;
        private System.Data.DataSet dataSet;
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
        public SqlCommand sqCom2 = new SqlCommand();
        public List<string> liste = new List<string>();
        public List<string> liste2 = new List<string>();
        public Sikayet()
        {
            connection.Open();
            sqCom.Connection = connection;
            sqCom2.Connection = connection;
            InitializeComponent();
        }

        private void Sikayet_Load(object sender, EventArgs e)
        {
            using (dataSet = new System.Data.DataSet())
            {
                sqCom.CommandText = @"SELECT Sikayet_Adi FROM Sikayet";
                sqCom.CommandType = CommandType.Text;

                using (SqlDataReader dataReader = sqCom.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        object sikayet_adi = dataReader["Sikayet_Adi"];
                        listBox1.Items.Add(sikayet_adi.ToString());
                    }
                }
                sqCom.ExecuteScalar();  
            }
            this.listBox1.Click += new EventHandler(listBox1_Click);
            
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.Text);
            liste.Add(listBox1.Text);//şikayet isimlerini tutar
        }

        private void button1_Click(object sender, EventArgs e)//şikayet onayla
        {

            SqlDataReader dataReader;
            int sikayet_id;
            for(int i=0;i<liste.Count;i++)
            {
                    sqCom.CommandText = @"SELECT Sikayet_Id FROM Sikayet WHERE Sikayet_Adi='"+liste[i]+"'";
                    sqCom.CommandType = CommandType.Text;
                    dataReader = sqCom.ExecuteReader();
                    dataReader.Read();

                    sikayet_id = Int32.Parse(dataReader.GetDecimal(0).ToString());
                    dataReader.Dispose();
                    sqCom2.CommandText = @"INSERT INTO Muayene_Sikayet(Protokol_No,Hasta_Kimlik_No,Sikayet_Id,Sikayet_Aciklama,Sikayet_Hikaye) 
                                       VALUES('"+p_no+"','" + k_no + "','" + sikayet_id + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "')";
                    sqCom2.CommandType = CommandType.Text;
                    sqCom2.ExecuteScalar(); 
            }
            MessageBox.Show("İşleminiz kaydedildi");
            this.Close();
        }

    }
}
