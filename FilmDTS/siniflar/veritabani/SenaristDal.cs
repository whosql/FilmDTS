using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FilmDTS.siniflar.veritabani
{
    public class SenaristDal
    {
        static SqlConnection _connection = new SqlConnection("Data Source=DESKTOP-50JSIU1\\SQLEXPRESS;Initial Catalog=FilmDTS;Integrated Security=True");

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select SenaristID, AdSoyad from Senarist where AdSoyad like '%' + @arananKelime + '%'";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet senaristler = new DataSet();
            _connection.Open();
            adapter.Fill(senaristler);
            _connection.Close();
            return senaristler;
        }

        public DataSet GetByID(int senaristID)
        {
            string komutStr = @"select * from Senarist where SenaristID=@senaristID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@senaristID", senaristID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet senarist = new DataSet();
            _connection.Open();
            adapter.Fill(senarist);
            _connection.Close();
            return senarist;
        }

        public List<Senarist> GetAll()
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select * from Senarist", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Senarist> senaristler = new List<Senarist>();

            while (reader.Read())
            {
                Senarist Senarist = new Senarist
                {
                    SenaristID = Convert.ToInt32(reader["SenaristID"]),
                    AdSoyad = reader["AdSoyad"].ToString()
                };
                senaristler.Add(Senarist);
            }

            reader.Close();
            _connection.Close();
            return senaristler;
        }

    }
}