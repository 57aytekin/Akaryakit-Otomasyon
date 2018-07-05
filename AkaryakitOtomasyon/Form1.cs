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

namespace AkaryakitOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textboxlariTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        SqlConnection con = new SqlConnection("Data Source=desktop-ll44d11\\sqlexpress;Initial Catalog=akaryakit;Integrated Security=True");

        
        int E_benzin95 = 0, E_benzin97 = 0, E_dizel = 0, E_euroDizel = 0, E_lpg = 0;
        double F_benzin95 = 0, F_benzin97 = 0, F_dizel = 0, F_euroDizel = 0, F_lpg = 0;
        int benzin95 = 0, benzin97 = 0, dizel = 0, euroDizel = 0, lpg = 0;

        private void indirimVeFaizhesapla(Double benzinTuru, Label label, TextBox textbox)
        {
            try
            {
                benzinTuru = Convert.ToDouble(label.Text);
                benzinTuru = benzinTuru + (Convert.ToDouble(textbox.Text) * benzinTuru / 100);
                label.Text = benzinTuru.ToString();
            }catch(Exception e)
            {
                string hata = e.Message;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            indirimVeFaizhesapla(F_benzin95, label16, textBox10);
            indirimVeFaizhesapla(F_benzin97, label15, textBox9);
            indirimVeFaizhesapla(F_dizel, label14, textBox8);
            indirimVeFaizhesapla(F_euroDizel, label13, textBox7);
            indirimVeFaizhesapla(F_lpg, label12, textBox6);

            con.Open();
            string komut = "update fiyat set benzin95='"+ label16.Text + "', benzin97='"+label15.Text+ "', dizel='"+label14.Text+ "', euroDizel='"+label13.Text+ "', lpg='"+label12.Text+ "' where id="+id+"";
            SqlCommand cmd = new SqlCommand(komut, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label29.Text = "____________";
            if (comboBox1.Text == "Benzin (95)")
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;

                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = 0;

            }
            else if(comboBox1.Text == "Benzin (97)")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;

                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = 0;
            }
            else if(comboBox1.Text == "Dizel"){
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;

                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = 0;
            }
            else if(comboBox1.Text == "Euro Dizel")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = false;

                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = 0;
            }
            else if(comboBox1.Text == "LPG")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = true;

                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Enabled == true)
            {
                int depo = Convert.ToInt32(label6.Text);
                int numericDeger = Convert.ToInt32(numericUpDown1.Value);
                benzin95 = depo - numericDeger;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update depo set benzin95='" + benzin95 + "' where id=" + id + "", con);
                cmd.ExecuteNonQuery();
                veriler(label6, label7, label8, label9, label10, "depo");
                numericupdownguncelle();
                con.Close();
                double ucret = (Convert.ToDouble(label16.Text)) * (Convert.ToInt32(numericUpDown1.Value));
                label29.Text = Convert.ToString(ucret);
                numericUpDown1.Value = 0;
            }
            else if(numericUpDown2.Enabled == true)
            {
                benzin97 = (Convert.ToInt32(label7.Text)) - (Convert.ToInt32(numericUpDown2.Value.ToString()));
                con.Open();
                SqlCommand cmd = new SqlCommand("Update depo set benzin97='" + benzin97 + "' where id=" + id + "", con);
                cmd.ExecuteNonQuery();
                veriler(label6, label7, label8, label9, label10, "depo");
                numericupdownguncelle();
                con.Close();
                double ucret = (Convert.ToDouble(label15.Text)) * (Convert.ToInt32(numericUpDown2.Value));
                label29.Text = Convert.ToString(ucret);
                numericUpDown2.Value = 0;
            }
            else if(numericUpDown3.Enabled == true)
            {
                dizel = (Convert.ToInt32(label8.Text)) - (Convert.ToInt32(numericUpDown3.Value.ToString()));
                con.Open();
                SqlCommand cmd = new SqlCommand("Update depo set dizel='" + dizel + "' where id=" + id + "", con);
                cmd.ExecuteNonQuery();
                veriler(label6, label7, label8, label9, label10, "depo");
                numericupdownguncelle();
                con.Close();
                double ucret = (Convert.ToDouble(label14.Text)) * (Convert.ToInt32(numericUpDown3.Value));
                label29.Text = Convert.ToString(ucret);
                numericUpDown3.Value = 0;
            }
            else if(numericUpDown4.Enabled == true)
            {
                euroDizel = (Convert.ToInt32(label9.Text)) - (Convert.ToInt32(numericUpDown4.Value.ToString()));
                con.Open();
                SqlCommand cmd = new SqlCommand("Update depo set euroDizel='" + euroDizel + "' where id=" + id + "", con);
                cmd.ExecuteNonQuery();
                veriler(label6, label7, label8, label9, label10, "depo");
                numericupdownguncelle();
                con.Close();
                double ucret = (Convert.ToDouble(label13.Text)) * (Convert.ToInt32(numericUpDown4.Value));
                label29.Text = Convert.ToString(ucret);
                numericUpDown4.Value = 0;
            }
            else if(numericUpDown5.Enabled == true)
            {
                lpg = (Convert.ToInt32(label10.Text)) - (Convert.ToInt32(numericUpDown5.Value.ToString()));
                con.Open();
                SqlCommand cmd = new SqlCommand("Update depo set lpg='" + lpg + "' where id=" + id + "", con);
                cmd.ExecuteNonQuery();
                veriler(label6, label7, label8, label9, label10, "depo");
                numericupdownguncelle();
                con.Close();
                double ucret = (Convert.ToDouble(label12.Text)) * (Convert.ToInt32(numericUpDown5.Value));
                label29.Text = Convert.ToString(ucret);
                numericUpDown5.Value = 0;
            }

        }

        private void progresbarDegerleri(ProgressBar progress, Label label)
        {
            progress.Maximum = 1000;
            progress.Value = Convert.ToInt32(label.Text);

            
        }
        private void veriler(Label l1, Label l2, Label l3, Label l4, Label l5, string k)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string komut = "Select * from " + k;
                SqlCommand cmd = new SqlCommand(komut,con);
                SqlDataReader oku = cmd.ExecuteReader();
                while (oku.Read())
                {
                    l1.Text = oku["benzin95"].ToString();
                    l2.Text = oku["benzin97"].ToString();
                    l3.Text = oku["dizel"].ToString();
                    l4.Text = oku["euroDizel"].ToString();
                    l5.Text = oku["lpg"].ToString();

                }
                con.Close();
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
            }
            progresbarDegerleri(progressBar1, label6);
            progresbarDegerleri(progressBar2, label7);
            progresbarDegerleri(progressBar3, label8);
            progresbarDegerleri(progressBar4, label9);
            progresbarDegerleri(progressBar5, label10);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label29.Text = "____________";
            veriler(label6, label7, label8, label9, label10, "depo");
            numericupdownguncelle();

            string[] yakitTurleri = { "Benzin (95)", "Benzin (97)", "Dizel", "Euro Dizel", "LPG" };
            comboBox1.Items.AddRange(yakitTurleri);
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
            numericUpDown5.Enabled = false;

            numericUpDown1.ReadOnly = true;
            numericUpDown2.ReadOnly = true;
            numericUpDown3.ReadOnly = true;
            numericUpDown4.ReadOnly = true;
            numericUpDown5.ReadOnly = true;
            
        }

        private void numericupdownguncelle()
        {
            numericUpDown1.Maximum = Convert.ToInt32(label6.Text);
            numericUpDown2.Maximum = Convert.ToInt32(label7.Text);
            numericUpDown3.Maximum = Convert.ToInt32(label8.Text);
            numericUpDown4.Maximum = Convert.ToInt32(label9.Text);
            numericUpDown5.Maximum = Convert.ToInt32(label10.Text);

        }

        int id = 1;
        private void hataYakala(int benzinTuru, TextBox ilgiliTextbox)
        {
            try
            {
                benzinTuru = Convert.ToInt32(ilgiliTextbox.Text);
                if (benzinTuru > 1000 || benzinTuru <= 0)
                {
                    ilgiliTextbox.Text = "Hata";
                }
            }
            catch (Exception ex)
            {
                ilgiliTextbox.Text = "Hata";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            hataYakala(E_benzin95, textBox1);
            hataYakala(E_benzin97, textBox2);
            hataYakala(E_dizel, textBox3);
            hataYakala(E_euroDizel, textBox4);
            hataYakala(E_lpg, textBox5);
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string komut = "Update depo set benzin95='" + textBox1.Text.ToString() + "', benzin97='" + textBox2.Text.ToString() + "', dizel='" + textBox3.Text.ToString() + "', euroDizel='" + textBox4.Text.ToString() + "', lpg='" + textBox5.Text.ToString() + "' where id=" + id + "";
                SqlCommand cmd = new SqlCommand(komut, con);
                cmd.ExecuteNonQuery();

                con.Close();
                veriler(label6, label7, label8, label9, label10, "depo");
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = 0;
                numericupdownguncelle();

            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
            }
            textboxlariTemizle();
        }
        
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            veriler(label16, label15, label14, label13, label12, "fiyat");
        }
        
    }
}
