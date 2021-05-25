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
    public partial class yonetmen : System.Web.UI.Page
    {
        YonetmenDal _yonetmenDal = new YonetmenDal();
        FilmDal _filmDal = new FilmDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            YonetmenCek();
            FilmlerCek();
        }

        public void YonetmenCek()
        {
            DataSet yonetmen = _yonetmenDal.GetByID(Convert.ToInt32(Page.RouteData.Values["YonetmenID"]));
            Yonetmen1.DataSource = yonetmen.Tables[0];
            Yonetmen1.DataBind();
        }

        public void FilmlerCek()
        {
            DataSet filmler = _filmDal.GetByYonetmenID(Convert.ToInt32(Page.RouteData.Values["YonetmenID"]));
            Filmler1.DataSource = filmler.Tables[0];
            Filmler1.DataBind();
        }
    }
}