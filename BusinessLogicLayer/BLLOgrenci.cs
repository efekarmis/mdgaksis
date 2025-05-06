using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class BLLOgrenci
    {
        public static bool OgrenciGirisBLL(string ogrenciNumara, string ogrenciSifre)
        {
            return DALOgrenci.OgrenciGiris(ogrenciNumara, ogrenciSifre);
        }

        public static int OgrenciEkleBLL(EntityOgrenci param)
        {
            if (param.AD != null && param.SOYAD != null && param.NUMARA != null && param.FOTOGRAF != null &&
                param.SIFRE != null)
                return DALOgrenci.OgrenciEkle(param);

            return -1;
        }

        public static List<EntityOgrenci> BllListele()
        {
            return DALOgrenci.OgrenciListele();
        }

        public static bool OgrenciSilBll(int param)
        {
            if (param >= 0)
            {
                return DALOgrenci.OgrenciSil(param);
            }

            return false;
        }
        public static List<EntityOgrenci> BllDetay(int param)
        {
            return DALOgrenci.OgrenciDetay(param);
        }

        public static bool OgrenciGuncelleBll(EntityOgrenci param)
        {
            if (param.AD != null && param.AD != "" 
                && param.SOYAD != null && param.SOYAD != "" 
                && param.NUMARA != null && param.NUMARA != ""
                && param.FOTOGRAF != null && param.FOTOGRAF != ""
                && param.SIFRE != null && param.SIFRE != ""
                && param.ID > 0)
            {
                return DALOgrenci.OgrenciGuncelle(param);
            }
            return false;
        }

        public static List<EntityDersKayit> OgrenciOnayliDersListesiBll(int ogrenciId)
        {
            return DALOgrenci.OgrenciOnayliDersListesi(ogrenciId);
        }

        public static bool BakiyeEkleBll(int ogrenciId, double eklenecekMiktar)
        {
            if (ogrenciId <= 0)
            {
                Console.WriteLine("BLLOgrenci.BakiyeEkleBll Hata: Geçersiz Öğrenci ID.");
                return false;
            }
            if (eklenecekMiktar <= 0)
            {
                Console.WriteLine("BLLOgrenci.BakiyeEkleBll Hata: Eklenecek miktar pozitif olmalıdır.");
                return false;
            }

            try
            {
                return DALOgrenci.BakiyeGuncelle(ogrenciId, eklenecekMiktar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("BLLOgrenci.BakiyeEkleBll -> DAL Çağrısı Hata: " + ex.Message);
                return false;
            }
        }

        public static bool BakiyeAzaltBll(int ogrenciId, decimal azaltilacakMiktar)
        {
            if (ogrenciId <= 0 || azaltilacakMiktar < 0)
            {
                return false;
            }
            return DALOgrenci.BakiyeAzalt(ogrenciId, azaltilacakMiktar);
        }

        public static List<EntityOgrenci> GetOgrencilerByDersIdBll(int dersId)
        {
            if (dersId <= 0)
            {
                return new List<EntityOgrenci>();
            }
            return DALOgrenci.GetOgrencilerByDersId(dersId);
        }
    }
}