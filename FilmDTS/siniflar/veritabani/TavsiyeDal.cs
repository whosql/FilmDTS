using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FilmDTS.siniflar.veritabani
{
    public class FilmPuan
    {
        public int FilmID { get; set; }
        public int Puan { get; set; }
    }
    public class TavsiyeDal
    {
        static SqlConnection _connection = new SqlConnection("Data Source=DESKTOP-50JSIU1\\SQLEXPRESS;Initial Catalog=FilmDTS;Integrated Security=True");
        public List<int> IzlenmisFilmlerByID(int kullaniciID)
        {
            _connection.Open();
            string komutStr = @"SELECT FilmID FROM FilmIzlenme WHERE KullaniciID = @KullaniciID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
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
        public DataSet KullaniciOneri1(int currentUserID, List<int> CurrentUserIzlenenler)
        {
            string izlenenlerIN = "";
            int filmSayisi = CurrentUserIzlenenler.Count();
            foreach (var filmID in CurrentUserIzlenenler)
            {
                filmSayisi--;
                izlenenlerIN += filmID;
                if (filmSayisi > 0)
                {
                    izlenenlerIN += ", ";
                }
            }

            string komutStr = @"SELECT Kullanici.KullaniciAdi, COUNT(FilmID) as OrtakFilmSayisi, Kullanici.KullaniciID FROM FilmIzlenme right join Kullanici on FilmIzlenme.KullaniciID = Kullanici.KullaniciID WHERE FilmID IN (" + izlenenlerIN + ") AND Kullanici.KullaniciID!=@CurrentUserID GROUP BY Kullanici.KullaniciID, Kullanici.KullaniciAdi ORDER BY OrtakFilmSayisi DESC";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@CurrentUserID", currentUserID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet onerinlenler = new DataSet();
            _connection.Open();
            adapter.Fill(onerinlenler);
            _connection.Close();
            return onerinlenler;
        }

        public List<FilmPuan> PuanlanmisFilmlerVePuanlariByID(int kullaniciID)
        {
            _connection.Open();

            string komutStr = @"SELECT FilmID, Puan FROM FilmPuan WHERE KullaniciID = @KullaniciID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataReader reader = komut.ExecuteReader();
            List<FilmPuan> FilmlerVePuanlar = new List<FilmPuan>();

            while (reader.Read())
            {
                FilmPuan PairFilmPuan = new FilmPuan
                {
                    FilmID = Convert.ToInt32(reader["FilmID"]),
                    Puan = Convert.ToInt32(reader["Puan"])
                };

                FilmlerVePuanlar.Add(PairFilmPuan);
            }

            reader.Close();
            _connection.Close();
            return FilmlerVePuanlar;
        }
        public DataSet KullaniciOneri2(int currentUserID, List<FilmPuan> FilmVePuanlar)
        {
            _connection.Open();
            DataSet onerinlenler = new DataSet();
            foreach (var PairFilmPuan in FilmVePuanlar)
            {
                string komutStr = @"SELECT Kullanici.KullaniciAdi, FilmPuan.FilmID, FilmPuan.Puan, Film.Ad, FilmPuan.KullaniciID FROM FilmPuan RIGHT JOIN Kullanici on FilmPuan.KullaniciID = Kullanici.KullaniciID RIGHT JOIN Film on FilmPuan.FilmID = Film.FilmID WHERE FilmPuan.FilmID=@FilmID AND FilmPuan.Puan=@Puan AND FilmPuan.KullaniciID!=@CurrentUserID GROUP BY Kullanici.KullaniciAdi, FilmPuan.FilmID, FilmPuan.Puan, Film.Ad, FilmPuan.KullaniciID";

                SqlCommand komut = new SqlCommand(komutStr, _connection);
                komut.Parameters.AddWithValue("@FilmID", PairFilmPuan.FilmID);
                komut.Parameters.AddWithValue("@Puan", PairFilmPuan.Puan);
                komut.Parameters.AddWithValue("@CurrentUserID", currentUserID);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komut;
                adapter.Fill(onerinlenler);
            }

            _connection.Close();
            return onerinlenler;
        }

        public DataSet FilmOneri1(List<int> CurrentUserIzlenenler, List<int> OnerilenKullaniciIDs)
        {
            string izlenenlerNotIN = "";
            int filmSayisi = CurrentUserIzlenenler.Count();
            foreach (var FilmID in CurrentUserIzlenenler)
            {
                filmSayisi--;
                izlenenlerNotIN += FilmID;
                if (filmSayisi > 0)
                {
                    izlenenlerNotIN += ", ";
                }
            }

            string KullaniciIN = "";
            int kullaniciSayisi = OnerilenKullaniciIDs.Count();
            foreach (var xkullaniciID in OnerilenKullaniciIDs)
            {
                kullaniciSayisi--;
                KullaniciIN += xkullaniciID;
                if (kullaniciSayisi > 0)
                {
                    KullaniciIN += ", ";
                }
            }

            string komutStr = @"SELECT TOP(10) NEWID() as RandomID, Film.Ad, Film.FilmID FROM FilmIzlenme right join Film on FilmIzlenme.FilmID = Film.FilmID WHERE FilmIzlenme.FilmID NOT IN (" + izlenenlerNotIN + ") AND FilmIzlenme.KullaniciID IN (" + KullaniciIN + ")" + " GROUP BY Film.Ad, Film.FilmID ORDER BY RandomID";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet onerilenFilm = new DataSet();
            _connection.Open();
            adapter.Fill(onerilenFilm);
            _connection.Close();

            return onerilenFilm;
        }

        public DataSet FilmOneri2(List<int> CurrentUserIzlenenler,  List<int> ArkadasIDListesi)
        {
            string CurrUserIzlenenlerNotIN = "";
            int filmSayisi = CurrentUserIzlenenler.Count();
            foreach (var FilmID in CurrentUserIzlenenler)
            {
                filmSayisi--;
                CurrUserIzlenenlerNotIN += FilmID;
                if (filmSayisi > 0)
                {
                    CurrUserIzlenenlerNotIN += ", ";
                }
            }

            string ArkadasIDleriIN = "";
            int kullaniciSayisi = ArkadasIDListesi.Count();
            foreach (var xkullaniciID in ArkadasIDListesi)
            {
                kullaniciSayisi--;
                ArkadasIDleriIN += xkullaniciID;
                if (kullaniciSayisi > 0)
                {
                    ArkadasIDleriIN += ", ";
                }
            }

            string komutStr = @"SELECT TOP(5) COUNT(FilmIzlenme.FilmID) as IzlenmeSayisi, Film.Ad, Film.FilmID FROM FilmIzlenme INNER join Film on FilmIzlenme.FilmID = Film.FilmID WHERE FilmIzlenme.FilmID NOT IN (" + CurrUserIzlenenlerNotIN + ") AND FilmIzlenme.KullaniciID IN (" + ArkadasIDleriIN + ")" + " GROUP BY Film.Ad, Film.FilmID ORDER BY IzlenmeSayisi DESC";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet onerilenFilm = new DataSet();
            _connection.Open();
            adapter.Fill(onerilenFilm);
            _connection.Close();

            return onerilenFilm;
        }

        public DataSet FilmOneri3(int currentUserID, int CurrUserEnCokIzledigiKategoriID, List<int> KategorideIzledigiFilmlerByKullaniciID )
        {

            string CurrUserKategorideIzlenenlerNotIN = "";
            int filmSayisi = KategorideIzledigiFilmlerByKullaniciID.Count();
            foreach (var FilmID in KategorideIzledigiFilmlerByKullaniciID)
            {
                filmSayisi--;
                CurrUserKategorideIzlenenlerNotIN += FilmID;
                if (filmSayisi > 0)
                {
                    CurrUserKategorideIzlenenlerNotIN += ", ";
                }
            }


            string komutStr = @"SELECT TOP(5) Film.FilmID, Film.Ad, CAST(AVG(FilmPuan.Puan) AS DECIMAL(10,2)) as OrtalamaPuan FROM FilmPuan RIGHT JOIN Film on FilmPuan.FilmID = Film.FilmID WHERE Film.KategoriID = @CurrUserEnCokIzledigiKategoriID AND EXISTS (SELECT TOP(5) Film.Ad, Film.FilmID FROM FilmIzlenme INNER join Film on FilmIzlenme.FilmID = Film.FilmID WHERE FilmIzlenme.FilmID NOT IN (" + CurrUserKategorideIzlenenlerNotIN + ") AND FilmIzlenme.KullaniciID != @CurrentUserID GROUP BY Film.Ad, Film.FilmID) GROUP BY Film.FilmID, Film.Ad HAVING CAST(AVG(FilmPuan.Puan) AS DECIMAL(10,2)) > 0 ORDER BY OrtalamaPuan desc";

            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@CurrentUserID", currentUserID);
            komut.Parameters.AddWithValue("@CurrUserEnCokIzledigiKategoriID", CurrUserEnCokIzledigiKategoriID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet onerilenFilmler = new DataSet();
            _connection.Open();
            adapter.Fill(onerilenFilmler);
            _connection.Close();

            return onerilenFilmler;

        }


    }
}