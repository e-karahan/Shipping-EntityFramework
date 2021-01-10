using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KargoTakipOtomasyonu
{
    public partial class KargoDuzenle : Form
    {
        shippingEntities db = new shippingEntities();
        public KargoDuzenle()
        {
            InitializeComponent();
        }

        private void btnkargodegistir_Click(object sender, EventArgs e)
        {
            if (tbkargoid.Text == "")
            {
                MessageBox.Show("Lütfen Listeden Seçim Yapınız");
            }
            else
            {
                if (gbgonderici.Visible == false)
                {
                    gbgonderici.Visible = true;
                    tbkargoid2.Text = tbkargoid.Text;
                }
                else if (gbgonderici.Visible == true)
                {
                    gbgonderici.Visible = false;
                    tbkargoid2.Text = "";
                }
            }
        }

        private void KargoDuzenle_Load(object sender, EventArgs e)
        {
            kargobilgicek();
            gondericicek();
        }
        private void gondericicek()
        {
            datagridgonderici.DataSource = db.Musteri.ToList();
        }
        private void kargobilgicek()
        {
            var sorgu = from d1 in db.KargoBilgi
                        join d2 in db.Musteri
                        on d1.Musteri_ID equals d2.ID
                        select new
                        {
                            MüşteriID = d2.ID,
                            KargoID = d1.ID,
                            Gönderici = d2.Ad + " " + d2.Soyad,
                            GöndericiTC = d2.TC,
                            GöndericiTel = d2.Telefon,
                            KargoBarkod = d1.Barkod,
                            AlıcıAd = d1.Alici_Ad,
                            AlıcıSoyad=d1.Alici_Soyad,
                            AlıcıTel = d1.Alici_Tel,
                            AlıcıAdres = d1.Alici_Adres,
                            GonderimTarihi = d1.Gonderim_Tarih,
                            KargoDurumu = d1.Durum,
                            KargoIcerik = d1.Icerik,
                            KargoHassaslık = d1.Hassaslik

                        };
            datagridkargobilgi.DataSource = sorgu.ToList();
        }
        private void tbClear()
        {
            tbkargoid.Text = "";
            tbbarkod.Text = "";
            tbaliciad.Text = "";
            tbalicisoyad.Text = "";
            tbalicitel.Text = "";
            tbaliciadres.Text = "";
            cbdurum.Text = "";
            tbkargoicerik.Text = "";
            cbhassas.Text = "";
        }
        private void tbClear2()
        {
            tbkargoid2.Text = "";
            tbgondericiid.Text = "";
            tbgondericiad2.Text = "";
            tbgondericisoyad2.Text = "";
            tbgondericitc2.Text = "";
            tbgondericitel2.Text = "";
        }
        private void datagridkargobilgi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagridkargobilgi.Rows[gridindex];
            tbkargoid.Text = selectedRow.Cells[1].Value.ToString();
            tbbarkod.Text = selectedRow.Cells[5].Value.ToString();
            tbaliciad.Text = selectedRow.Cells[6].Value.ToString();
            tbalicisoyad.Text = selectedRow.Cells[7].Value.ToString();
            tbalicitel.Text = selectedRow.Cells[8].Value.ToString();
            tbaliciadres.Text = selectedRow.Cells[9].Value.ToString();
            cbdurum.Text = selectedRow.Cells[11].Value.ToString();
            tbkargoicerik.Text = selectedRow.Cells[12].Value.ToString();
            cbhassas.Text = selectedRow.Cells[13].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tbkargoid.Text=="")
            {
                MessageBox.Show("Lütfen Düzenleme Yapamadan Önce Listeden Seçim Yapınız");
            }
            else if(tbbarkod.Text=="" || tbaliciad.Text=="" || tbalicisoyad.Text=="" || tbalicitel.Text==""||tbaliciadres.Text=="" ||cbdurum.Text=="" || tbkargoicerik.Text=="" || cbhassas.Text == "")
            {
                MessageBox.Show("Lütfen İçerik Bilgilerini Eksiksiz Giriniz");
            }
            else
            {
                int kargoid = int.Parse(tbkargoid.Text);
                var kargoDuzenle = db.KargoBilgi.Find(kargoid);
                kargoDuzenle.Barkod = tbbarkod.Text;
                kargoDuzenle.Alici_Ad = tbaliciad.Text;
                kargoDuzenle.Alici_Soyad = tbalicisoyad.Text;
                kargoDuzenle.Alici_Tel = tbalicitel.Text;
                kargoDuzenle.Alici_Adres = tbaliciadres.Text;
                kargoDuzenle.Durum = cbdurum.Text;
                kargoDuzenle.Icerik = tbkargoicerik.Text;
                kargoDuzenle.Hassaslik = cbhassas.Text;
                db.SaveChanges();
                MessageBox.Show("Kargo Bilgileri Başarılı Bir Şekilde Güncellenmiştir");
                kargobilgicek();
                tbClear();
                
                
            }
        }

        private void datagridgonderici_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagridgonderici.Rows[gridindex];
            tbgondericiid.Text = selectedRow.Cells[0].Value.ToString();
            tbgondericiad2.Text = selectedRow.Cells[1].Value.ToString();
            tbgondericisoyad2.Text = selectedRow.Cells[2].Value.ToString();
            tbgondericitc2.Text = selectedRow.Cells[3].Value.ToString();
            tbgondericitel2.Text = selectedRow.Cells[4].Value.ToString();
        }

        private void btngondericiekle_Click(object sender, EventArgs e)
        {
            int gondericiid = int.Parse(tbkargoid2.Text);
            var gondericiDuzenle = db.KargoBilgi.Find(gondericiid);
            gondericiDuzenle.Musteri_ID = int.Parse(tbgondericiid.Text);
            db.SaveChanges();
            MessageBox.Show("Secilen Kargonun Göndericisi Başarılı Bir Şekilde Güncellendi");
            kargobilgicek();
            tbClear2();
            tbClear();
            gbgonderici.Visible = false;
        }
    }
}
