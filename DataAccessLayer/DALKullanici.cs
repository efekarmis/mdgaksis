using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntityLayer;


namespace DataAccessLayer
{
    public static class DALKullanici
    {
        public static EntityKullanici KullaniciKontrol(string kullaniciID, string sifre, int kullaniciTipi)
        {
            EntityKullanici user = null;
            string query = "SELECT * FROM TBLKULLANICI WHERE KULLANICIID = @kullaniciID AND KULLANICISIFRE = @sifre AND KULLANICITIPI = @kullaniciTipi";

            if (!int.TryParse(kullaniciID, out int idAsInt))
            {
                Console.WriteLine("DALKullanici.KullaniciKontrol: Geçersiz kullanıcı ID formatı.");
                return null;
            }

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@kullaniciID", idAsInt);
                cmd.Parameters.AddWithValue("@sifre", sifre);
                cmd.Parameters.AddWithValue("@kullaniciTipi", kullaniciTipi);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new EntityKullanici
                            {
                                KULLANICIID = (int)reader["KULLANICIID"],
                                KULLANICISIFRE = reader["KULLANICISIFRE"].ToString(),
                                KULLANICITIPI = (int)reader["KULLANICITIPI"],
                                KULLANICIAD = reader["KULLANICIAD"].ToString(),
                                KULLANICISOYAD = reader["KULLANICISOYAD"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALKullanici.KullaniciKontrol Hata: " + ex.Message);
                }
            }
            return user;
        }

        public static void KullaniciEkle()
        {
            string ogrenciEkleQuery = @"
                INSERT INTO TBLKULLANICI (KULLANICIID, KULLANICISIFRE, KULLANICITIPI, KULLANICIAD, KULLANICISOYAD)
                SELECT OGRID, OGRSIFRE, 0, OGRAD, OGRSOYAD
                FROM TBLOGRENCI
                WHERE OGRID NOT IN (SELECT KULLANICIID FROM TBLKULLANICI WHERE KULLANICITIPI = 0)";

            string ogretmenEkleQuery = @"
                INSERT INTO TBLKULLANICI (KULLANICIID, KULLANICISIFRE, KULLANICITIPI, KULLANICIAD, KULLANICISOYAD)
                SELECT OGRTID, OGRTSIFRE, 1, OGRTAD, OGRTSOYAD
                FROM TBLOGRETMEN
                WHERE OGRTID NOT IN (SELECT KULLANICIID FROM TBLKULLANICI WHERE KULLANICITIPI = 1)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmdOgrenci = new MySqlCommand(ogrenciEkleQuery, connection))
                    {
                        cmdOgrenci.ExecuteNonQuery();
                    }

                    using (MySqlCommand cmdOgretmen = new MySqlCommand(ogretmenEkleQuery, connection))
                    {
                        cmdOgretmen.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALKullanici.KullaniciEkle Hata: " + ex.Message);
                }
            }
        }

        public static void KullaniciTablosunuKontrolEt()
        {
            string ogrenciKontrolQuery = @"
                INSERT INTO TBLKULLANICI (KULLANICIID, KULLANICISIFRE, KULLANICITIPI, KULLANICIAD, KULLANICISOYAD)
                SELECT OGRID, OGRSIFRE, 0, OGRAD, OGRSOYAD
                FROM TBLOGRENCI
                WHERE NOT EXISTS (SELECT 1 FROM TBLKULLANICI
                                  WHERE KULLANICIID = OGRID AND KULLANICITIPI = 0)";

            string ogretmenKontrolQuery = @"
                INSERT INTO TBLKULLANICI (KULLANICIID, KULLANICISIFRE, KULLANICITIPI, KULLANICIAD, KULLANICISOYAD)
                SELECT OGRTID, OGRTSIFRE, 1, OGRTAD, OGRTSOYAD
                FROM TBLOGRETMEN
                WHERE NOT EXISTS (SELECT 1 FROM TBLKULLANICI
                                  WHERE KULLANICIID = OGRTID AND KULLANICITIPI = 1)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmdOgrenci = new MySqlCommand(ogrenciKontrolQuery, connection))
                    {
                        cmdOgrenci.ExecuteNonQuery();
                    }

                    using (MySqlCommand cmdOgretmen = new MySqlCommand(ogretmenKontrolQuery, connection))
                    {
                        cmdOgretmen.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALKullanici.KullaniciTablosunuKontrolEt Hata: " + ex.Message);
                }
            }
        }
    }
}