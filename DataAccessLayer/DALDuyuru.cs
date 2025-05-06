using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public static class DALDuyuru
    {
        public static List<EntityDuyuru> AktifDuyurulariGetir(int kullaniciTipi)
        {
            List<EntityDuyuru> duyurular = new List<EntityDuyuru>();
            string query = @"SELECT DuyuruID, Baslik, Icerik, HedefKitle, OnemDerecesi, YayinTarihi
                             FROM TBLDUYURU
                             WHERE HedefKitle = 2 OR HedefKitle = @KullaniciTipiParam
                             ORDER BY OnemDerecesi DESC, YayinTarihi DESC;";

            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@KullaniciTipiParam", kullaniciTipi);

                try
                {
                    con.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityDuyuru duyuru = new EntityDuyuru
                            {
                                DuyuruID = Convert.ToInt32(rd["DuyuruID"]),
                                Baslik = rd["Baslik"].ToString(),
                                Icerik = rd["Icerik"].ToString(),
                                HedefKitle = Convert.ToInt32(rd["HedefKitle"]),
                                OnemDerecesi = Convert.ToInt16(rd["OnemDerecesi"]),
                                YayinTarihi = Convert.ToDateTime(rd["YayinTarihi"])
                            };
                            duyurular.Add(duyuru);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DALDuyuru.AktifDuyurulariGetir Hata: " + ex.ToString());
                }
            }
            return duyurular;
        }

        public static List<EntityDuyuru> TumDuyurulariGetir()
        {
            List<EntityDuyuru> duyurular = new List<EntityDuyuru>();
            string query = @"SELECT DuyuruID, Baslik, Icerik, HedefKitle, OnemDerecesi, YayinTarihi
                              FROM TBLDUYURU ORDER BY YayinTarihi DESC;";
            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityDuyuru duyuru = new EntityDuyuru
                            {
                                DuyuruID = Convert.ToInt32(rd["DuyuruID"]),
                                Baslik = rd["Baslik"].ToString(),
                                Icerik = rd["Icerik"].ToString(),
                                HedefKitle = Convert.ToInt32(rd["HedefKitle"]),
                                OnemDerecesi = Convert.ToInt16(rd["OnemDerecesi"]),
                                YayinTarihi = Convert.ToDateTime(rd["YayinTarihi"])
                            };
                            duyurular.Add(duyuru);
                        }
                    }
                }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine("DALDuyuru.TumDuyurulariGetir Hata: " + ex.ToString()); }
            }
            return duyurular;
        }

        public static int DuyuruEkle(EntityDuyuru duyuru)
        {
            int sonuc = 0;
            string query = @"INSERT INTO TBLDUYURU (Baslik, Icerik, HedefKitle, OnemDerecesi)
                             VALUES (@Baslik, @Icerik, @HedefKitle, @OnemDerecesi);";
            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Baslik", duyuru.Baslik);
                cmd.Parameters.AddWithValue("@Icerik", duyuru.Icerik);
                cmd.Parameters.AddWithValue("@HedefKitle", duyuru.HedefKitle);
                cmd.Parameters.AddWithValue("@OnemDerecesi", duyuru.OnemDerecesi);

                try { con.Open(); sonuc = cmd.ExecuteNonQuery(); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine("DALDuyuru.DuyuruEkle Hata: " + ex.ToString()); sonuc = -1; }
            }
            return sonuc;
        }

        public static bool DuyuruSil(int duyuruId)
        {
            int sonuc = 0;
            string query = "DELETE FROM TBLDUYURU WHERE DuyuruID = @DuyuruID";
            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DuyuruID", duyuruId);
                try { con.Open(); sonuc = cmd.ExecuteNonQuery(); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine("DALDuyuru.DuyuruSil Hata: " + ex.ToString()); }
            }
            return sonuc > 0;
        }

        public static EntityDuyuru DuyuruGetir(int duyuruId)
        {
            EntityDuyuru duyuru = null;
            string query = @"SELECT DuyuruID, Baslik, Icerik, HedefKitle, OnemDerecesi, YayinTarihi
                             FROM TBLDUYURU WHERE DuyuruID = @DuyuruID";

            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DuyuruID", duyuruId);
                try
                {
                    con.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            duyuru = new EntityDuyuru
                            {
                                DuyuruID = Convert.ToInt32(rd["DuyuruID"]),
                                Baslik = rd["Baslik"].ToString(),
                                Icerik = rd["Icerik"].ToString(),
                                HedefKitle = Convert.ToInt32(rd["HedefKitle"]),
                                OnemDerecesi = Convert.ToInt16(rd["OnemDerecesi"]),
                                YayinTarihi = Convert.ToDateTime(rd["YayinTarihi"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DALDuyuru.DuyuruGetir Hata: " + ex.ToString());
                }
            }
            return duyuru;
        }

        public static bool DuyuruGuncelle(EntityDuyuru duyuru)
        {
            int sonuc = 0;
            string query = @"UPDATE TBLDUYURU SET
                                Baslik = @Baslik,
                                Icerik = @Icerik,
                                HedefKitle = @HedefKitle,
                                OnemDerecesi = @OnemDerecesi
                             WHERE DuyuruID = @DuyuruID";

            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Baslik", duyuru.Baslik);
                cmd.Parameters.AddWithValue("@Icerik", duyuru.Icerik);
                cmd.Parameters.AddWithValue("@HedefKitle", duyuru.HedefKitle);
                cmd.Parameters.AddWithValue("@OnemDerecesi", duyuru.OnemDerecesi);
                cmd.Parameters.AddWithValue("@DuyuruID", duyuru.DuyuruID);

                try { con.Open(); sonuc = cmd.ExecuteNonQuery(); }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine("DALDuyuru.DuyuruGuncelle Hata: " + ex.ToString()); }
            }
            return sonuc > 0;
        }

        public static List<EntityDuyuru> TumOgrenciDuyurulariGetir()
        {
            List<EntityDuyuru> duyurular = new List<EntityDuyuru>();
            string query = @"SELECT DuyuruID, Baslik, Icerik, HedefKitle, OnemDerecesi, YayinTarihi
                             FROM TBLDUYURU
                             WHERE HedefKitle = 0 OR HedefKitle = 2
                             ORDER BY YayinTarihi DESC;";

            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityDuyuru duyuru = new EntityDuyuru
                            {
                                DuyuruID = Convert.ToInt32(rd["DuyuruID"]),
                                Baslik = rd["Baslik"].ToString(),
                                Icerik = rd["Icerik"].ToString(),
                                HedefKitle = Convert.ToInt32(rd["HedefKitle"]),
                                OnemDerecesi = Convert.ToInt16(rd["OnemDerecesi"]),
                                YayinTarihi = Convert.ToDateTime(rd["YayinTarihi"])
                            };
                            duyurular.Add(duyuru);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DALDuyuru.TumOgrenciDuyurulariGetir Hata: " + ex.ToString());
                }
            }
            return duyurular;
        }
    }
}