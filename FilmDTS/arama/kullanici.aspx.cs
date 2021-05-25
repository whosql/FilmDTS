using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS.arama
{
    public partial class kullanici : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
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
            DataSet yonetmenler = _uyeDal.Arama(arananKelime);
            Kullanicilar1.DataSource = yonetmenler.Tables[0];
            Kullanicilar1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }
    }
}