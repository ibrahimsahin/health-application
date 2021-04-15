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
    
    public partial class Form1 : Form
    {
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
         
        DataGridView dgv = new DataGridView();//datagridview
      
        DateTimePicker bas_tar = new DateTimePicker();//datetimepickerlar
        DateTimePicker bit_tar = new DateTimePicker();
        Form HastaSec = new Form();

        public Form1()
        {
            connection.Open();//baglantı kurulur
            sqCom.Connection = connection;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//muayene modülü
        {
            Muayene m = new Muayene();
            m.Show();
        }

        private void button2_Click(object sender, EventArgs e)//randevu modülü
        {
            //formun alt kısmı
          
            dgv.Size = new Size(644,167);
            dgv.Location = new Point(82,177);
            AraMenu.Text = "Randevu Modülü";

            Button ran_ekle = new Button();//ekle sil butonları
            ran_ekle.Location = new Point(291, 365);
            ran_ekle.Size = new Size(92, 38);
            ran_ekle.Text = "Ekle";
            ran_ekle.Click += new EventHandler(ran_ekle_Click);
            Button ran_sil = new Button();//ekle sil butonları
            ran_sil.Location = new Point(420, 365);
            ran_sil.Size = new Size(92, 38);
            ran_sil.Text = "Sil";
            ran_sil.Click += new EventHandler(ran_sil_Click);

            AraMenu.Controls.Add(dgv);
            AraMenu.Controls.Add(ran_ekle);
            AraMenu.Controls.Add(ran_sil);





            //formun üst kısmı
            GroupBox Randevu_Sorgu_Group_Box = new GroupBox();
            Randevu_Sorgu_Group_Box.Size = new Size(684,129);
            Randevu_Sorgu_Group_Box.Location = new Point(39,37);
            AraMenu.Controls.Add(Randevu_Sorgu_Group_Box);
            Randevu_Sorgu_Group_Box.Text = "Sorgulama Kriterleri";
 
            Label l1 = new Label();//labellar
            l1.Text = "Başlangıç Tarihi";
            l1.Location = new Point(20,38);
            l1.Size = new Size(107,18);
            Label l2 = new Label();
            l2.Text = "Bitiş Tarihi";
            l2.Location = new Point(385,38);
            l2.Size = new Size(76,18);

           
            bas_tar.Location = new Point(133,32);
            bas_tar.Size = new Size(200,26);
           
            bit_tar.Location = new Point(467,32);
            bit_tar.Size = new Size(200,26);

            Button ran_sorgulama_butonu = new Button();
            ran_sorgulama_butonu.Location= new Point(320,82);
            ran_sorgulama_butonu.Size = new Size(104,28);
            ran_sorgulama_butonu.Text = "Sorgula";
            ran_sorgulama_butonu.Click += new EventHandler(sorgula_Click);
            
            Randevu_Sorgu_Group_Box.Controls.Add(l1);
            Randevu_Sorgu_Group_Box.Controls.Add(l2);
            Randevu_Sorgu_Group_Box.Controls.Add(bas_tar);
            Randevu_Sorgu_Group_Box.Controls.Add(bit_tar);
            Randevu_Sorgu_Group_Box.Controls.Add(ran_sorgulama_butonu);

            randevu_hepsi_sorgula(dgv);
           


        }

        private void button3_Click(object sender, EventArgs e)//bebek izlem modülü
        {

        }

        private void button4_Click(object sender, EventArgs e)//hasta modülü
        {

        }

        void randevu_hepsi_sorgula(DataGridView dgv)
        {
            String today = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Today);//bugünün tarihi
            sqCom.CommandText = @"SELECT  Randevu.Randevu_Tarih , Randevu.Protokol_No , Randevu.Hasta_Kimlik_No, 
                                 Hasta.Hasta_Adi , Hasta.Hasta_Soyadi , Hasta.Telefon 
                                 FROM Randevu , Hasta 
                                 WHERE Randevu.Hasta_Kimlik_No=Hasta.Hasta_Kimlik_No And Randevu.Randevu_Tarih='"+today+"'";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            //data adapter e bu komutun selectmi updatemi insertmi deletemi oldugunu belirlemeliyim
            sqDa.Fill(dtProd);// data table ı doldurur
            dgv.DataSource = dtProd;// gridde gostermeyi saglar

        }

        void sorgula_Click(object sender, EventArgs e)
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            String s1 = bas_tar.Text;
            String s2 = bit_tar.Text;
            dt1 = Convert.ToDateTime(s1);
            dt2 = Convert.ToDateTime(s2);
            String s3,s4;
            s3= String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt1); // datetime formats
            s4 = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt2);
          
            sqCom.CommandText = @"SELECT  Randevu.Randevu_Tarih , Randevu.Protokol_No , Randevu.Hasta_Kimlik_No,
                                  Hasta.Hasta_Adi , Hasta.Hasta_Soyadi , Hasta.Telefon 
                                  FROM Randevu , Hasta
                                  WHERE Randevu.Hasta_Kimlik_No=Hasta.Hasta_Kimlik_No And Randevu.Randevu_Tarih>='" + s3 + "' And Randevu.Randevu_Tarih<='"+s4+"'"+
                                  "ORDER BY Randevu.Randevu_Tarih";
            sqCom.CommandType = CommandType.Text;
            sqCom.ExecuteScalar();
            DataTable dtProd = new DataTable();
            SqlDataAdapter sqDa = new SqlDataAdapter();
            sqDa.SelectCommand = sqCom;
            //data adapter e bu komutun selectmi updatemi insertmi deletemi oldugunu belirlemeliyim
            sqDa.Fill(dtProd);// data table ı doldurur
            dgv.DataSource = dtProd;// gridde gostermeyi saglar
            
        }
        void ran_ekle_Click(object sender, EventArgs e) 
        {
            Yeni_Randevu yeni_randevu = new Yeni_Randevu();
            yeni_randevu.Show();
            
           
        }
        void ran_sil_Click(object sender, EventArgs e) 
        {
            RandevuIptal ri = new RandevuIptal();
            ri.Show();
            
        }  
    }
}
