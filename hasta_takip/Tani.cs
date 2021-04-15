using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class Tani : Form
    {
        public SqlConnection connection = new SqlConnection("server=SINAN_7\\SQLEXPRESS;database=AILE_HEKIMI;integrated security=SSPI");
        public SqlCommand sqCom = new SqlCommand();
        public SqlCommand sqCom2 = new SqlCommand();
        DataTable dtProd = new DataTable();
        SqlDataAdapter sqDa = new SqlDataAdapter();
        ListView tani_listesi = null;
        public int p_no;
        public int k_no;
        
        public Tani()
        {
            connection.Open();//baglantı kurulur
            sqCom.Connection = connection;
            sqCom2.Connection = connection;
            InitializeComponent();
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
        }

        private void Tani_Load(object sender, EventArgs e)//tanıyla ilgili tablolar treeviewe yüklenir
        {
            TreeNode ResultNode;
            TreeNode aa;
            SqlDataReader dataReader;
            List<String> liste= new List<String>();
            List<String> liste2 = new List<String>();
            List<String> liste3 = new List<String>();
            List<String> liste4 = new List<String>();

            sqCom.CommandText = @"SELECT Ust_Tani_Adi , Ust_Tani_Id
                                FROM Ust_Tani";
            sqCom.CommandType = CommandType.Text;
            dataReader = sqCom.ExecuteReader();
            dataReader.Read();
            treeView1.Nodes.Add(dataReader.GetString(0).ToString());//en sondaki elemanı göz ardı etmemesini sağlıyor 
            liste.Add(dataReader.GetDecimal(1).ToString());
            liste2.Add(dataReader.GetString(0));
            while (dataReader.Read())//ust tanı ekleme----------
            {
                try
                {
                    treeView1.Nodes.Add(dataReader.GetString(0).ToString());//ust tanı bilgisi treeviewe eklendi
                    liste.Add(dataReader.GetDecimal(1).ToString());
                    liste2.Add(dataReader.GetString(0));
                }
                catch { }
            }
            dataReader.Dispose();

            for (int i = 0; i < liste.Count; i++)//alt tanı ekleme------
            {
                sqCom.CommandText = @"SELECT Alt_Tani_Adi,Alt_Tani_Id
                                         FROM Alt_Tani
                                         WHERE Ust_Tani_Id ='" + liste[i].ToString() + "'";
                sqCom.CommandType = CommandType.Text;
                dataReader = sqCom.ExecuteReader();
                dataReader.Read();
                ResultNode = GetNode(liste2[i].ToString());
                ResultNode.Nodes.Add(dataReader.GetString(0));//alt tanı bilgisi eklenir
                liste3.Add(dataReader.GetString(0));//alt tanı adını tutar
                liste4.Add(dataReader.GetDecimal(1).ToString());//alt tanı id tutar
                while (dataReader.Read())
                {
                    try
                    {
                        ResultNode = GetNode(liste2[i].ToString());
                        ResultNode.Nodes.Add(dataReader.GetString(0));//alt tanı bilgisi eklenir
                        liste3.Add(dataReader.GetString(0));//alt tanı adını tutar
                        liste4.Add(dataReader.GetDecimal(1).ToString());//alt tanı id tutar
                    }
                    catch { }
                }
                dataReader.Dispose();
            }
            int genel = liste.Count;
          
            for (int i = 0; i < liste3.Count; i++)//tanı ekleme----------
            {
             
                sqCom.CommandText = @"SELECT Icd_Kod,Icd_Ad
                                         FROM Tani
                                         WHERE Alt_Tani_Id ='" + liste4[i].ToString() + "'";
                sqCom.CommandType = CommandType.Text;
                dataReader = sqCom.ExecuteReader();
                dataReader.Read();

                ResultNode = GetNode2(liste3[i].ToString(),genel);
                ResultNode.Nodes.Add(dataReader.GetString(1));
                while (dataReader.Read())
                {
                    try
                    {
                        ResultNode = GetNode2(liste3[i].ToString(),genel);//ilgili alt tanının treeviewdeki node unu bul getir
                        ResultNode.Nodes.Add(dataReader.GetString(1));//tanı eklenir
                    }
                    catch { }
                }
                dataReader.Dispose();
            }

            tani_listesi = new ListView();//dinamik listview oluşturuluyor
            groupBox1.Controls.Add(tani_listesi);
            tani_listesi.View = View.Details;
            tani_listesi.Location = new Point(25,29);
            tani_listesi.Size = new Size(418, 144);

            tani_listesi.Columns.Add("ICD Kod", 100, HorizontalAlignment.Center);
            tani_listesi.Columns.Add("ICD Ad", 100, HorizontalAlignment.Center);

            
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
        private TreeNode GetNode2(string strText,int genel)//treeview da gezerek aranan nodu bulur ve değerini döndürür
        {
            for (int i = 0; i < genel; i++)
            {
                for (int j = 0; j < treeView1.Nodes[i].Nodes.Count; j++)
                {
                    MessageBox.Show(treeView1.Nodes[i].Nodes[j].Text);
                    if (treeView1.Nodes[i].Nodes[j].Text == strText)
                        return treeView1.Nodes[i].Nodes[j];
                }
            }
            return null;
        }
        private void treeView1_AfterSelect(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            SqlDataReader dataReader;
            String i_k;//icd kod
            string selected = treeView1.SelectedNode.Text;
            sqCom.CommandText = @"SELECT Icd_Kod
                                         FROM Tani
                                         WHERE Icd_Ad ='" + selected + "'";
            sqCom.CommandType = CommandType.Text;
            dataReader = sqCom.ExecuteReader();
            dataReader.Read();
            i_k = dataReader.GetDecimal(0).ToString();
            ListViewItem t = new ListViewItem(i_k, 0);
            tani_listesi.Items.AddRange(new ListViewItem[] { t });
            t.SubItems.Add(selected);
            tani_listesi.FullRowSelect = true;
        }
    }
}
