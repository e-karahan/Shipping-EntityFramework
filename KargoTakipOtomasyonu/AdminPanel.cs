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
    public partial class AdminPanel : Form
    {
        shippingEntities db = new shippingEntities();
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            var username = txtKullaniciAd.Text;
            var password = txtSifre.Text;
            var user = (from item in db.Admin
                       where  
                              item.Kullanici_Adi == username & item.Sifre == password
                       select item).FirstOrDefault();
            if(txtKullaniciAd.Text == " " || txtSifre.Text == "")
            {
                MessageBox.Show("Kullanıcı Bilgileri Boş Geçilemez");
            }
            else
            {
                if (user != null)
                {
                    AdminControl admincontrol = new AdminControl();
                    this.Hide();
                    admincontrol.Show();
                }
                else
                {
                    MessageBox.Show("Giriş Bilgileriniz Yanlıs");
                }
            }
          
            
        }
    }
}
