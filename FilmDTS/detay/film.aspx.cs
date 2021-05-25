using FilmDTS.siniflar;
using FilmDTS.siniflar.veritabani;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS.detay
{
    public partial class film : System.Web.UI.Page
    {
        FilmDal _filmDal = new FilmDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            FilmCek();
            IncelemelerYaz();
            AlintilarYaz();

            if (!IsPostBack)
            {
                HttpCookie cookieRepData = Request.Cookies["FlushData"];
                if (cookieRepData != null)
                {
                    string strCookieVal = (string)cookieRepData.Value;      //strCookieVal is NOT null!
                    strCookieVal = HttpUtility.UrlDecode(strCookieVal);
                    if (strCookieVal != null)
                    {
                        if (strCookieVal != "")
                        {
                            areaBilgilendirme.InnerHtml = ElementHazirla(strCookieVal);
                        }
                    }
                }
            }


            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int filmID = Convert.ToInt32(Page.RouteData.Values["FilmID"]);

            int isIzlenme = _filmDal.KontrolIzlenmeKayit(kullaniciID, filmID);
            if (isIzlenme > 0)
            {
                areaOkunma.InnerHtml = ElementHazirla("Bu filmi izlediniz.");
            }

            int isPuanlama = _filmDal.KontrolPuanVer(kullaniciID, filmID);
            if (isPuanlama > 0)
            {
                areaPuan.InnerHtml = ElementHazirla("Zaten puanladınız."); ;
            }

            int isInceleme = _filmDal.KontrolIncelemeYaz(kullaniciID, filmID);
            if (isInceleme > 0)
            {
                areaInceleme.InnerHtml = ElementHazirla("Zaten inceleme yazdınız."); ;
                areaIncelemeButon.InnerHtml = "";
            }

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

        public void SetFlushData(string strMsg, int timeSeconds)
        {
            Response.Cookies["FlushData"].Value = HttpUtility.UrlEncode(strMsg);
            Response.Cookies["FlushData"].Expires = DateTime.Now.AddSeconds(timeSeconds);
        }

        public void FilmCek()
        {
            Film film = new Film();
            int FilmID = Convert.ToInt32(Page.RouteData.Values["FilmID"]);
            film = _filmDal.GetByID(FilmID);
            filmAdi.InnerText = film.Ad;
            yonetmen.InnerText = film.YonetmenID.ToString();
            senarist.InnerText = film.SenaristID.ToString();
            izlenme.InnerText = _filmDal.IzlenmeSayisiByFilmID(FilmID).ToString();
            ortalamaPuan.InnerText = _filmDal.OrtalamaPuanByFilmID(FilmID).ToString();
            tanitimBilgisi.InnerText = film.TanitimBilgisi;
            resim.Src = "/fotograf/film/" + film.ResimURL + ".jpg";
        }

        public void IncelemelerYaz()
        {
            DataSet incelemeler = _filmDal.IncelemelerCek(Convert.ToInt32(Page.RouteData.Values["FilmID"]));
            Incelemeler1.DataSource = incelemeler.Tables[0];
            Incelemeler1.DataBind();
        }

        public void AlintilarYaz()
        {
            DataSet alintilar = _filmDal.AlintilarCek(Convert.ToInt32(Page.RouteData.Values["FilmID"]));
            Alintilar1.DataSource = alintilar.Tables[0];
            Alintilar1.DataBind();
        }

        protected void btnPuan_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int FilmID = Convert.ToInt32(Page.RouteData.Values["FilmID"]);
            decimal puan = Convert.ToDecimal(selectPuan.Items[selectPuan.SelectedIndex].Text);
            _filmDal.PuanVer(kullaniciID, FilmID, puan);

            SetFlushData("Kitabı başarıyla puanladınız!", 5);

            Response.Redirect("/film/" + Page.RouteData.Values["FilmID"]);
        }

        protected void btnInceleme_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int FilmID = Convert.ToInt32(Page.RouteData.Values["FilmID"]);
            string inceleme = inputInceleme.Text;
            _filmDal.IncelemeYaz(kullaniciID, FilmID, inceleme);

            SetFlushData("İncelemeniz başarıyla kaydedildi!", 5);

            Response.Redirect("/film/" + Page.RouteData.Values["FilmID"]);

        }

        protected void btnAlinti_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int FilmID = Convert.ToInt32(Page.RouteData.Values["FilmID"]);
            string cumle = inputAlintiCumle.Text;
            _filmDal.AlintiYap(kullaniciID, FilmID, cumle);

            SetFlushData("Alıntınız başarıyla kaydedildi!", 5);

            Response.Redirect("/film/" + Page.RouteData.Values["FilmID"]);
        }

        protected void btnOkundu_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int FilmID = Convert.ToInt32(Page.RouteData.Values["FilmID"]);
            _filmDal.IzlenmeKayit(kullaniciID, FilmID);

            SetFlushData("İzleme kaydınız başarıyla oluşturuldu!", 5);

            Response.Redirect("/film/" + Page.RouteData.Values["FilmID"]);
        }
    }
}