using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDTS.siniflar
{
    public class Film
    {
        public int id { get; set; }
        public string Ad { get; set; }
        public int Sure { get; set; }
        public int KategoriID { get; set; }
        public int SenaristID { get; set; }
        public int YonetmenID { get; set; }
        public int VizyonaGirisTarihi { get; set; }
        public string ResimURL { get; set; }

        public string TanitimBilgisi { get; set; }


    }
}