using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FilmDTS.siniflar.veritabani
{
    public class FilmDal
    {
        static SqlConnection _connection = new SqlConnection("Data Source=DESKTOP-50JSIU1\\SQLEXPRESS;Initial Catalog=FilmDTS;Integrated Security=True");

        //public List<Film> GetAll()
        //{
        //    _connection.Open();
        //    SqlCommand command = new SqlCommand("Select * from Film", _connection);

        //    SqlDataReader reader = command.ExecuteReader();

        //    List<Film> Film = new List<Film>();

        //    while (reader.Read())
        //    {
        //        Film Film = new Film
        //        {
        //            FilmID = Convert.ToInt32(reader["FilmID"]),
        //            yonetmenID = Convert.ToInt32(reader["yonetmenID"]),
        //            Ad = reader["Ad"].ToString(),
        //            Yayinevi = reader["Yayinevi"].ToString(),
        //            TanitimBilgisi = reader["TanitimBilgisi"].ToString(),
        //            ResimURL = reader["ResimURL"].ToString()
        //        };
        //        Film.Add(Film);
        //    }

        //    reader.Close();
        //    _connection.Close();
        //    return Film;
        //}
        //public Film GetByID(int FilmID)
        //{
        //    _connection.Open();
        //    SqlCommand command = new SqlCommand("Select * from Film where FilmID = @FilmID", _connection);
        //    command.Parameters.AddWithValue("@FilmID", FilmID);

        //    SqlDataReader reader = command.ExecuteReader();

        //    List<Film> Film = new List<Film>();

        //    while (reader.Read())
        //    {
        //        Film Film = new Film
        //        {
        //            FilmID = Convert.ToInt32(reader["FilmID"]),
        //            yonetmenID = Convert.ToInt32(reader["yonetmenID"]),
        //            Ad = reader["Ad"].ToString(),
        //            Yayinevi = reader["Yayinevi"].ToString(),
        //            TanitimBilgisi = reader["TanitimBilgisi"].ToString(),
        //            ResimURL = reader["ResimURL"].ToString()
        //        };
        //        Film.Add(Film);
        //    }

        //    reader.Close();
        //    _connection.Close();
        //    return Film[0];
        //}

        public int EnCokIzlenenKategoriByKullaniciID(int kullaniciID)
        {
            string komutStr = "SELECT TOP(1) Film.KategoriID, COUNT(Film.KategoriID) as IzlenmeSayisi FROM FilmIzlenme INNER JOIN Film ON FilmIzlenme.FilmID = Film.FilmID WHERE KullaniciID = @KullaniciID GROUP BY Film.KategoriID ORDER BY IzlenmeSayisi DESC";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            int EnCokIzledigiKategoriID = Convert.ToInt32(Film.Tables[0].Rows[0]["KategoriID"]);
            return EnCokIzledigiKategoriID;
        }

        public List<int> KategorideIzledigiFilmlerByKullaniciID(int kullaniciID, int arananKategoriID)
        {
            _connection.Open();
            string komutStr = @"SELECT FilmIzlenme.FilmID FROM FilmIzlenme INNER JOIN Film ON FilmIzlenme.FilmID = Film.FilmID WHERE FilmIzlenme.KullaniciID = @KullaniciID AND Film.KategoriID = @arananKategoriID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            komut.Parameters.AddWithValue("@arananKategoriID", arananKategoriID);
            SqlDataReader reader = komut.ExecuteReader();
            List<Int32> IzlenmisFilmler = new List<Int32>();

            while (reader.Read())
            {
                int filmID = Convert.ToInt32(reader["FilmID"]);
                IzlenmisFilmler.Add(filmID);
            }

            reader.Close();
            _connection.Close();
            return IzlenmisFilmler;
        }


        public int OrtalamaPuanByFilmID(int FilmID)
        {
            string komutStr = "SELECT AVG(FilmPuan.Puan) as OrtalamaPuan FROM FilmPuan WHERE FilmID = @FilmID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@FilmID", FilmID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            int FilmIzlenme = 0;

            if (Film.Tables[0] != null)
            {
                if (Film.Tables[0].Rows[0]["OrtalamaPuan"] != DBNull.Value)
                {
                    FilmIzlenme = Convert.ToInt32(Film.Tables[0].Rows[0]["OrtalamaPuan"]);
                }
            }
            return FilmIzlenme;
        }
        public int IzlenmeSayisiByFilmID(int FilmID)
        {
            string komutStr = "SELECT COUNT(FilmID) as IzlenmeSayisi FROM FilmIzlenme WHERE FilmID = @FilmID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@FilmID", FilmID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            int FilmOrtPuan = Convert.ToInt32(Film.Tables[0].Rows[0]["IzlenmeSayisi"]);
            return FilmOrtPuan;
        }

        public Film GetByID(int filmID)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select * from Film where FilmID = @FilmID", _connection);
            command.Parameters.AddWithValue("@FilmID", filmID);

            SqlDataReader reader = command.ExecuteReader();

            List<Film> fimler = new List<Film>();

            while (reader.Read())
            {
                Film film = new Film
                {
                    id = Convert.ToInt32(reader["FilmID"]),
                    Ad = reader["Ad"].ToString(),
                    YonetmenID = Convert.ToInt32(reader["YonetmenID"]),
                    SenaristID = Convert.ToInt32(reader["SenaristID"]),
                    VizyonaGirisTarihi = Convert.ToInt32(reader["VizyonaGirisTarihi"]),
                    TanitimBilgisi = reader["TanitimBilgisi"].ToString(),
                    ResimURL = reader["ResimURL"].ToString()
                };
                fimler.Add(film);
            }

            reader.Close();
            _connection.Close();
            return fimler[0];
        }

        public DataSet GetByYonetmenID(int yonetmenID)
        {
            SqlCommand komut = new SqlCommand("select * from Film where YonetmenID=@yonetmenID", _connection);
            komut.Parameters.AddWithValue("@yonetmenID", yonetmenID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            return Film;
        }

        public DataSet GetBySenaristID(int senaristID)
        {
            SqlCommand komut = new SqlCommand("select * from Film where SenaristID=@SenaristID", _connection);
            komut.Parameters.AddWithValue("@SenaristID", senaristID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            return Film;
        }

        public DataSet PopulerKategorilerCek()
        {
            string komutStr = @"SELECT COUNT(Film.KategoriID) as KategoriIzlenme, KategoriAdi, Kategori.KategoriID FROM FilmIzlenme INNER JOIN Film on FilmIzlenme.FilmID = Film.FilmID INNER JOIN Kategori on Film.KategoriID = Kategori.KategoriID GROUP BY Kategori.KategoriID, KategoriAdi HAVING COUNT(Film.KategoriID) > 0 ORDER BY KategoriIzlenme desc";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet kategoriler = new DataSet();
            _connection.Open();
            adapter.Fill(kategoriler);
            _connection.Close();
            return kategoriler;
        }

        public DataSet FilmCek()
        {
            SqlCommand komut = new SqlCommand("select * from Film", _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            return Film;
        }

        public DataSet FilmlerCek()
        {
            SqlCommand komut = new SqlCommand("select * from Film", _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet filmler = new DataSet();
            _connection.Open();
            adapter.Fill(filmler);
            _connection.Close();
            return filmler;
        }

        public DataSet PopulerFilmlerCek()
        {
            // ref
            string komutStr = "SELECT Film.FilmID, Film.Ad, COUNT(FilmIzlenme.FilmID) as IzlenmeSayisi FROM FilmIzlenme RIGHT JOIN Film on FilmIzlenme.FilmID = Film.FilmID GROUP BY Film.FilmID, Film.Ad HAVING COUNT(FilmIzlenme.FilmID) > 0 ORDER BY IzlenmeSayisi desc";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            return Film;
        }
        public DataSet YuksekPuanliFilmlerCek()
        {
            string komutStr = "SELECT Film.FilmID, Film.Ad, CAST(AVG(FilmPuan.Puan) AS DECIMAL(10,2)) as OrtalamaPuan FROM FilmPuan RIGHT JOIN Film on FilmPuan.FilmID = Film.FilmID GROUP BY Film.FilmID, Film.Ad HAVING CAST(AVG(FilmPuan.Puan) AS DECIMAL(10,2)) > 0 ORDER BY OrtalamaPuan desc";


            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            return Film;
        }

        public void Ekle(Film Film)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "Insert into Film (Ad, Sure, KategoriID, SenaristID, YonetmenID, VizyonaGirisTarihi, ResimURL, TanitimBilgisi) values (@Ad, @Sure, @KategoriID, @SenaristID, @YonetmenID, @VizyonaGirisTarihi, @ResimURL, @TanitimBilgisi)", _connection);
            command.Parameters.AddWithValue("@Ad", Film.Ad);
            command.Parameters.AddWithValue("@Sure", Film.Sure);
            command.Parameters.AddWithValue("@KategoriID", Film.KategoriID);
            command.Parameters.AddWithValue("@SenaristID", Film.SenaristID);
            command.Parameters.AddWithValue("@YonetmenID", Film.YonetmenID);
            command.Parameters.AddWithValue("@VizyonaGirisTarihi", Film.VizyonaGirisTarihi);
            command.Parameters.AddWithValue("@ResimURL", Film.ResimURL);
            command.Parameters.AddWithValue("@TanitimBilgisi", Film.TanitimBilgisi);


            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Sil(int FilmID)
        {
            string commandStr = "DELETE FROM Film WHERE FilmID=@FilmID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@FilmID", FilmID);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void Guncelle(Film Film)
        {
            string commandStr = "UPDATE Film SET Ad=@Ad, Sure=@Sure, KategoriID=@KategoriID, SenaristID=@SenaristID, YonetmenID=@YonetmenID,VizyonaGirisTarihi=@VizyonaGirisTarihi, ResimURL=@ResimURL, TanitimBilgisi=@TanitimBilgisi WHERE FilmID=@FilmID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@FilmID", Film.id);
            command.Parameters.AddWithValue("@Ad", Film.Ad);
            command.Parameters.AddWithValue("@Sure", Film.Sure);
            command.Parameters.AddWithValue("@SenaristID", Film.SenaristID);
            command.Parameters.AddWithValue("@YonetmenID", Film.YonetmenID);
            command.Parameters.AddWithValue("@KategoriID", Film.KategoriID);
            command.Parameters.AddWithValue("@VizyonaGirisTarihi", Film.VizyonaGirisTarihi);
            command.Parameters.AddWithValue("@ResimURL", Film.ResimURL);
            command.Parameters.AddWithValue("@TanitimBilgisi", Film.TanitimBilgisi);


            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select FilmID, Ad from Film where Ad like '%' + @arananKelime + '%' ";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet Film = new DataSet();
            _connection.Open();
            adapter.Fill(Film);
            _connection.Close();
            return Film;
        }

        public void PuanVer(int kullaniciID, int FilmID, decimal puan)
        {
            string commandStr = "INSERT INTO FilmPuan(KullaniciID, FilmID, Puan) values(@KullaniciID, @FilmID, @Puan)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);
            command.Parameters.AddWithValue("@Puan", puan);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolPuanVer(int kullaniciID, int FilmID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM FilmPuan WHERE KullaniciID=@KullaniciID AND FilmID=@FilmID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }
        public void IncelemeYaz(int kullaniciID, int FilmID, string inceleme)
        {
            string commandStr = "INSERT INTO FilmInceleme(KullaniciID, FilmID, Inceleme) values(@KullaniciID, @FilmID, @Inceleme)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);
            command.Parameters.AddWithValue("@Inceleme", inceleme);

            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolIncelemeYaz(int kullaniciID, int FilmID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM FilmInceleme WHERE KullaniciID=@KullaniciID AND FilmID=@FilmID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }

        public void AlintiYap(int kullaniciID, int FilmID, string cumle)
        {
            string commandStr = "INSERT INTO FilmAlinti(KullaniciID, FilmID, Cumle) values(@KullaniciID, @FilmID, @Cumle)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);
            command.Parameters.AddWithValue("@Cumle", cumle);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolAlintiYap(int kullaniciID, int FilmID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM FilmAlinti WHERE KullaniciID=@KullaniciID AND FilmID=@FilmID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }
        public void IzlenmeKayit(int kullaniciID, int FilmID)
        {
            string commandStr = "INSERT INTO FilmIzlenme(KullaniciID, FilmID) values(@KullaniciID, @FilmID)";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public int KontrolIzlenmeKayit(int kullaniciID, int FilmID)
        {
            string commandStr = "SELECT COUNT(*) as AffectedRows FROM FilmIzlenme WHERE KullaniciID=@KullaniciID AND FilmID=@FilmID";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            command.Parameters.AddWithValue("@FilmID", FilmID);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            int Kontrol = 0;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = Convert.ToInt32(sonuc.Tables[0].Rows[0]["AffectedRows"]);
            }

            return Kontrol;
        }
        public DataSet IncelemelerCek(int FilmID)
        {
            SqlCommand komut = new SqlCommand("select * from FilmInceleme RIGHT JOIN Kullanici ON FilmInceleme.KullaniciID = Kullanici.KullaniciID where FilmID=@FilmID", _connection);
            komut.Parameters.AddWithValue("@FilmID", FilmID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet incelemeler = new DataSet();
            _connection.Open();
            adapter.Fill(incelemeler);
            _connection.Close();
            return incelemeler;
        }
        public DataSet IncelemelerCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select * from FilmInceleme RIGHT JOIN Film on FilmInceleme.FilmID = Film.FilmID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet incelemeler = new DataSet();
            _connection.Open();
            adapter.Fill(incelemeler);
            _connection.Close();
            return incelemeler;
        }

        public DataSet AlintilarCek(int FilmID)
        {
            SqlCommand komut = new SqlCommand("select * from FilmAlinti RIGHT JOIN Kullanici ON FilmAlinti.KullaniciID = Kullanici.KullaniciID where FilmID=@FilmID", _connection);
            komut.Parameters.AddWithValue("@FilmID", FilmID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet alintilar = new DataSet();
            _connection.Open();
            adapter.Fill(alintilar);
            _connection.Close();
            return alintilar;
        }
        public DataSet AlintilarCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select * from FilmAlinti RIGHT JOIN Film on FilmAlinti.FilmID = Film.FilmID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet alintilar = new DataSet();
            _connection.Open();
            adapter.Fill(alintilar);
            _connection.Close();
            return alintilar;
        }
        public DataSet OkunanlarCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select Film.FilmID, Film.Ad from FilmIzlenme RIGHT JOIN Film on FilmIzlenme.FilmID = Film.FilmID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet okunanlar = new DataSet();
            _connection.Open();
            adapter.Fill(okunanlar);
            _connection.Close();
            return okunanlar;
        }
        public DataSet PuanlarCekByKID(int kullaniciID)
        {
            SqlCommand komut = new SqlCommand("select * from FilmPuan RIGHT JOIN Film on FilmPuan.FilmID = Film.FilmID where KullaniciID=@KullaniciID", _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet puanlar = new DataSet();
            _connection.Open();
            adapter.Fill(puanlar);
            _connection.Close();
            return puanlar;
        }


    }
}