﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino_app
{
    public partial class MailForm : Form
    {
        Label lbl, lbl1;
        TextBox NimiTxt, EmailTxt;
        Button EsitaBtn;
        string SaadaEmail;
        string SaadaNimi;
        int[] _tag;
        int _Suuremus;
        string _SelectedFilm;
        DateTime dateTime;
        Label _lbl;

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\Kino_app\Kino_app\AppData\KinnoBaas.mdf;Integrated Security=True");
        SqlCommand command;

        public MailForm(int[] tag, int Suuremus, string SelectedFilm, DateTime dateTimePicker, Label lbl)
        {
            

            _tag = tag;
            _Suuremus = Suuremus;
            _SelectedFilm = SelectedFilm;
            dateTime = dateTimePicker;
            _lbl = lbl;

            lbl = new Label()
            {
                Text = "Nimi",
                Size = new Size(27, 13)

            };
            lbl.Location = new Point(181, 67);
            lbl1 = new Label()
            {
                Text = "Email",
                Size = new Size(32, 13)
            };
            lbl1.Location = new Point(181, 115);

            NimiTxt = new TextBox()
            { 
                Size = new Size(273, 20)
            };

            EmailTxt = new TextBox()
            {
                Size = new Size(273, 20)
            };

            NimiTxt.Location = new Point(243, 64);
            EmailTxt.Location = new Point(243, 115);


            EsitaBtn = new Button()
            {
                Text = "Esita",
                Font = new Font("Microsoft Sans Serif", 18),
                Margin = new Padding(3, 3, 3, 3),
                Size = new Size(300, 100)
            };

            EsitaBtn.Click += EsitaBtn_Click;

            EsitaBtn.Location = new Point(284, 193);



            this.Controls.Add(lbl);
            this.Controls.Add(lbl1);
            this.Controls.Add(NimiTxt);
            this.Controls.Add(EmailTxt);
            this.Controls.Add(EsitaBtn);
            this.Size = new Size(816, 489);
        }

        private void EsitaBtn_Click(object sender, EventArgs e)
        {
            string adress = "ilja200303@gmail.com";
            string Salasona = System.IO.File.ReadAllText(@"C:\Users\opilane\Desktop\Password.txt");
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("iljaharbi@gmail.com", Salasona),
                    EnableSsl = true
                };
                mail.From = new MailAddress("iljaharbi@gmail.com");
                mail.To.Add(adress);
                mail.Subject = "test";
                mail.Body = "test";
                smtpClient.Send(mail);


                connection.Open();
                command = new SqlCommand("INSERT INTO KinnoTable(rida, koht, SaaliSuuremus, filmiNimetus, aeg) VALUES(@rida, @koht, @saaliSuuremus, @filmiNimetus, @aeg)", connection);

                command.Parameters.AddWithValue("@rida", (_tag[0] + 1));
                command.Parameters.AddWithValue("@koht", (_tag[1] + 1));
                command.Parameters.AddWithValue("@saaliSuuremus", _Suuremus);
                command.Parameters.AddWithValue("@filmiNimetus", _SelectedFilm);
                command.Parameters.AddWithValue("@aeg", dateTime);

                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Andmed on lisatud");
                _lbl.BackColor = Color.Red;

                MessageBox.Show((_tag[0] + 1).ToString() + " " + (_tag[1] + 1).ToString());


            }

            catch (Exception)
            {
                MessageBox.Show("Viga!!!");
            }
        }
    }
}
