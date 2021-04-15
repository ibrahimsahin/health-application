using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace hasta_takip
{
    public partial class Bulgu : Form
    {
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
        public SqlCommand sqCom2 = new SqlCommand();
        DataTable dtProd = new DataTable();
        SqlDataAdapter sqDa = new SqlDataAdapter();
        ListView bulgu_listesi=null;
        public int p_no;
        public int k_no;
        public Bulgu()
        {
            connection.Open();//baglantı kurulur
            sqCom.Connection = connection;
            sqCom2.Connection = connection;
            InitializeComponent();
            //treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            treeView1.AfterCheck += new TreeViewEventHandler(treeView1_AfterCheck);
        }
        private void Bulgu_Load(object sender, EventArgs e)//databasedeki mevcut bulgular ekran açılır açılmaz forma dolar
        {
            TreeNode ResultNode;
            TreeNode araNode;
            SqlDataReader dataReader;
            
            sqCom.CommandText = @"SELECT DISTINCT Bulgu_Bolge
                                FROM Bulgu";
            sqCom.CommandType = CommandType.Text;
            dataReader = sqCom.ExecuteReader();
            dataReader.Read();
            treeView1.Nodes.Add(dataReader.GetString(0).ToString());//en sondaki elemanı göz ardı etmemesini sağlıyor
            while (dataReader.Read())
            {
                try
                {
                    treeView1.Nodes.Add(dataReader.GetString(0).ToString());                  
                }
                catch { }
            }
            dataReader.Dispose();
            sqCom.CommandText = @"SELECT Bulgu_Id , Bulgu_Adi , Bulgu_Bolge
                                FROM Bulgu";
            sqCom.CommandType = CommandType.Text;
            dataReader = sqCom.ExecuteReader();
            dataReader.Read();
            ResultNode = GetNode(dataReader.GetString(2));
            ResultNode.Nodes.Add(dataReader.GetString(1));
            araNode = GetNode(dataReader.GetString(1));
          
            while (dataReader.Read())
            {
                try
                {
                    ResultNode = GetNode(dataReader.GetString(2));
                    ResultNode.Nodes.Add(dataReader.GetString(1));
                    araNode = GetNode(dataReader.GetString(1));
                    araNode.Checked = false;
                    ResultNode.Checked = false;
                }
                catch { }
            }
            dataReader.Dispose();
            
            bulgu_listesi = new ListView();//dinamik listview oluşturuluyor
            this.Controls.Add(bulgu_listesi);
            bulgu_listesi.View = View.Details;
            bulgu_listesi.Location = new Point(116, 288);
            bulgu_listesi.Size = new Size(301, 147);

            bulgu_listesi.Columns.Add("Bulgu Adı", 100, HorizontalAlignment.Center);
            bulgu_listesi.Columns.Add("Bulgu Değeri", 100, HorizontalAlignment.Center);
        }
        
        private TreeNode GetNode(string strText)//treeview da gezerek aranan nodu bulur ve değerini döndürür
        {

            IEnumerator iEnum = treeView1.Nodes.GetEnumerator();

            while (iEnum.MoveNext())
            {
                if (((TreeNode)iEnum.Current).Text == strText)
                    return ((TreeNode)iEnum.Current);
            }
            return null;
        }

 
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)//bulgu seçildiğinde
        {
            String[] temp = e.Node.ToString().Split(' ');
            String bulgu_="";
            int count = temp.Length;
            for (int i = 1; i < count; i++)
            {
                if (i > 1)
                    temp[i] = " " + temp[i];
                bulgu_ = bulgu_ + temp[i];
            }
            ListViewItem t = new ListViewItem(bulgu_,0);
            bulgu_listesi.Items.AddRange(new ListViewItem[]{t});
            t.SubItems.Add("var");
            bulgu_listesi.FullRowSelect = true;
        }

        private void button2_Click(object sender, EventArgs e)//seçilen bulguları iptal et
        {
           
            int sayi = bulgu_listesi.SelectedItems.Count;
            ListViewItem[] eleman= new ListViewItem[sayi];
            if (sayi > 0)
            {
                for (int i = 0; i < sayi; i++)
                    eleman[i] = bulgu_listesi.SelectedItems[i];
                for (int i = 0; i < sayi; i++)
                    bulgu_listesi.Items.Remove(eleman[i]);
            }
            else
                MessageBox.Show("Seçili Eleman yok");          
            
        }

        private void button1_Click(object sender, EventArgs e)//bulguları onayla
        {
            SqlDataReader dataReader;
            String b_adi=null;
            String b_id = null;
            int count = bulgu_listesi.Items.Count;
            for (int i = 0; i < count; i++)
            {
                b_adi = bulgu_listesi.Items[i].Text;
                sqCom.CommandText = @"SELECT Bulgu_Id FROM  Bulgu WHERE Bulgu_Adi='" + b_adi + "'";
                sqCom.CommandType = CommandType.Text;
                dataReader = sqCom.ExecuteReader();
                dataReader.Read();
                b_id = dataReader.GetDecimal(0).ToString();
                dataReader.Dispose();
                sqCom2.CommandText = @"INSERT INTO Muayene_Bulgu(Protokol_No,Hasta_Kimlik_No,Bulgu_Id,Bulgu_Deger) 
                                       VALUES('" + p_no + "','" + k_no + "','" + b_id + "','var')";
                sqCom2.CommandType = CommandType.Text;
                sqCom2.ExecuteScalar();

            }
            MessageBox.Show("İşleminiz Kaydedildi");
            this.Close();
        }
    }
}
