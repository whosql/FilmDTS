using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FilmDTS.siniflar.veritabani
{
    public class YonetmenDal
    {

        static SqlConnection _connection = new SqlConnection("Data Source=DESKTOP-50JSIU1\\SQLEXPRESS;Initial Catalog=FilmDTS;Integrated Security=True");

        public DataSet Arama(string arananKelime)
        {
            string komutStr = @"select YonetmenID, AdSoyad from Yonetmen where AdSoyad like '%' + @arananKelime + '%'";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@arananKelime", arananKelime);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet yonetmenler = new DataSet();
            _connection.Open();
            adapter.Fill(yonetmenler);
            _connection.Close();
            return yonetmenler;
        }

        public DataSet GetByID(int yonetmenID)
        {
            string komutStr = @"select * from Yonetmen where YonetmenID=@yonetmenID";
            SqlCommand komut = new SqlCommand(komutStr, _connection);
            komut.Parameters.AddWithValue("@yonetmenID", yonetmenID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komut;
            DataSet yonetmen = new DataSet();
            _connection.Open();
            adapter.Fill(yonetmen);
            _connection.Close();
            return yonetmen;
        }

        public List<Yonetmen> GetAll()
        {
            _connection.Open();
            SqlCommand command = new SqlCommand("Select * from Yonetmen", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Yonetmen> yonetmenler = new List<Yonetmen>();

            while (reader.Read())
            {
                Yonetmen yonetmen = new Yonetmen
                {
                    YonetmenID = Convert.ToInt32(reader["YonetmenID"]),
                    AdSoyad = reader["AdSoyad"].ToString()
                };
                yonetmenler.Add(yonetmen);
            }

            reader.Close();
            _connection.Close();
            return yonetmenler;
        }

    }
}