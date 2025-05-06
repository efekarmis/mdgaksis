using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public class DALBasvuru
    {
        public static List<EntityBasvuruForm> BasvuruListele()
        {
            List<EntityBasvuruForm> degerler = new List<EntityBasvuruForm>();
            string query = @"
                SELECT
                    TBLBASVURUFORM.BASVURUID,
                    TBLOGRENCI.OGRAD,
                    TBLOGRENCI.OGRSOYAD,
                    TBLDERS.DERSAD
                FROM TBLBASVURUFORM
                LEFT JOIN TBLDERS ON TBLDERS.DERSID = TBLBASVURUFORM.DERSID
                LEFT JOIN TBLOGRENCI ON TBLOGRENCI.OGRID = TBLBASVURUFORM.OGRENCIID;";

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
                            EntityBasvuruForm ent = new EntityBasvuruForm
                            {
                                BASVURUID = Convert.ToInt32(rd["BASVURUID"]),
                                BASVURUDERSAD = rd["DERSAD"].ToString(),
                                BASVURUOGRADSOYAD = rd["OGRAD"].ToString() + " " + rd["OGRSOYAD"].ToString()
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.BasvuruListele Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static List<EntityBasvuruForm> BasvuruKontrolListesi()
        {
            List<EntityBasvuruForm> degerler = new List<EntityBasvuruForm>();
            string query = @"
                SELECT
                    TBLBASVURUFORM.BASVURUID,
                    TBLBASVURUFORM.DERSID,
                    TBLBASVURUFORM.OGRENCIID
                FROM TBLBASVURUFORM";

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
                            EntityBasvuruForm ent = new EntityBasvuruForm
                            {
                                BASVURUID = Convert.ToInt32(rd["BASVURUID"]),
                                BASVURUOGRID = Convert.ToInt32(rd["OGRENCIID"]),
                                BASVURUDERSID = Convert.ToInt32(rd["DERSID"])
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.BasvuruKontrolListesi Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static EntityBasvuruForm BasvuruGetir(int basvuruId)
        {
            EntityBasvuruForm basvuru = null;
            string query = "SELECT * FROM TBLBASVURUFORM WHERE BASVURUID = @id";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", basvuruId);
                try
                {
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            basvuru = new EntityBasvuruForm
                            {
                                BASVURUID = Convert.ToInt32(rd["BASVURUID"]),
                                BASVURUDERSID = Convert.ToInt32(rd["DERSID"]),
                                BASVURUOGRID = Convert.ToInt32(rd["OGRENCIID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.BasvuruGetir Hata: " + ex.Message);
                }
            }
            return basvuru;
        }

        public static void BasvuruSil(int basvuruId)
        {
            string query = "DELETE FROM TBLBASVURUFORM WHERE BASVURUID = @id";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", basvuruId);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.BasvuruSil Hata: " + ex.Message);
                }
            }
        }

        public static void DersKaydiEkle(int ogrenciId, int dersId)
        {
            string query = "INSERT INTO TBLDERSKAYIT (OGRENCIID, DERSID, KAYITTARIHI) VALUES (@ogrenciId, @dersId, @kayitTarihi)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ogrenciId", ogrenciId);
                cmd.Parameters.AddWithValue("@dersId", dersId);
                cmd.Parameters.AddWithValue("@kayitTarihi", DateTime.Now);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.DersKaydiEkle Hata: " + ex.Message);
                }
            }
        }

        public static int TalepEkle(EntityBasvuruForm param)
        {
            int etkilenenSatir = 0;
            string query = "INSERT INTO TBLBASVURUFORM (OGRENCIID,DERSID) VALUES (@P1,@P2)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", param.BASVURUOGRID);
                cmd.Parameters.AddWithValue("@P2", param.BASVURUDERSID);
                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.TalepEkle Hata: " + ex.Message);
                    etkilenenSatir = -1;
                }
            }
            return etkilenenSatir;
        }

        public static bool IsOgrenciKayitli(int ogrenciId, int dersId)
        {
            bool kayitli = false;
            string query = "SELECT COUNT(*) FROM TBLDERSKAYIT WHERE OGRENCIID = @OgrenciId AND DERSID = @DersId";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OgrenciId", ogrenciId);
                cmd.Parameters.AddWithValue("@DersId", dersId);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        kayitli = Convert.ToInt32(result) > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALBasvuru.IsOgrenciKayitli Hata: " + ex.Message);
                }
            }
            return kayitli;
        }
    }
}