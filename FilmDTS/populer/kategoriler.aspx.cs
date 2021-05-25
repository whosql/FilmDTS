using FilmDTS.siniflar.veritabani;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS.populer
{
    public partial class kategoriler : System.Web.UI.Page
    {
        FilmDal _filmDal = new FilmDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            DataSet kategoriler = _filmDal.PopulerKategorilerCek();
            Kategoriler1.DataSource = kategoriler.Tables[0];
            Kategoriler1.DataBind();
        }
    }
}