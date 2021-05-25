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
    public partial class senarist : System.Web.UI.Page
    {
        SenaristDal _senaristDal = new SenaristDal();
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
            DataSet senaristler = _senaristDal.Arama(arananKelime);
            Senaristler1.DataSource = senaristler.Tables[0];
            Senaristler1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }
    }
}