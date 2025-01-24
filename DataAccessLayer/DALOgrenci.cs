using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EntityLayer;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class DALOgrenci
    {
        public static int OgrenciEkle(EntityOgrenci param)
        {
            var cmd1 = new MySqlCommand(
                "INSERT INTO TBLOGRENCI(OGRAD,OGRSOYAD,OGRNUMARA,OGRFOTOGRAF,OGRSIFRE) VALUES (@p1,@p2,@p3,@p4,@p5)",
                Baglanti.bgl);
            if (cmd1.Connection.State != ConnectionState.Open) cmd1.Connection.Open();

            cmd1.Parameters.AddWithValue("@p1", param.AD);
            cmd1.Parameters.AddWithValue("@p2", param.SOYAD);
            cmd1.Parameters.AddWithValue("@p3", param.NUMARA);
            cmd1.Parameters.AddWithValue("@p4", param.FOTOGRAF);
            cmd1.Parameters.AddWithValue("@p5", param.SIFRE);

            var sonuc = cmd1.ExecuteNonQuery();

            return sonuc;
        }
        public static List<EntityOgrenci> OgrenciListele()
        {
            List<EntityOgrenci> degerler = new List<EntityOgrenci>();
            MySqlCommand cmd2 = new MySqlCommand(
                "SELECT * FROM TBLOGRENCI", Baglanti.bgl);
            if (cmd2.Connection.State != ConnectionState.Open) cmd2.Connection.Open();
            MySqlDataReader rd = cmd2.ExecuteReader();
            while (rd.Read())
            {
                EntityOgrenci ent = new EntityOgrenci();

                ent.ID = Convert.ToInt32(rd["OGRID"].ToString());
                ent.AD = rd["OGRAD"].ToString();
                ent.SOYAD = rd["OGRSOYAD"].ToString();
                ent.NUMARA = rd["OGRNUMARA"].ToString();
                ent.FOTOGRAF = rd["OGRFOTOGRAF"].ToString();
                ent.SIFRE = rd["OGRSIFRE"].ToString();
                ent.BAKIYE = Convert.ToDouble(rd["OGRBAKIYE"].ToString());
                degerler.Add(ent);
            }
            rd.Close();
            return degerler;
        }
        public static bool OgrenciSil(int param)
        {
            MySqlCommand cmd3 = new MySqlCommand("DELETE FROM TBLOGRENCI WHERE OGRID=@P1", Baglanti.bgl);
            if (cmd3.Connection.State != ConnectionState.Open) cmd3.Connection.Open();
            cmd3.Parameters.AddWithValue("@P1", param);
            return cmd3.ExecuteNonQuery() > 0;
        }
        public static List<EntityOgrenci> OgrenciDetay(int id)
        {
            List<EntityOgrenci> degerler = new List<EntityOgrenci>();
            MySqlCommand cmd4 = new MySqlCommand(
                "SELECT * FROM TBLOGRENCI WHERE OGRID=@P1", Baglanti.bgl);
            if (cmd4.Connection.State != ConnectionState.Open) cmd4.Connection.Open();
            cmd4.Parameters.AddWithValue("@P1", id);
            MySqlDataReader rd = cmd4.ExecuteReader();
            while (rd.Read())
            {
                EntityOgrenci ent = new EntityOgrenci();

                ent.AD = rd["OGRAD"].ToString();
                ent.SOYAD = rd["OGRSOYAD"].ToString();
                ent.NUMARA = rd["OGRNUMARA"].ToString();
                ent.FOTOGRAF = rd["OGRFOTOGRAF"].ToString();
                ent.SIFRE = rd["OGRSIFRE"].ToString();
                ent.BAKIYE = Convert.ToDouble(rd["OGRBAKIYE"].ToString());
                degerler.Add(ent);
            }
            rd.Close();
            return degerler;
        }

        public static bool OgrenciGuncelle(EntityOgrenci deger)
        {
            MySqlCommand cmd5 =
                new MySqlCommand(
                    "UPDATE TBLOGRENCI SET OGRAD=@P1, OGRSOYAD=@P2, OGRNUMARA=@P3, OGRFOTOGRAF=@P4, OGRSIFRE=@P5 WHERE OGRID=@P6",Baglanti.bgl);
            if (cmd5.Connection.State != ConnectionState.Open) cmd5.Connection.Open();
            cmd5.Parameters.AddWithValue("@P1", deger.AD);
            cmd5.Parameters.AddWithValue("@P2", deger.SOYAD);
            cmd5.Parameters.AddWithValue("@P3", deger.NUMARA);
            cmd5.Parameters.AddWithValue("@P4", deger.FOTOGRAF);
            cmd5.Parameters.AddWithValue("@P5", deger.SIFRE);
            cmd5.Parameters.AddWithValue("@P6", deger.ID);
            return cmd5.ExecuteNonQuery() > 0;
        }
    }
}