using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FilmDTS
{
    public class UyeDal
    {
        SqlConnection _connection = new SqlConnection("Data Source=DESKTOP-50JSIU1\\SQLEXPRESS;Initial Catalog=FilmDTS;Integrated Security=True");

        public void Ekle(Uye uye)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "Insert into Kullanici (isim, Soyisim, Cinsiyet, DogumTarihi, KullaniciAdi, Sifre) values (@isim,@Soyisim,@Cinsiyet,@DogumTarihi,@KullaniciAdi,@Sifre)", _connection);
            command.Parameters.AddWithValue("@isim", uye.Isim);
            command.Parameters.AddWithValue("@Soyisim", uye.Soyisim);
            command.Parameters.AddWithValue("@Cinsiyet", uye.Cinsiyet);
            command.Parameters.AddWithValue("@DogumTarihi", uye.DogumTarihi);
            command.Parameters.AddWithValue("@KullaniciAdi", uye.KullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", uye.Sifre);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public bool GirisYap(string KullaniciAdi, string Sifre)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "select * from Kullanici where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
            command.Parameters.AddWithValue("@Sifre", Sifre);

            SqlDataAdapter adaptor = new SqlDataAdapter(command);
            DataSet sonucDS = new DataSet();
            adaptor.Fill(sonucDS);
            _connection.Close();

            bool sonuc = false;
            if (sonucDS.Tables[0].Rows.Count > 0)
                sonuc = true;

            return sonuc;
        }

        public int GetIDbyKullaniciAdi(string kullaniciAdi)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select KullaniciID from Kullanici where KullaniciAdi = @KullaniciAdi", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

            SqlDataReader reader = command.ExecuteReader();

            List<Uye> uyeler = new List<Uye>();

            while (reader.Read())
            {
                Uye uye = new Uye
                {
                    KullaniciID = Convert.ToInt32(reader["KullaniciID"])
                };
                uyeler.Add(uye);
            }

            reader.Close();
            _connection.Close();
            return uyeler[0].KullaniciID;
        }

        public List<int> GetArkadasListesiByID(int kullaniciID)
        {
            _connection.Open();
            string komutStr = @"SELECT ArkadasID FROM KullaniciArkadas WHERE KullaniciID = @KullaniciID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlDataReader reader = komut.ExecuteReader();
            List<Int32> arkadasListesi = new List<Int32>();

            while (reader.Read())
            {
                int arkadasID = Convert.ToInt32(reader["ArkadasID"]);
                arkadasListesi.Add(arkadasID);
            }

            reader.Close();
            _connection.Close();
            return arkadasListesi;
        }

        public Uye GetByKAdi(string kullaniciAdi)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select KullaniciID, isim, Soyisim from Kullanici where KullaniciAdi = @KullaniciAdi", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

            SqlDataReader reader = command.ExecuteReader();

            List<Uye> uyeler = new List<Uye>();

            while (reader.Read())
            {
                Uye uye = new Uye
                {
                    Isim = reader["isim"].ToString(),
                    Soyisim = reader["Soyisim"].ToString(),
                    KullaniciID = Convert.ToInt32(reader["KullaniciID"])
                };
                uyeler.Add(uye);
            }

            reader.Close();
            _connection.Close();
            return uyeler[0];
        }

        public bool YoneticiGirisYap(string AdminAdi, string Sifre)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "select * from Yonetici where AdminAdi=@KullaniciAdi and Sifre=@Sifre", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", AdminAdi);
            command.Parameters.AddWithValue("@Sifre", Sifre);

            SqlDataAdapter adaptor = new SqlDataAdapter(command);
            DataSet sonucDS = new DataSet();
            adaptor.Fill(sonucDS);
            _connection.Close();

            bool sonuc = false;
            if (sonucDS.Tables[0].Rows.Count > 0)
                sonuc = true;

            return sonuc;
        }

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select KullaniciAdi, CONCAT(isim,' ',Soyisim) as AdSoyad from Kullanici where KullaniciAdi like '%' + @arananKelime + '%' ";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet uyeler = new DataSet();
            _connection.Open();
            adapter.Fill(uyeler);
            _connection.Close();
            return uyeler;
        }

        public void ArkadasEkle(int ekleyen, int eklenen)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(
                "Insert into KullaniciArkadas (KullaniciID, ArkadasID, ArkadaslikTarihi) values (@ekleyen, @eklenen, @tarih)", _connection);
            command.Parameters.AddWithValue("@Ekleyen", ekleyen);
            command.Parameters.AddWithValue("@Eklenen", eklenen);
            command.Parameters.AddWithValue("@Tarih", DateTime.Today );

            command.ExecuteNonQuery();

            _connection.Close();

        }

        public bool isFriend(int currUser, int ziyaretEdilen)
        {

            string commandStr = "SELECT COUNT(*) as AffectedRows FROM KullaniciArkadas WHERE KullaniciID=@ekleyen AND ArkadasID=@eklenen";
            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddWithValue("@ekleyen", currUser);
            command.Parameters.AddWithValue("@eklenen", ziyaretEdilen);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet sonuc = new DataSet();
            _connection.Open();
            adapter.Fill(sonuc);
            _connection.Close();

            bool Kontrol = false;

            if (sonuc.Tables[0].Rows[0]["AffectedRows"] != DBNull.Value)
            {
                Kontrol = true;
            }

            return Kontrol;

        }






    }
}