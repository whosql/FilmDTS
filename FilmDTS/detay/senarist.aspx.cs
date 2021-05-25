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
    public partial class senarist : System.Web.UI.Page
    {
        SenaristDal _senaristDal = new SenaristDal();
        FilmDal _filmDal = new FilmDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            SenaristCek();
            FilmlerCek();
        }

        public void SenaristCek()
        {
            DataSet yazar = _senaristDal.GetByID(Convert.ToInt32(Page.RouteData.Values["SenaristID"]));
            Senarist1.DataSource = yazar.Tables[0];
            Senarist1.DataBind();
        }

        public void FilmlerCek()
        {
            DataSet filmler = _filmDal.GetBySenaristID(Convert.ToInt32(Page.RouteData.Values["SenaristID"]));
            Filmler1.DataSource = filmler.Tables[0];
            Filmler1.DataBind();
        }
    }
}