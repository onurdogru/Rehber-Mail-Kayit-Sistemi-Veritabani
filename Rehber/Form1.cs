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

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        SqlConnection baglanti = new SqlConnection(@"Data Source=GROOVYPRIMAT\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");






        void listele() //OTOMATİK OLARAK ÇAĞIRMA METODU
        {
            DataTable dt = new DataTable();  //RAM TUTMAK İÇİN
            SqlDataAdapter da = new SqlDataAdapter("Select * from KISILER", baglanti);
            da.Fill(dt); //data adapterden gelen sorguyu, dt'nin içine doldur.
            dataGridView1.DataSource = dt;  
        }





        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            textBox1.Focus();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }






        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into KISILER (AD, SOYAD, TELEFON, MAIL) values (@P1, @P2, @P3, @P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", textBox2.Text);
            komut.Parameters.AddWithValue("@P2", textBox3.Text);
            komut.Parameters.AddWithValue("@P3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@P4", textBox4.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kisi Sisteme Kaydedildi");


            listele();
            temizle();

        }




        private void button4_Click(object sender, EventArgs e)
        {
            temizle();
        }







        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //grid'in eventlarından buldum. (ilgili texbox'a aktarmak için)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; //sıfırıncı indexin seçilen satırı hafızada tut.
            textBox1.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();




        }




        private void button3_Click(object sender, EventArgs e) //SİLME İŞLEMİ
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from KISILER where ID=" + textBox1.Text, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Rehberden Silindi");

            listele();
            temizle();
        }
    }
}


//Data Source=GROOVYPRIMAT\SQLEXPRESS;Initial Catalog=DboTicariOtomasyon;Integrated Security=True