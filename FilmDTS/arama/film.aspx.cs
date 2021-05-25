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
    public partial class film : System.Web.UI.Page
    {
        FilmDal _filmDal = new FilmDal();
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
            DataSet filmler = _filmDal.Arama(arananKelime);
            Filmler1.DataSource = filmler.Tables[0];
            Filmler1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }

    }
}