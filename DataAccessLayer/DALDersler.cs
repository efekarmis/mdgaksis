using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using EntityLayer;
using MySql.Data.MySqlClient;


namespace DataAccessLayer
{
    public class DALDersler
    {
        
        public static List<EntityDersler> DersListele()
        {
            List<EntityDersler> degerler = new List<EntityDersler>();
            MySqlCommand cmd2 = new MySqlCommand(@"
        SELECT 
            TBLDERS.DERSID, 
            TBLDERS.DERSAD, 
            TBLDERS.DERSMINKONTENJAN, 
            TBLDERS.DERSMAXKONTENJAN, 
            TBLDERS.OGRETMENID,
            TBLOGRETMEN.OGRTADSOYAD
        FROM 
            TBLDERS
        LEFT JOIN 
            TBLOGRETMEN ON TBLDERS.OGRETMENID = TBLOGRETMEN.OGRTID;
    ", Baglanti.bgl);
            if (cmd2.Connection.State != ConnectionState.Open) cmd2.Connection.Open();
            MySqlDataReader rd = cmd2.ExecuteReader();
            while (rd.Read())
            {
                EntityDersler ent = new EntityDersler();
                EntityOgretmen ent2 = new EntityOgretmen();

                ent.ID = Convert.ToInt32(rd["DERSID"]);
                ent.DERSAD = rd["DERSAD"].ToString();
                ent.MIN = Convert.ToInt32(rd["DERSMINKONTENJAN"]);
                ent.MAX = Convert.ToInt32(rd["DERSMAXKONTENJAN"]);
                ent.OGRETMENID = Convert.ToInt32(rd["OGRETMENID"]);
                ent2.OGRTADSOYAD = rd["OGRTADSOYAD"].ToString();

                ent.OGRETMEN = ent2;
                
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
        MySqlCommand checkCmd = new MySqlCommand(
            "SELECT COUNT(*) FROM TBLOGRETMEN WHERE OGRTID = @P1", Baglanti.bgl);
        checkCmd.Parameters.AddWithValue("@P1", deger.OGRETMENID);
        
        if (checkCmd.Connection.State != ConnectionState.Open) checkCmd.Connection.Open();
        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
        if (count == 0) return false; // Hatalı OGRETMENID
        
        MySqlCommand cmd8 =
            new MySqlCommand(
                @" UPDATE TBLDERS
                              SET DERSAD = @P1, OGRETMENID = @P2
                              WHERE DERSID = @P3;
            ",Baglanti.bgl);
            if (cmd8.Connection.State != ConnectionState.Open) cmd8.Connection.Open();
            cmd8.Parameters.AddWithValue("@P1", deger.DERSAD);
            cmd8.Parameters.AddWithValue("@P2", deger.OGRETMENID);
            cmd8.Parameters.AddWithValue("@P3", deger.ID);
            
            return cmd8.ExecuteNonQuery() > 0;
        }
        
        public static List<EntityDersler> DersDetay(int id)
        {
            List<EntityDersler> degerler = new List<EntityDersler>();

            // Güncellenmiş SQL sorgusu
            MySqlCommand cmd9 = new MySqlCommand(@"
    SELECT 
        TBLDERS.DERSID, 
        TBLDERS.DERSAD, 
        TBLDERS.DERSMINKONTENJAN, 
        TBLDERS.DERSMAXKONTENJAN, 
        TBLDERS.OGRETMENID,
        TBLOGRETMEN.OGRTADSOYAD
    FROM 
        TBLDERS
    LEFT JOIN 
        TBLOGRETMEN ON TBLDERS.OGRETMENID = TBLOGRETMEN.OGRTID
    WHERE 
        TBLDERS.DERSID = @P1;
",Baglanti.bgl);
            
            // Bağlantıyı kontrol et ve aç
            if (cmd9.Connection.State != ConnectionState.Open)
                cmd9.Connection.Open();

            // Parametreyi ekle
            cmd9.Parameters.AddWithValue("@P1", id);

            // Sorguyu çalıştır ve sonuçları oku
            MySqlDataReader rd = cmd9.ExecuteReader();
            while (rd.Read())
            {
                EntityDersler ent = new EntityDersler();
                EntityOgretmen ent2 = new EntityOgretmen();

                // Veritabanından gelen değerleri nesnelere atama
                ent.ID = Convert.ToInt32(rd["DERSID"]);
                ent.DERSAD = rd["DERSAD"].ToString();
                ent.MIN = Convert.ToInt32(rd["DERSMINKONTENJAN"]);
                ent.MAX = Convert.ToInt32(rd["DERSMAXKONTENJAN"]);
                ent.OGRETMENID = Convert.ToInt32(rd["OGRETMENID"]);
                ent2.OGRTADSOYAD = rd["OGRTADSOYAD"].ToString();
                
                ent.OGRETMEN = ent2;
                
                degerler.Add(ent);
            }
            
            rd.Close();
            
            
            
            
            return degerler;
        }
        public static int DersEkle(EntityDersler param)
        {
            var cmd10 = new MySqlCommand(
                "INSERT INTO TBLDERS(DERSAD, DERSMINKONTENJAN, DERSMAXKONTENJAN, OGRETMENID) VALUES (@DersAd, @Min, @Max, @OgretmenId)",
                Baglanti.bgl);
            if (cmd10.Connection.State != ConnectionState.Open) cmd10.Connection.Open();

            cmd10.Parameters.AddWithValue("@DersAd", param.DERSAD);
            cmd10.Parameters.AddWithValue("@Min", param.MIN);
            cmd10.Parameters.AddWithValue("@Max", param.MAX);
            cmd10.Parameters.AddWithValue("@OgretmenId", param.OGRETMENID);

            var sonuc = cmd10.ExecuteNonQuery();
            return sonuc;
            
        }
    }
}