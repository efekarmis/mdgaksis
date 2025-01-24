using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EntityLayer;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class DALOgretmen
    {
        public static int OgretmenEkle(EntityOgretmen param)
        {
            var cmd1 = new MySqlCommand(
                "INSERT INTO TBLOGRETMEN(OGRTADSOYAD) VALUES (@p1)",
                Baglanti.bgl);
            if (cmd1.Connection.State != ConnectionState.Open) cmd1.Connection.Open();

            cmd1.Parameters.AddWithValue("@p1", param.OGRTADSOYAD);
            var sonuc = cmd1.ExecuteNonQuery();

            return sonuc;
        }
        public static List<EntityOgretmen> OgretmenListele()
        {
            List<EntityOgretmen> degerler = new List<EntityOgretmen>();
            MySqlCommand cmd2 = new MySqlCommand(
                "SELECT * FROM TBLOGRETMEN", Baglanti.bgl);
            if (cmd2.Connection.State != ConnectionState.Open) cmd2.Connection.Open();
            MySqlDataReader rd = cmd2.ExecuteReader();
            while (rd.Read())
            {
                EntityOgretmen ent = new EntityOgretmen();

                ent.OGRTID = Convert.ToInt32(rd["OGRTID"].ToString());
                ent.OGRTADSOYAD = rd["OGRTADSOYAD"].ToString();
                degerler.Add(ent);
            }
            rd.Close();
            return degerler;
        }
        public static bool OgretmenSil(int param)
        {
            MySqlCommand cmd3 = new MySqlCommand("DELETE FROM TBLOGRETMEN WHERE OGRID=@P1", Baglanti.bgl);
            if (cmd3.Connection.State != ConnectionState.Open) cmd3.Connection.Open();
            cmd3.Parameters.AddWithValue("@P1", param);
            return cmd3.ExecuteNonQuery() > 0;
        }
        public static List<EntityOgretmen> OgretmenDetay(int id)
        {
            List<EntityOgretmen> degerler = new List<EntityOgretmen>();
            MySqlCommand cmd4 = new MySqlCommand(
                "SELECT * FROM TBLOGRETMEN WHERE OGRID=@P1", Baglanti.bgl);
            if (cmd4.Connection.State != ConnectionState.Open) cmd4.Connection.Open();
            cmd4.Parameters.AddWithValue("@P1", id);
            MySqlDataReader rd = cmd4.ExecuteReader();
            while (rd.Read())
            {
                EntityOgretmen ent = new EntityOgretmen();

                ent.OGRTADSOYAD = rd["OGRTADSOYAD"].ToString();
                degerler.Add(ent);
            }
            rd.Close();
            return degerler;
        }

        public static bool OgretmenGuncelle(EntityOgretmen deger)
        {
            MySqlCommand cmd5 =
                new MySqlCommand(
                    "UPDATE TBLOGRETMEN SET OGRTADSOYAD=@P1 WHERE OGRTID=@P2",Baglanti.bgl);
            if (cmd5.Connection.State != ConnectionState.Open) cmd5.Connection.Open();
            cmd5.Parameters.AddWithValue("@P1", deger.OGRTADSOYAD);
            cmd5.Parameters.AddWithValue("@P2", deger.OGRTID);
            return cmd5.ExecuteNonQuery() > 0;
        }
    }
}