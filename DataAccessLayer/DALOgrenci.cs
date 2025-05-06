using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public class DALOgrenci
    {
        public static List<EntityDersKayit> OgrenciOnayliDersListesi(int ogrenciId)
        {
            List<EntityDersKayit> degerler = new List<EntityDersKayit>();
            string query = @"
                WITH DersOgrenciSayilari AS (
    SELECT DERSID, COUNT(DISTINCT OGRENCIID) AS ToplamKayitliOgrenci
    FROM TBLDERSKAYIT GROUP BY DERSID
),
OgrencininSonKayitlari AS (
     SELECT DERSID, KAYITTARIHI,
            ROW_NUMBER() OVER(PARTITION BY OGRENCIID, DERSID ORDER BY KAYITTARIHI DESC) as rn
     FROM TBLDERSKAYIT WHERE OGRENCIID = @P1
)
SELECT
    D.DERSID, D.DERSAD,
    -- ISNULL yerine IFNULL ve + yerine CONCAT kullanıldı
    IFNULL(CONCAT(O.OGRTAD, ' ', O.OGRTSOYAD), 'Atanmamış') AS OgretmenAdSoyad,
    OSK.KAYITTARIHI,
    -- ISNULL yerine IFNULL kullanıldı
    IFNULL(DOS.ToplamKayitliOgrenci, 0) AS MevcutOgrenciSayisi,
    D.DERSMINKONTENJAN,
    O.OGRTID
FROM OgrencininSonKayitlari OSK
INNER JOIN TBLDERS D ON OSK.DERSID = D.DERSID
LEFT JOIN TBLOGRETMEN O ON D.OGRETMENID = O.OGRTID
LEFT JOIN DersOgrenciSayilari DOS ON OSK.DERSID = DOS.DERSID
WHERE OSK.rn = 1;";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", ogrenciId);
                try
                {
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityDersKayit ent = new EntityDersKayit
                            {
                                DersId = Convert.ToInt32(rd["DERSID"]),
                                DersAd = rd["DERSAD"].ToString(),
                                OgretmenAdSoyad = rd["OgretmenAdSoyad"].ToString(),
                                KayitTarihi = Convert.ToDateTime(rd["KAYITTARIHI"]),
                                MevcutOgrenciSayisi = Convert.ToInt32(rd["MevcutOgrenciSayisi"]),
                                MinKontenjan = Convert.ToInt32(rd["DERSMINKONTENJAN"]),
                                OgretmenID = rd["OGRTID"] == DBNull.Value ? 0 : Convert.ToInt32(rd["OGRTID"])
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.OgrenciOnayliDersListesi Hata: " + ex.Message);
                }
            }
            return degerler;
        }
        public static bool OgrenciGiris(string ogrenciNumara, string ogrenciSifre)
        {
            bool girisBasarili = false;
            string query = "SELECT KULLANICIID FROM TBLKULLANICI WHERE KULLANICIID = @numara AND KULLANICISIFRE = @sifre AND KULLANICITIPI = 0";

            if (!int.TryParse(ogrenciNumara, out int numaraAsInt))
            {
                Console.WriteLine("DALOgrenci.OgrenciGiris: Geçersiz numara formatı.");
                return false;
            }

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@numara", numaraAsInt);
                cmd.Parameters.AddWithValue("@sifre", ogrenciSifre);

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
                    Console.WriteLine("DALOgrenci.OgrenciGiris Hata: " + ex.Message);
                }
            }
            return girisBasarili;
        }

        public static int OgrenciEkle(EntityOgrenci param)
        {
            int etkilenenSatir = 0;
            string query = "INSERT INTO TBLOGRENCI(OGRAD,OGRSOYAD,OGRNUMARA,OGRFOTOGRAF,OGRSIFRE) VALUES (@p1,@p2,@p3,@p4,@p5)";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@p1", param.AD);
                cmd.Parameters.AddWithValue("@p2", param.SOYAD);
                cmd.Parameters.AddWithValue("@p3", param.NUMARA);
                cmd.Parameters.AddWithValue("@p4", param.FOTOGRAF ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@p5", param.SIFRE);

                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.OgrenciEkle Hata: " + ex.Message);
                    etkilenenSatir = -1;
                }
            }
            return etkilenenSatir;
        }

        public static List<EntityOgrenci> OgrenciListele()
        {
            List<EntityOgrenci> degerler = new List<EntityOgrenci>();
            string query = "SELECT OGRID, OGRAD, OGRSOYAD, OGRNUMARA, OGRFOTOGRAF, OGRSIFRE, OGRBAKIYE FROM TBLOGRENCI";

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
                            EntityOgrenci ent = new EntityOgrenci
                            {
                                ID = Convert.ToInt32(rd["OGRID"]),
                                AD = rd["OGRAD"].ToString(),
                                SOYAD = rd["OGRSOYAD"].ToString(),
                                NUMARA = rd["OGRNUMARA"].ToString(),
                                FOTOGRAF = rd["OGRFOTOGRAF"] != DBNull.Value ? rd["OGRFOTOGRAF"].ToString() : null,
                                SIFRE = rd["OGRSIFRE"].ToString(),
                                BAKIYE = rd["OGRBAKIYE"] != DBNull.Value ? Convert.ToDouble(rd["OGRBAKIYE"]) : 0.0
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.OgrenciListele Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static bool OgrenciSil(int ogrenciId)
        {
            int etkilenenSatir = 0;
            string query = "DELETE FROM TBLOGRENCI WHERE OGRID=@P1";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", ogrenciId);
                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.OgrenciSil Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static List<EntityOgrenci> OgrenciDetay(int id)
        {
            List<EntityOgrenci> degerler = new List<EntityOgrenci>();
            string query = "SELECT OGRID, OGRAD, OGRSOYAD, OGRNUMARA, OGRFOTOGRAF, OGRSIFRE, OGRBAKIYE FROM TBLOGRENCI WHERE OGRID=@P1";

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
                            EntityOgrenci ent = new EntityOgrenci
                            {
                                ID = Convert.ToInt32(rd["OGRID"]),
                                AD = rd["OGRAD"].ToString(),
                                SOYAD = rd["OGRSOYAD"].ToString(),
                                NUMARA = rd["OGRNUMARA"].ToString(),
                                FOTOGRAF = rd["OGRFOTOGRAF"] != DBNull.Value ? rd["OGRFOTOGRAF"].ToString() : null,
                                SIFRE = rd["OGRSIFRE"].ToString(),
                                BAKIYE = rd["OGRBAKIYE"] != DBNull.Value ? Convert.ToDouble(rd["OGRBAKIYE"]) : 0.0
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.OgrenciDetay Hata: " + ex.Message);
                }
            }
            return degerler;
        }

        public static bool OgrenciGuncelle(EntityOgrenci deger)
        {
            int etkilenenSatir = 0;
            string query = "UPDATE TBLOGRENCI SET OGRAD=@P1, OGRSOYAD=@P2, OGRNUMARA=@P3, OGRFOTOGRAF=@P4, OGRSIFRE=@P5 WHERE OGRID=@P6";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@P1", deger.AD);
                cmd.Parameters.AddWithValue("@P2", deger.SOYAD);
                cmd.Parameters.AddWithValue("@P3", deger.NUMARA);
                cmd.Parameters.AddWithValue("@P4", deger.FOTOGRAF ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@P5", deger.SIFRE);
                cmd.Parameters.AddWithValue("@P6", deger.ID);
                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.OgrenciGuncelle Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static bool BakiyeGuncelle(int ogrenciId, double eklenecekMiktar)
        {
            int etkilenenSatir = 0;
            string query = "UPDATE TBLOGRENCI SET OGRBAKIYE = ISNULL(OGRBAKIYE, 0) + @EklenecekMiktar WHERE OGRID = @OgrenciId";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.Add("@EklenecekMiktar", MySqlDbType.Decimal).Value = Convert.ToDecimal(eklenecekMiktar);
                cmd.Parameters.AddWithValue("@OgrenciId", ogrenciId);

                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.BakiyeGuncelle Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static bool BakiyeAzalt(int ogrenciId, decimal azaltilacakMiktar)
        {
            if (azaltilacakMiktar < 0) return false;

            int etkilenenSatir = 0;
            string query = @"UPDATE TBLOGRENCI
                     SET OGRBAKIYE = ISNULL(OGRBAKIYE, 0) - @AzaltilacakMiktar
                     WHERE OGRID = @OgrenciId
                       AND ISNULL(OGRBAKIYE, 0) >= @AzaltilacakMiktar;";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.Add("@AzaltilacakMiktar", MySqlDbType.Decimal).Value = azaltilacakMiktar;
                cmd.Parameters.AddWithValue("@OgrenciId", ogrenciId);

                try
                {
                    connection.Open();
                    etkilenenSatir = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.BakiyeAzalt Hata: " + ex.Message);
                }
            }
            return etkilenenSatir > 0;
        }

        public static List<EntityOgrenci> GetOgrencilerByDersId(int dersId)
        {
            List<EntityOgrenci> degerler = new List<EntityOgrenci>();
            string query = @"SELECT O.OGRID, O.OGRAD, O.OGRSOYAD, O.OGRNUMARA, O.OGRFOTOGRAF, O.OGRBAKIYE, O.OGRSIFRE
                     FROM TBLDERSKAYIT DK
                     INNER JOIN TBLOGRENCI O ON DK.OGRENCIID = O.OGRID
                     WHERE DK.DERSID = @DersId
                     ORDER BY O.OGRSOYAD, O.OGRAD;";

            using (MySqlConnection connection = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@DersId", dersId);
                try
                {
                    connection.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityOgrenci ent = new EntityOgrenci
                            {
                                ID = Convert.ToInt32(rd["OGRID"]),
                                AD = rd["OGRAD"].ToString(),
                                SOYAD = rd["OGRSOYAD"].ToString(),
                                NUMARA = rd["OGRNUMARA"].ToString(),
                                FOTOGRAF = rd["OGRFOTOGRAF"] != DBNull.Value ? rd["OGRFOTOGRAF"].ToString() : null,
                                BAKIYE = rd["OGRBAKIYE"] != DBNull.Value ? Convert.ToDouble(rd["OGRBAKIYE"]) : 0.0,
                                SIFRE = rd["OGRSIFRE"].ToString()
                            };
                            degerler.Add(ent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DALOgrenci.GetOgrencilerByDersId Hata: " + ex.Message);
                }
            }
            return degerler;
        }
    }
}