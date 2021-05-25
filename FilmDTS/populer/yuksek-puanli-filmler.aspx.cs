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
    public partial class yuksek_puanli_filmler : System.Web.UI.Page
    {
        FilmDal _kitapDal = new FilmDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            DataSet kitaplar = _kitapDal.YuksekPuanliFilmlerCek();
            Filmler1.DataSource = kitaplar.Tables[0];
            Filmler1.DataBind();
        }
    }

}