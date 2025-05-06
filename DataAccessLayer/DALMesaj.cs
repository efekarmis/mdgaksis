using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public static class DALMesaj
    {
        public static int MesajEkle(EntityMesaj mesaj)
        {
            int sonuc = 0;
            string query = @"INSERT INTO TBLMESAJLAR (DersID, GonderenKullaniciID, GonderenKullaniciTipi, MesajIcerik)
                             VALUES (@DersID, @GonderenID, @GonderenTip, @Icerik);";

            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DersID", mesaj.DersID);
                cmd.Parameters.AddWithValue("@GonderenID", mesaj.GonderenKullaniciID);
                cmd.Parameters.AddWithValue("@GonderenTip", mesaj.GonderenKullaniciTipi);
                cmd.Parameters.AddWithValue("@Icerik", mesaj.MesajIcerik);

                try
                {
                    con.Open();
                    sonuc = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DALMesaj.MesajEkle Hata: " + ex.ToString());
                    sonuc = -1;
                }
            }
            return sonuc;
        }

        public static List<EntityMesaj> GetMesajlarByDersId(int dersId)
        {
            List<EntityMesaj> mesajlar = new List<EntityMesaj>();
            string query = @"SELECT MesajID, DersID, GonderenKullaniciID, GonderenKullaniciTipi, MesajIcerik, GondermeTarihi
                             FROM TBLMESAJLAR
                             WHERE DersID = @DersId
                             ORDER BY GondermeTarihi ASC;";

            using (MySqlConnection con = new MySqlConnection(Baglanti.ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DersId", dersId);
                try
                {
                    con.Open();
                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            EntityMesaj mesaj = new EntityMesaj
                            {
                                MesajID = Convert.ToInt32(rd["MesajID"]),
                                DersID = Convert.ToInt32(rd["DersID"]),
                                GonderenKullaniciID = Convert.ToInt32(rd["GonderenKullaniciID"]),
                                GonderenKullaniciTipi = Convert.ToInt32(rd["GonderenKullaniciTipi"]),
                                MesajIcerik = rd["MesajIcerik"].ToString(),
                                GondermeTarihi = Convert.ToDateTime(rd["GondermeTarihi"])
                            };
                            mesajlar.Add(mesaj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DALMesaj.GetMesajlarByDersId Hata: " + ex.ToString());
                }
            }
            return mesajlar;
        }
    }
}