using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EntityLayer;
using MySql.Data.MySqlClient;


namespace DataAccessLayer
{
    public class DALKontenjanlar
    {
        public static List<EntityDersler> DersListele()
        {
            List<EntityDersler> degerler = new List<EntityDersler>();
            MySqlCommand cmd2 = new MySqlCommand(
                "SELECT * FROM TBLDERS", Baglanti.bgl);
            if (cmd2.Connection.State != ConnectionState.Open) cmd2.Connection.Open();
            MySqlDataReader rd = cmd2.ExecuteReader();
            while (rd.Read())
            {
                EntityDersler ent = new EntityDersler();

                ent.ID = Convert.ToInt32(rd["DERSID"].ToString());
                ent.DERSAD = rd["DERSAD"].ToString();
                ent.MIN = int.Parse(rd["DERSMINKONTENJAN"].ToString());
                ent.MAX = int.Parse(rd["DERSMAXKONTENJAN"].ToString());
                degerler.Add(ent);
            }
            rd.Close();
            return degerler;
        }

        public static int TalepEkle(EntityBasvuruForm param)
        {
            MySqlCommand cmd6 = new MySqlCommand("INSERT INTO TBLBASVURUFORM (OGRENCIID,DERSID) VALUES (@P1,@P2)",
                Baglanti.bgl);
            cmd6.Parameters.AddWithValue("@P1", param.BASVURUOGRID);
            cmd6.Parameters.AddWithValue("@P2", param.BASVURUDERSID);
            if (cmd6.Connection.State != ConnectionState.Open) cmd6.Connection.Open();
            return cmd6.ExecuteNonQuery();
        }
        
        public static bool DersSil(int param)
        {
            MySqlCommand cmd7 = new MySqlCommand("DELETE FROM TBLDERS WHERE DERSID=@P1", Baglanti.bgl);
            if (cmd7.Connection.State != ConnectionState.Open) cmd7.Connection.Open();
            cmd7.Parameters.AddWithValue("@P1", param);
            return cmd7.ExecuteNonQuery() > 0;
        }
        
        public static bool DersGuncelle(EntityDersler deger)
        {
            MySqlCommand cmd8 =
                new MySqlCommand(
                    "UPDATE TBLDERS SET DERSAD=@P1, DERSMAXKONTENJAN=@P2, DERSMINKONTENJAN=@P3 WHERE DERSID=@P4",Baglanti.bgl);
            if (cmd8.Connection.State != ConnectionState.Open) cmd8.Connection.Open();
            cmd8.Parameters.AddWithValue("@P1", deger.DERSAD);
            cmd8.Parameters.AddWithValue("@P2", deger.MAX);
            cmd8.Parameters.AddWithValue("@P3", deger.MIN);
            cmd8.Parameters.AddWithValue("@P4", deger.ID);
            return cmd8.ExecuteNonQuery() > 0;
        }
        
        public static List<EntityDersler> DersDetay(int id)
        {
            List<EntityDersler> degerler = new List<EntityDersler>();
            MySqlCommand cmd9 = new MySqlCommand(
                "SELECT * FROM TBLDERS WHERE DERSID=@P1", Baglanti.bgl);
            if (cmd9.Connection.State != ConnectionState.Open) cmd9.Connection.Open();
            cmd9.Parameters.AddWithValue("@P1", id);
            MySqlDataReader rd = cmd9.ExecuteReader();
            while (rd.Read())
            {
                EntityDersler ent = new EntityDersler();
                ent.DERSAD = rd["DERSAD"].ToString();
                ent.MIN = Convert.ToInt32(rd["DERSMINKONTENJAN"].ToString());
                ent.MAX = Convert.ToInt32(rd["DERSMAXKONTENJAN"].ToString());
                degerler.Add(ent);
            }
            rd.Close();
            return degerler;
        }
        public static int DersEkle(EntityDersler param)
        {
            var cmd10 = new MySqlCommand(
                "INSERT INTO TBLOGRENCI(DERSAD,DERSMINKONTENJAN,DERSMAXKONTENJAN) VALUES (@p1,@p2,@p3)",
                Baglanti.bgl);
            if (cmd10.Connection.State != ConnectionState.Open) cmd10.Connection.Open();

            cmd10.Parameters.AddWithValue("@p1", param.DERSAD);
            cmd10.Parameters.AddWithValue("@p2", param.MIN);
            cmd10.Parameters.AddWithValue("@p3", param.MAX);
            var sonuc = cmd10.ExecuteNonQuery();

            return sonuc;
        }
    }
}