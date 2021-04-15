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
    public partial class RandevuIptal : Form
    {
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
        DataGridView dgv2 = new DataGridView();//datagridview
        public RandevuIptal()
        {
            connection.Open();//baglantı kurulur
            sqCom.Connection = connection;
            InitializeComponent();
        }

        private void RandevuIptal_Load(object sender, EventArgs e)
        {
           
            dgv2.Size = new Size(240, 249);
            dgv2.Location = new Point(89, 29);
            sqCom.CommandText = @"SELECT * FROM Randevu ";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            sqDa.Fill(dtProd);
            dgv2.DataSource = dtProd;
            this.Controls.Add(dgv2);
            dgv2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String str = dgv2.CurrentRow.Cells[1].Value.ToString();
            String s1 = dgv2.CurrentRow.Cells[2].Value.ToString();
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(s1);
            String s2;
            s2 = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt); // datetime formats
            MessageBox.Show(str);
            int hasta_kimlik_no = Int32.Parse(str);
            sqCom.CommandText = @"DELETE FROM Randevu 
                                 WHERE Hasta_Kimlik_No='" + hasta_kimlik_no + "' And Randevu.Randevu_Tarih<='"+s2+"'";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            MessageBox.Show("Kayıt başarıyla silindi");
            this.Close();
           
        }
    }
}
