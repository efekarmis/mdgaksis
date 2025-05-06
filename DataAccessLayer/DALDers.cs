using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public class DALDers
    {
        public static List<EntityDers> DersListele()
        {
            List<EntityDers> degerler = new List<EntityDers>();
            string query = @"
                SELECT
                    TBLDERS.DERSID,
                    TBLDERS.DERSAD,
                    TBLDERS.DERSMINKONTENJAN,
                    TBLDERS.DERSMAXKONTENJAN,
                    TBLDERS.OGRETMENID,
                    TBLOGRETMEN.OGRTAD,
                    TBLOGRETMEN.OGRTSOYAD,
                    TBLDERS.DERSUCRET,
                    TBLDERS.DERSACIKLAMA
                FROM
                    TBLDERS
                LEFT JOIN
                    TBLOGRETMEN ON TBLDERS.OGRETMENID = TBLOGRETMEN.OGRTID;";

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
                            EntityOgretmen ent2 = new EntityOgretmen
                            {
                                OGRTAD = rd["OGRTAD"] != DBNull.Value ? rd["OGRTAD"].ToString() : string.Empty,
                                OGRTSOYAD = rd["OGRTSOYAD"] != DBNull.Value ? rd["OGRTSOYAD"].ToString() : string.Empty
                            };

                            EntityDers ent = new EntityDers
                            {
                                ID = Convert.ToInt32(rd["DERSID"]),
                                DERSAD = rd["DERSAD"].ToString(),
                                MIN = Convert.ToInt32(rd["DERSMINKONTENJAN"]),
                                MAX = Convert.ToInt32(rd["DERSMAXKONTENJAN"]),
                                OGRETMENID = rd["OGRETMENID"] != DBNull.Value ? Convert.ToInt32(rd["OGRETMENID"]) : 0,
                                OGRETMEN = ent2,
                                DERSUCRET = rd["DERSUCRET"] == DBNull.Value ? 0.0m : Convert.ToDecimal(rd["DERSUCRET"]),
                                DERSACIKLAMA = rd["DERSACIKLAMA"] == DBNull.Value ? null : rd["DERSACIKLAMA"].ToString(),
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.DersListele Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static bool DersSil(int dersId)
        {
            int etkilenenSatir = 0;
            string query = "DELETE FROM TBLDERS WHERE DERSID=@P1";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", dersId);
                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.DersSil Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static bool DersGuncelle(EntityDers deger)
        {
            int etkilenenSatir = 0;
            string checkQuery = "SELECT COUNT(*) FROM TBLOGRETMEN WHERE OGRTID = @OgretmenId";
            string updateQuery = @"UPDATE TBLDERS
                                SET DERSAD = @DersAd,
                                    OGRETMENID = @OgretmenId,
                                    DERSMINKONTENJAN = @Min,
                                    DERSMAXKONTENJAN = @Max,
                                    DERSUCRET = @Ucret,
                                    DERSACIKLAMA = @Aciklama
                                WHERE DERSID = @DersId;";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            {
                try
                {
                    connection.Open();

                    if (deger.OGRETMENID > 0)
                    {
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@OgretmenId", deger.OGRETMENID);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (count == 0)
                            {
                                Console.WriteLine("DALDers.DersGuncelle Hata: Belirtilen OGRTID bulunamadı.");
                                return false;
                            }
                        }
                    }

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@DersAd", deger.DERSAD);
                        if (deger.OGRETMENID > 0)
                        {
                            updateCmd.Parameters.AddWithValue("@OgretmenId", deger.OGRETMENID);
                        }
                        else
                        {
                            updateCmd.Parameters.AddWithValue("@OgretmenId", DBNull.Value);
                        }
                        updateCmd.Parameters.AddWithValue("@DersId", deger.ID);

                        updateCmd.Parameters.AddWithValue("@Min", deger.MIN);
                        updateCmd.Parameters.AddWithValue("@Max", deger.MAX);
                        updateCmd.Parameters.Add("@Ucret", MySqlDbType.Decimal).Value = deger.DERSUCRET;
                        updateCmd.Parameters.AddWithValue("@Aciklama", string.IsNullOrEmpty(deger.DERSACIKLAMA) ? (object)DBNull.Value : deger.DERSACIKLAMA);

                        etkilenenSatir = updateCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.DersGuncelle Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static List<EntityDers> DersDetay(int id)
        {
            List<EntityDers> degerler = new List<EntityDers>();
            string query = @"
                SELECT
                    D.DERSID, D.DERSAD, D.DERSMINKONTENJAN, D.DERSMAXKONTENJAN, D.DERSUCRET, D.DERSACIKLAMA,
                    O.OGRTID, O.OGRTAD, O.OGRTSOYAD, O.OGRTFOTOGRAF
                FROM
                    TBLDERS D
                LEFT JOIN
                    TBLOGRETMEN O ON D.OGRETMENID = O.OGRTID
                WHERE
                    D.DERSID = @P1;";

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
                            EntityOgretmen ogretmen = null;
                            if (rd["OGRTID"] != DBNull.Value)
                            {
                                ogretmen = new EntityOgretmen
                                {
                                    OGRTID = Convert.ToInt32(rd["OGRTID"]),
                                    OGRTAD = rd["OGRTAD"].ToString(),
                                    OGRTSOYAD = rd["OGRTSOYAD"].ToString(),
                                    OGRTFOTOGRAF = rd["OGRTFOTOGRAF"] == DBNull.Value ? null : rd["OGRTFOTOGRAF"].ToString()
                                };
                            }

                            EntityDers ent = new EntityDers
                            {
                                ID = Convert.ToInt32(rd["DERSID"]),
                                DERSAD = rd["DERSAD"].ToString(),
                                MIN = Convert.ToInt32(rd["DERSMINKONTENJAN"]),
                                MAX = Convert.ToInt32(rd["DERSMAXKONTENJAN"]),
                                DERSUCRET = rd["DERSUCRET"] == DBNull.Value ? 0.0m : Convert.ToDecimal(rd["DERSUCRET"]),
                                DERSACIKLAMA = rd["DERSACIKLAMA"] == DBNull.Value ? null : rd["DERSACIKLAMA"].ToString(),
                                OGRETMENID = ogretmen?.OGRTID ?? 0,
                                OGRETMEN = ogretmen
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.DersDetay Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static int DersEkle(EntityDers param)
        {
            int etkilenenSatir = 0;
            string query = "INSERT INTO TBLDERS(DERSAD, DERSMINKONTENJAN, DERSMAXKONTENJAN, OGRETMENID, DERSUCRET, DERSACIKLAMA) VALUES (@DersAd, @Min, @Max, @OgretmenId, @Ucret, @Aciklama)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@DersAd", param.DERSAD);
                cmd.Parameters.AddWithValue("@Min", param.MIN);
                cmd.Parameters.AddWithValue("@Max", param.MAX);
                cmd.Parameters.Add("@Ucret", MySqlDbType.Decimal).Value = param.DERSUCRET;
                cmd.Parameters.AddWithValue("@Aciklama", string.IsNullOrEmpty(param.DERSACIKLAMA) ? (object)DBNull.Value : param.DERSACIKLAMA);


                if (param.OGRETMENID > 0)
                {
                   cmd.Parameters.AddWithValue("@OgretmenId", param.OGRETMENID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OgretmenId", DBNull.Value);
                }

                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.DersEkle Hata: " + ex.Message);
                    etkilenenSatir = -1;
                }
            }
            return etkilenenSatir;
        }

        public static int GetDersKayitSayisi(int dersId)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM TBLDERSKAYIT WHERE DERSID = @dersId";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@dersId", dersId);
                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    count = (result == DBNull.Value || result == null) ? 0 : Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.GetDersKayitSayisi Hata: " + ex.Message);
                }
            }
            return count;
        }

        public static List<EntityDers> GetDerslerByOgretmenId(int ogretmenId)
        {
            List<EntityDers> degerler = new List<EntityDers>();
            string query = @"SELECT DERSID, DERSAD, DERSMINKONTENJAN, DERSMAXKONTENJAN, DERSUCRET
                             FROM TBLDERS
                             WHERE OGRETMENID = @OgretmenId
                             ORDER BY DERSAD;";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OgretmenId", ogretmenId);
                try
                {
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityDers ent = new EntityDers
                            {
                                ID = Convert.ToInt32(rd["DERSID"]),
                                DERSAD = rd["DERSAD"].ToString(),
                                MIN = Convert.ToInt32(rd["DERSMINKONTENJAN"]),
                                MAX = Convert.ToInt32(rd["DERSMAXKONTENJAN"]),
                                OGRETMENID = ogretmenId,
                                DERSUCRET = rd["DERSUCRET"] == DBNull.Value ? 0.0m : Convert.ToDecimal(rd["DERSUCRET"])
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALDers.GetDerslerByOgretmenId Hata: " + ex.Message);
                }
            }
            return degerler;
        }
    }
}