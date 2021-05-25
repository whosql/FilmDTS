using FilmDTS.siniflar.veritabani;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS.arama
{
    public partial class yonetmen : System.Web.UI.Page
    {
        YonetmenDal _yonetmenDal = new YonetmenDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
            else
            {
                string Aranan = inputArananKelime.Text;
                AramaSonuclari(Aranan);
            }
        }

        public void AramaSonuclari(string arananKelime)
        {
            DataSet yonetmenler = _yonetmenDal.Arama(arananKelime);
            Yonetmenler1.DataSource = yonetmenler.Tables[0];
            Yonetmenler1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }
    }
}