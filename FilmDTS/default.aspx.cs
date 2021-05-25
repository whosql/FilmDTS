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
    public partial class _default : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        TavsiyeDal _tavsiyeDal = new TavsiyeDal();
        FilmDal _filmDal = new FilmDal();
        List<int> _onerilenKullanicilarIDs = new List<int>();
        int kullaniciID;
        List<int> currUserIzledikleri;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Convert.ToBoolean(Session["isLogged"]))
            {
                kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Session["KullaniciAdi"].ToString());
                currUserIzledikleri = _tavsiyeDal.IzlenmisFilmlerByID(kullaniciID);

                KullaniciOneri1();
                KullaniciOneri2();
                FilmOneri1();
                FilmOneri2();
                FilmOneri3();
            }

        }
        public void KullaniciOneri1()
        {
            List<int> okuduklari = _tavsiyeDal.IzlenmisFilmlerByID(kullaniciID);
            if (!(okuduklari.Count == 0))
            {
                DataSet onerilenler1 = _tavsiyeDal.KullaniciOneri1(kullaniciID, okuduklari);
                K_Oneri1.DataSource = onerilenler1.Tables[0];
                K_Oneri1.DataBind();
                foreach (DataRow row in onerilenler1.Tables[0].Rows)
                {
                    int currentKullanici = Convert.ToInt32(row["KullaniciID"]);
                    _onerilenKullanicilarIDs.Add(currentKullanici);
                }
            }


        }
        public void KullaniciOneri2()
        {

            List<FilmPuan> puanladiklari = _tavsiyeDal.PuanlanmisFilmlerVePuanlariByID(kullaniciID);

            if (!(puanladiklari.Count == 0)){
                DataSet onerilenler2 = _tavsiyeDal.KullaniciOneri2(kullaniciID, puanladiklari);
                onerilenler2.Tables[0].DefaultView.Sort = "KullaniciAdi ASC";
                K_Oneri2.DataSource = onerilenler2.Tables[0];
                K_Oneri2.DataBind();
                foreach (DataRow row in onerilenler2.Tables[0].Rows)
                {
                    int currentKullanici = Convert.ToInt32(row["KullaniciID"]);
                    _onerilenKullanicilarIDs.Add(currentKullanici);
                }
            }
        }


        public void FilmOneri1()
        {

            if ( !(currUserIzledikleri.Count == 0) ){
                DataSet onerilenFilmler = _tavsiyeDal.FilmOneri1(currUserIzledikleri, _onerilenKullanicilarIDs);
                F_Oneri1.DataSource = onerilenFilmler.Tables[0];
                F_Oneri1.DataBind();
            }

        }

        public void FilmOneri2()
        {
            List<int> arkadasListesi = _uyeDal.GetArkadasListesiByID(kullaniciID);

            if (!(arkadasListesi.Count == 0))
            {
                DataSet onerilenlerFilmler2 = _tavsiyeDal.FilmOneri2(currUserIzledikleri, arkadasListesi);
                //onerilenler2.Tables[0].DefaultView.Sort = "KullaniciAdi ASC";
                F_Oneri2.DataSource = onerilenlerFilmler2.Tables[0];
                F_Oneri2.DataBind();
            }

        }

        public void FilmOneri3()
        {
            int CurrUserEnCokIzledigiKategoriID = _filmDal.EnCokIzlenenKategoriByKullaniciID(kullaniciID);

            if (CurrUserEnCokIzledigiKategoriID > 0)
            {
                List<int> KategorideIzledigiFilmlerByKullaniciID = _filmDal.KategorideIzledigiFilmlerByKullaniciID(kullaniciID, CurrUserEnCokIzledigiKategoriID);

                DataSet onerilenlerFilmler3 = _tavsiyeDal.FilmOneri3(kullaniciID, CurrUserEnCokIzledigiKategoriID, KategorideIzledigiFilmlerByKullaniciID);
                F_Oneri3.DataSource = onerilenlerFilmler3.Tables[0];
                F_Oneri3.DataBind();

            }

        }

    }
}