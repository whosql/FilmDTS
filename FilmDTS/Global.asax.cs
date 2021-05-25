using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace FilmDTS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            SaveRoutes(RouteTable.Routes);
        }

        void SaveRoutes(RouteCollection routes)
        {
            // Yönetim
            routes.MapPageRoute("yonetimSayfalar",
                "yonetim/{Sayfa}",
                "~/yonetim/{Sayfa}.aspx");

            // Kullanıcı
            routes.MapPageRoute("TekParcaURI",
                "{uri1}/",
                "~/{uri1}.aspx");

            // Sayfalar
            routes.MapPageRoute("populerSayfalar",
                "populer/{Sayfa}",
                "~/populer/{Sayfa}.aspx");
            routes.MapPageRoute("aramaSayfalar",
                "arama/{Sayfa}",
                "~/arama/{Sayfa}.aspx");

            // Detay Sayfaları
            routes.MapPageRoute("KullaniciProfil",
                "kullanici/{KullaniciAdi}",
                "~/profil.aspx");
            routes.MapPageRoute("Film",
                "film/{FilmID}",
                "~/detay/film.aspx");
            routes.MapPageRoute("Senarist",
                "senarist/{SenaristID}",
                "~/detay/senarist.aspx");
            routes.MapPageRoute("Yonetmen",
                "yonetmen/{YonetmenID}",
                "~/detay/yonetmen.aspx");


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}