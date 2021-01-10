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
    public partial class AdminControl : Form
    {
        shippingEntities db = new shippingEntities();
        int gondericiid;
        public AdminControl()
        {
            InitializeComponent();
        }

        private void btnkargoekle_Click(object sender, EventArgs e)
        {
            if (tbgondericiid.Text == "" || tbgondericiad.Text == "" || tbgondericisoyad.Text == "" || tbgondericitc.Text == "" || tbgondericitel.Text == "")
            {
                MessageBox.Show("Lütfen Listeden Gönderici Seçimi Yapınız");
            }
            else if (tbbarkod.Text == "" || tbaliciad.Text == "" || tbalicisoyad.Text == "" || tbaliciadres.Text == "" || tbalicitel.Text == "" || cbdurum.Text == "" || tbkargoicerik.Text == "" || cbhassas.Text == "")
            {
                MessageBox.Show("Lütfen Alıcı Bilgileri Doğru Bir Şekilde Giriniz");
            }
            else
            {
                KargoBilgi kargo = new KargoBilgi();
                kargo.Barkod = tbbarkod.Text;
                kargo.Musteri_ID =int.Parse(tbgondericiid.Text);
                kargo.Alici_Ad = tbaliciad.Text;
                kargo.Alici_Soyad = tbalicisoyad.Text;
                kargo.Alici_Tel = tbalicitel.Text;
                kargo.Alici_Adres = tbaliciadres.Text;
                kargo.Durum = cbdurum.Text;
                kargo.Icerik = tbkargoicerik.Text;
                kargo.Hassaslik = cbhassas.Text;
                db.KargoBilgi.Add(kargo);
                db.SaveChanges();
                MessageBox.Show("Kargo Bilgileri Başarıyla Kaydedildi");
                tbClear2();
            }
        }

        private void btngondericiekle_Click(object sender, EventArgs e)
        {
            if(tbgondericiad2.Text == "" || tbgondericisoyad2.Text == "" || tbgondericitc2.Text == "" || tbgondericitel2.Text == "")
            {
                MessageBox.Show("Lütfen Bilgileri Eksiksiz Giriniz");
            }
            else
            {
                Musteri musteri = new Musteri();
                musteri.Ad = tbgondericiad2.Text;
                musteri.Soyad = tbgondericisoyad2.Text;
                musteri.TC = tbgondericitc2.Text;
                musteri.Telefon = tbgondericitel2.Text;
                db.Musteri.Add(musteri);
                db.SaveChanges();
                MessageBox.Show("Gönderi Kaydı Başarılı Bir Şekilde Tamamlandı");
                gondericicek();
                tbClear();
            }
          

        }

        private void AdminControl_Load(object sender, EventArgs e)
        {
            gondericicek();
            teslimalinan();
            teslimedilen();
            dagitimacikan();
        }
        private void gondericicek()
        {
            datagridgonderici.DataSource = db.Musteri.ToList();
            datagridgonderici2.DataSource = db.Musteri.ToList();
        }
        private void teslimalinan()
        {
            //Kargo ID BARKOD ALICI ADI ALICI ADRES İÇERİK
            var sorgu =    from item in db.KargoBilgi
                           where item.Durum == "Teslim Alındı"
                           select new
                           {
                               item.ID,
                               item.Barkod,
                               item.Alici_Ad,
                               item.Alici_Adres,
                               item.Icerik
                           };
            datagridteslimalinan.DataSource = sorgu.ToList();
        }
        private void teslimedilen()
        {
            var sorgu = from item in db.KargoBilgi
                        where item.Durum == "Teslim Edildi"
                        select new 
                        {
                            item.ID,
                            item.Barkod,
                            item.Alici_Ad,
                            item.Alici_Adres,
                            item.Icerik
                        };
            datagridteslimedilen.DataSource = sorgu.ToList();
        }
        private void dagitimacikan()
        {
            var sorgu = from item in db.KargoBilgi
                        where item.Durum == "Dağıtıma Çıktı"
                        select new
                        {
                            item.ID,
                            item.Barkod,
                            item.Alici_Ad,
                            item.Alici_Adres,
                            item.Icerik
                        };
            datagriddagitim.DataSource = sorgu.ToList();
        }

        private void tbClear()
        {
            tbgondericiad2.Text = "";
            tbgondericisoyad2.Text = "";
            tbgondericitc2.Text = "";
            tbgondericitel2.Text = "";
        }
        private void tbClear2()
        {
            tbgondericiid.Text = "";
            tbgondericiad.Text = "";
            tbgondericisoyad.Text = "";
            tbgondericitc.Text = "";
            tbgondericitel.Text = "";
            tbbarkod.Text = "";
            tbaliciad.Text = "";
            tbalicisoyad.Text = "";
            tbaliciadres.Text = "";
            tbalicitel.Text = "";
            cbdurum.Text = "";
            tbkargoicerik.Text = "";
            cbhassas.Text = "";
        }

        private void btngondericiduzenle_Click(object sender, EventArgs e)
        {
            if (tbgondericiad2.Text == "" || tbgondericisoyad2.Text == "" || tbgondericitc2.Text == "" || tbgondericitel2.Text == "")
            {
                MessageBox.Show("Lütfen Bilgileri Eksiksiz Giriniz");
            }
            else
            {
                var gondericiDuzenle = db.Musteri.Find(gondericiid);
                gondericiDuzenle.Ad = tbgondericiad2.Text;
                gondericiDuzenle.Soyad = tbgondericisoyad2.Text;
                gondericiDuzenle.TC = tbgondericitc2.Text;
                gondericiDuzenle.Telefon = tbgondericitel2.Text;
                db.SaveChanges();
                MessageBox.Show("Müşteri Bilgileri Başarılı Bir Şekilde Güncellenmiştir");
                gondericicek();
                tbClear();
            }
           
        }

        private void btngondericisil_Click(object sender, EventArgs e)
        {
            if (tbgondericiad2.Text == "" || tbgondericisoyad2.Text == "" || tbgondericitc2.Text == "" || tbgondericitel2.Text == "")
            {
                MessageBox.Show("Lütfen Bilgileri Eksiksiz Giriniz");
            }
            else
            {
                var gondericiSil = db.Musteri.Find(gondericiid);
                db.Musteri.Remove(gondericiSil);
                db.SaveChanges();
                MessageBox.Show("Seçili Müşteri Başarıyla Silindi");
                gondericicek();
                tbClear();
            }
        }

        private void datagridgonderici_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagridgonderici.Rows[gridindex];
            gondericiid = int.Parse(selectedRow.Cells[0].Value.ToString());
            tbgondericiad2.Text = selectedRow.Cells[1].Value.ToString();
            tbgondericisoyad2.Text = selectedRow.Cells[2].Value.ToString();
            tbgondericitc2.Text = selectedRow.Cells[3].Value.ToString();
            tbgondericitel2.Text = selectedRow.Cells[4].Value.ToString();
        }

        private void tbara_TextChanged(object sender, EventArgs e)
        {
            if (rbad.Checked)
            {
                string aranan = tbara.Text;
                var degerler = from item in db.Musteri
                               where item.Ad.Contains(aranan)
                               select item;
                datagridgonderici.DataSource = degerler.ToList();
            }
            else if (rbtc.Checked)
            {
                string aranan = tbara.Text;
                var degerler = from item in db.Musteri
                               where item.TC.Contains(aranan)
                               select item;
                datagridgonderici.DataSource = degerler.ToList();
            }
        }

        private void datagridgonderici2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagridgonderici2.Rows[gridindex];
            tbgondericiid.Text = selectedRow.Cells[0].Value.ToString();
            tbgondericiad.Text = selectedRow.Cells[1].Value.ToString();
            tbgondericisoyad.Text = selectedRow.Cells[2].Value.ToString();
            tbgondericitc.Text = selectedRow.Cells[3].Value.ToString();
            tbgondericitel.Text = selectedRow.Cells[4].Value.ToString();
        }

        private void tbara2_TextChanged(object sender, EventArgs e)
        {
            if (rbad2.Checked)
            {
                string aranan = tbara2.Text;
                var degerler = from item in db.Musteri
                               where item.Ad.Contains(aranan)
                               select item;
                datagridgonderici2.DataSource = degerler.ToList();
            }
            else if (rbtc2.Checked)
            {
                string aranan = tbara2.Text;
                var degerler = from item in db.Musteri
                               where item.TC.Contains(aranan)
                               select item;
                datagridgonderici2.DataSource = degerler.ToList();
            }
        }

        private void kargoduzenle_Click(object sender, EventArgs e)
        {
            KargoDuzenle duzenle = new KargoDuzenle();
            duzenle.Show();
        }

        private void btndagitim_Click(object sender, EventArgs e)
        {
            if(lblkargoid.Text == "00")
            {
                MessageBox.Show("Lütfen Listeden Seçim Yapınız");
            }
            else
            {
                int kargoid = int.Parse(lblkargoid.Text);
                var dagitimduzenle = db.KargoBilgi.Find(kargoid);
                dagitimduzenle.Durum = "Dağıtıma Çıktı";
                db.SaveChanges();
                teslimalinan();
                teslimedilen();
                dagitimacikan();
                MessageBox.Show("Kargo Durumu Güncellendi");
                lblkargoid.Text = "00";
            }
        }

        private void tbalinanara_TextChanged(object sender, EventArgs e)
        {
            string aranan = tbalinanara.Text;
            var degerler = from item in db.KargoBilgi
                           where item.Barkod.Contains(aranan) & item.Durum == "Teslim Alındı"
                           select new
                           {
                               item.ID,
                               item.Barkod,
                               item.Alici_Ad,
                               item.Alici_Adres,
                               item.Icerik
                           };
            datagridteslimalinan.DataSource = degerler.ToList();
        }

        private void tbdagitimara_TextChanged(object sender, EventArgs e)
        {
            string aranan = tbdagitimara.Text;
            var degerler = from item in db.KargoBilgi
                           where item.Barkod.Contains(aranan) & item.Durum == "Dağıtıma Çıktı"
                           select new
                           {
                               item.ID,
                               item.Barkod,
                               item.Alici_Ad,
                               item.Alici_Adres,
                               item.Icerik
                           };
            datagriddagitim.DataSource = degerler.ToList();
        }

        private void tbteslimara_TextChanged(object sender, EventArgs e)
        {
            string aranan = tbteslimara.Text;
            var degerler = from item in db.KargoBilgi
                           where item.Barkod.Contains(aranan) & item.Durum == "Teslim Edildi"
                           select new
                           {
                               item.ID,
                               item.Barkod,
                               item.Alici_Ad,
                               item.Alici_Adres,
                               item.Icerik
                           };
            datagridteslimedilen.DataSource = degerler.ToList();
        }

        private void datagridteslimalinan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagridteslimalinan.Rows[gridindex];
            lblkargoid.Text = selectedRow.Cells[0].Value.ToString();
           
        }

        private void datagriddagitim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagriddagitim.Rows[gridindex];
            lblkargoid2.Text = selectedRow.Cells[0].Value.ToString();
        }

        private void datagridteslimedilen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int gridindex = e.RowIndex;
            DataGridViewRow selectedRow = datagridteslimedilen.Rows[gridindex];
            lblkargoid3.Text = selectedRow.Cells[0].Value.ToString();
        }

        private void btnteslim_Click(object sender, EventArgs e)
        {
            if (lblkargoid2.Text == "00")
            {
                MessageBox.Show("Lütfen Listeden Seçim Yapınız");
            }
            else
            {
                int kargoid = int.Parse(lblkargoid2.Text);
                var teslimduzenle = db.KargoBilgi.Find(kargoid);
                teslimduzenle.Durum = "Teslim Edildi";
                db.SaveChanges();
                teslimalinan();
                teslimedilen();
                dagitimacikan();
                MessageBox.Show("Kargo Durumu Güncellendi");
                lblkargoid2.Text = "00";
            }
        }

        private void btnarsiv_Click(object sender, EventArgs e)
        {
            if (lblkargoid3.Text == "00")
            {
                MessageBox.Show("Lütfen Listeden Seçim Yapınız");
            }
            else
            {
                int kargoid = int.Parse(lblkargoid3.Text);
                var arsivduzenle = db.KargoBilgi.Find(kargoid);
                arsivduzenle.Durum = "Arşiv";
                db.SaveChanges();
                teslimalinan();
                teslimedilen();
                dagitimacikan();
                MessageBox.Show("Kargo Durumu Güncellendi");
                lblkargoid3.Text = "00";
            }
        }
    }
}
