using FilmDTS.siniflar;
using FilmDTS.siniflar.veritabani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS.yonetim
{
    public partial class filmEkle : System.Web.UI.Page
    {
        FilmDal _filmDal = new FilmDal();
        YonetmenDal _yonetmenDal = new YonetmenDal();
        SenaristDal _senaristDal = new SenaristDal();

        protected void Page_Load(object sender, EventArgs e)
        {
            yonetmenCek();
            senaristCek();

        }

        protected void yonetmenCek()
        {
            List<Yonetmen> yonetmenler = _yonetmenDal.GetAll();

            foreach (Yonetmen yonetmen in yonetmenler)
            {
                string yonetmenAdSoyad = yonetmen.AdSoyad;
                ListItem yonetmenX = new ListItem(yonetmenAdSoyad, Convert.ToString(yonetmen.YonetmenID));
                selectYonetmenID.Items.Add(yonetmenX);
            }
        }

        protected void senaristCek()
        {
            List<Senarist> senaristler = _senaristDal.GetAll();

            foreach (Senarist senarist in senaristler)
            {
                string senaristAdSoyad = senarist.AdSoyad;
                ListItem senaristX = new ListItem(senaristAdSoyad, Convert.ToString(senarist.SenaristID));
                selectSenaristID.Items.Add(senaristX);
            }
        }

        protected void kitapEkleBtn_Click(object sender, EventArgs e)
        {
            _filmDal.Ekle(new Film
            {
                Ad = inputAd.Text,
                Sure = Convert.ToInt32(inputSure.Text),
                KategoriID = Convert.ToInt32(inputKategoriID.Text),
                YonetmenID = Convert.ToInt32(selectYonetmenID.SelectedValue),
                SenaristID = Convert.ToInt32(selectSenaristID.SelectedValue),
                VizyonaGirisTarihi = Convert.ToInt32(inputVizyonaGirisTarihi.Text),
                TanitimBilgisi = inputTanitimBilgisi.Text,
                ResimURL = inputResimURL.Text
            });

            string filename = inputResimURL.Text + ".jpg";
            uploadResim.SaveAs(Server.MapPath("../fotograf/film/") + filename);

            Response.Redirect("/yonetim/panel");
        }
    }
}