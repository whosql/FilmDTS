using FilmDTS.siniflar;
using FilmDTS.siniflar.veritabani;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FilmDTS.yonetim
{
    public partial class fimler : System.Web.UI.Page
    {
        FilmDal _filmDal = new FilmDal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }

        }

        public void BindGrid()
        {
            DataSet filmler = _filmDal.FilmlerCek();
            Filmler1.DataSource = filmler.Tables[0];
            Filmler1.DataBind();
        }

        protected void Filmler1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Filmler1.EditIndex = e.NewEditIndex;

            BindGrid();
        }

        protected void Filmler1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)

        {

            Filmler1.EditIndex = -1;

            BindGrid();

        }


        protected void Filmler1_RowDeleting(object sender, GridViewDeleteEventArgs e)

        {

            int FilmID = (Int32)Filmler1.DataKeys[e.RowIndex].Value;
            _filmDal.Sil(FilmID);

        }

        protected void Filmler1_RowUpdating(object sender, GridViewUpdateEventArgs e)

        {
            int FilmID = int.Parse(Filmler1.DataKeys[e.RowIndex].Value.ToString());
            string Ad = ((TextBox)Filmler1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            int Sure = int.Parse(((TextBox)Filmler1.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            int KategoriID = int.Parse(((TextBox)Filmler1.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
            int SenaristID = int.Parse(((TextBox)Filmler1.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
            int YonetmenID = int.Parse(((TextBox)Filmler1.Rows[e.RowIndex].Cells[5].Controls[0]).Text);
            int VizyonaGirisTarihi = int.Parse(((TextBox)Filmler1.Rows[e.RowIndex].Cells[6].Controls[0]).Text);
            string ResimURL = ((TextBox)Filmler1.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            string TanitimBilgisi = ((TextBox)Filmler1.Rows[e.RowIndex].Cells[8].Controls[0]).Text;


            Film film = new Film
            {
                Ad = Ad,
                id = FilmID,
                Sure = Sure,
                KategoriID = KategoriID,
                SenaristID = SenaristID,
                YonetmenID = YonetmenID,
                VizyonaGirisTarihi = VizyonaGirisTarihi,
                ResimURL = ResimURL,
                TanitimBilgisi = TanitimBilgisi,

            };

            _filmDal.Guncelle(film);

            Filmler1.EditIndex = -1;

            BindGrid();
        }
    }
}