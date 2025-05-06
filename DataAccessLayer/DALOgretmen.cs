using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public class DALOgretmen
    {
        public static bool OgretmenGiris(string ogretmenID, string ogretmenSifre)
        {
            bool girisBasarili = false;
            string query = "SELECT KULLANICIID FROM TBLKULLANICI WHERE KULLANICIID = @id AND KULLANICISIFRE = @sifre AND KULLANICITIPI = 1";

            if (!int.TryParse(ogretmenID, out int idAsInt))
            {
                Console.WriteLine("DALOgretmen.OgretmenGiris: Geçersiz öğretmen ID formatı.");
                return false;
            }

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", idAsInt);
                cmd.Parameters.AddWithValue("@sifre", ogretmenSifre);

                try
                {
                    connection.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        girisBasarili = dr.HasRows;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgretmen.OgretmenGiris Hata: " + ex.Message);
                }
            }
            return girisBasarili;
        }

        public static int OgretmenEkle(EntityOgretmen param)
        {
            int etkilenenSatir = 0;

            string query = "INSERT INTO TBLOGRETMEN(OGRTAD, OGRTSOYAD, OGRTSIFRE, OGRTFOTOGRAF) VALUES (@OgrtAd, @OgrtSoyad, @OgrtSifre, @OgrtFotograf)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OgrtAd", param.OGRTAD);
                cmd.Parameters.AddWithValue("@OgrtSoyad", param.OGRTSOYAD);
                cmd.Parameters.AddWithValue("@OgrtSifre", param.OGRTSIFRE);
                cmd.Parameters.AddWithValue("@OgrtFotograf", (object)param.OGRTFOTOGRAF ?? DBNull.Value);

                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgretmen.OgretmenEkle Hata: " + ex.Message);
                    etkilenenSatir = -1;
                }
            }
            return etkilenenSatir;
        }

        public static List<EntityOgretmen> OgretmenListele()
        {
            List<EntityOgretmen> degerler = new List<EntityOgretmen>();

            string query = "SELECT OGRTID, OGRTAD, OGRTSOYAD, OGRTSIFRE, OGRTFOTOGRAF FROM TBLOGRETMEN";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityOgretmen ent = new EntityOgretmen
                            {
                                OGRTID = Convert.ToInt32(rd["OGRTID"]),
                                OGRTAD = rd["OGRTAD"].ToString(),
                                OGRTSOYAD = rd["OGRTSOYAD"].ToString(),
                                OGRTSIFRE = rd["OGRTSIFRE"].ToString(),
                                OGRTFOTOGRAF = rd["OGRTFOTOGRAF"] != DBNull.Value ? rd["OGRTFOTOGRAF"].ToString() : null
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgretmen.OgretmenListele Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static bool OgretmenSil(int ogretmenId)
        {
            int etkilenenSatir = 0;
            string query = "DELETE FROM TBLOGRETMEN WHERE OGRTID=@P1";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", ogretmenId);
                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgretmen.OgretmenSil Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static List<EntityOgretmen> OgretmenDetay(int id)
        {
            List<EntityOgretmen> degerler = new List<EntityOgretmen>();

            string query = "SELECT OGRTID, OGRTAD, OGRTSOYAD, OGRTSIFRE, OGRTFOTOGRAF FROM TBLOGRETMEN WHERE OGRTID=@P1";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", id);
                try
                {
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityOgretmen ent = new EntityOgretmen
                            {
                                OGRTID = Convert.ToInt32(rd["OGRTID"]),
                                OGRTAD = rd["OGRTAD"].ToString(),
                                OGRTSOYAD = rd["OGRTSOYAD"].ToString(),
                                OGRTSIFRE = rd["OGRTSIFRE"].ToString(),
                                OGRTFOTOGRAF = rd["OGRTFOTOGRAF"] != DBNull.Value ? rd["OGRTFOTOGRAF"].ToString() : null
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgretmen.OgretmenDetay Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static bool OgretmenGuncelle(EntityOgretmen deger)
        {
            int etkilenenSatir = 0;

            string query = "UPDATE TBLOGRETMEN SET OGRTAD=@OgrtAd, OGRTSOYAD=@OgrtSoyad, OGRTSIFRE=@OgrtSifre, OGRTFOTOGRAF=@OgrtFotograf WHERE OGRTID=@OgrtId";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OgrtAd", deger.OGRTAD);
                cmd.Parameters.AddWithValue("@OgrtSoyad", deger.OGRTSOYAD);
                cmd.Parameters.AddWithValue("@OgrtSifre", deger.OGRTSIFRE);
                cmd.Parameters.AddWithValue("@OgrtFotograf", (object)deger.OGRTFOTOGRAF ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OgrtId", deger.OGRTID);
                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgretmen.OgretmenGuncelle Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }
    }
}