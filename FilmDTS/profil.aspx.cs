using FilmDTS.siniflar.veritabani;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS
{
    public partial class profil : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();

        FilmDal _filmDal = new FilmDal();
        int kullaniciID;
        protected void Page_Load(object sender, EventArgs e)
        {

            kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Page.RouteData.Values["KullaniciAdi"].ToString());

            UyeCek();
            IncelemelerYaz();
            AlintilarYaz();
            IzlenenlerYaz();
            PuanlarYaz();

            if (Convert.ToInt32(Session["KullaniciID"].ToString()) == kullaniciID )
            {
                areaArkadasEkleButonu.InnerHtml = ElementHazirla("Kendinizi arkadaş olarak ekleyemezsiniz.");
            }
            else{

                if (isFriend())
                {
                    areaArkadasEkleButonu.InnerHtml = ElementHazirla("Arkadaşsınız");
                }

            }

        }
        Uye uye = new Uye();
        public void UyeCek()
        {
            string kullaniciAdi = Convert.ToString(Page.RouteData.Values["KullaniciAdi"]);
            uye = _uyeDal.GetByKAdi(kullaniciAdi);
            adSoyad.InnerText = uye.Isim + " " + uye.Soyisim;
            profilFotografi.Src = "/fotograf/kullanici/" + kullaniciAdi + ".jpg";
        }

        public void IncelemelerYaz()
        {
            DataSet incelemeler = _filmDal.IncelemelerCekByKID(kullaniciID);
            Incelemeler1.DataSource = incelemeler.Tables[0];
            Incelemeler1.DataBind();
        }
        public void AlintilarYaz()
        {
            DataSet alintilar = _filmDal.AlintilarCekByKID(kullaniciID);
            Alintilar1.DataSource = alintilar.Tables[0];
            Alintilar1.DataBind();
        }
        public void IzlenenlerYaz()
        {
            DataSet izlenenler = _filmDal.OkunanlarCekByKID(kullaniciID);
            Izlenenler1.DataSource = izlenenler.Tables[0];
            Izlenenler1.DataBind();
        }
        public void PuanlarYaz()
        {
            DataSet puanlar = _filmDal.PuanlarCekByKID(kullaniciID);
            Puanlar1.DataSource = puanlar.Tables[0];
            Puanlar1.DataBind();
        }

        protected void btnArkadasEkle_Click(object sender, EventArgs e)
        {
            _uyeDal.ArkadasEkle( Convert.ToInt32(Session["KullaniciID"].ToString()), kullaniciID );
            //Response.Redirect("/kullanici/" + Page.RouteData.Values["KullaniciAdi"]);
            Response.Redirect("/");
        }

        public bool isFriend()
        {
            return _uyeDal.isFriend(Convert.ToInt32(Session["KullaniciID"].ToString()), kullaniciID );
        }

        public string ElementHazirla(string mesaj)
        {
            string elementHazirla = "";
            string elementText = mesaj;
            elementHazirla += "<div class=";
            elementHazirla += "'alert alert-primary' ";
            elementHazirla += " role=";
            elementHazirla += "'alert'";
            elementHazirla += ">" + elementText + "</div>";

            return elementHazirla;
        }

    }
}