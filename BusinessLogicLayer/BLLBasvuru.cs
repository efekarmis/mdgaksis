using System;
using BusinessLogicLayer;
using DataAccessLayer;
using EntityLayer;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;


namespace BusinessLogicLayer
{
    public class BLLBasvuru
    {
        public static List<EntityBasvuruForm> BasvuruListeleBll()
        {
            return DALBasvuru.BasvuruListele();
        }

        public static int OnaylaBasvuru(int basvuruId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var basvuru = DALBasvuru.BasvuruGetir(basvuruId);
                    if (basvuru == null) return -10;

                    int ogrenciId = basvuru.BASVURUOGRID;
                    int dersId = basvuru.BASVURUDERSID;

                    var dersList = BLLDers.BllDersDetay(dersId);
                    if (dersList == null || dersList.Count == 0) return -11;
                    decimal dersUcreti = dersList[0].DERSUCRET;

                    if (dersUcreti > 0)
                    {
                        bool bakiyeAzaltmaSonuc = BLLOgrenci.BakiyeAzaltBll(ogrenciId, dersUcreti);
                        if (!bakiyeAzaltmaSonuc)
                        {
                            return -12;
                        }
                    }

                    DALBasvuru.DersKaydiEkle(ogrenciId, dersId);

                    DALBasvuru.BasvuruSil(basvuruId);

                    scope.Complete();
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("BLLBasvuru.OnaylaBasvuru Hata: " + ex.Message);
                    return -99;
                }
            }
        }

        public static void RedBasvuru(int basvuruId)
        {
            DALBasvuru.BasvuruSil(basvuruId);
        }

        public static int TalepEkleBll(EntityBasvuruForm param)
        {
            if (param == null || param.BASVURUOGRID <= 0 || param.BASVURUDERSID <= 0)
            {
                return -4;
            }

            int ogrenciId = param.BASVURUOGRID;
            int dersId = param.BASVURUDERSID;

            List<EntityBasvuruForm> basvuruListesi = DALBasvuru.BasvuruKontrolListesi();
            bool zatenBasvurmus = basvuruListesi.Any(basvuru =>
                basvuru.BASVURUDERSID == dersId &&
                basvuru.BASVURUOGRID == ogrenciId);

            if (zatenBasvurmus)
            {
                return -1;
            }

            try
            {
                bool zatenKayitli = DALBasvuru.IsOgrenciKayitli(ogrenciId, dersId);
                if (zatenKayitli)
                {
                    return -5;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("BLLBasvuru.TalepEkleBll Mevcut Kayıt Kontrol Hatası: " + ex.Message);
                return -99;
            }

            try
            {
                List<EntityDers> dersDetayList = BLLDers.BllDersDetay(dersId);
                if (dersDetayList == null || dersDetayList.Count == 0)
                {
                    return -3;
                }
                EntityDers ders = dersDetayList[0];
                decimal dersUcreti = ders.DERSUCRET;
                int maxKontenjan = ders.MAX;

                int mevcutKayitSayisi = DALDers.GetDersKayitSayisi(dersId);
                if (mevcutKayitSayisi >= maxKontenjan)
                {
                    return -2;
                }

                if (dersUcreti > 0)
                {
                    List<EntityOgrenci> ogrenciList = BLLOgrenci.BllDetay(ogrenciId);
                    if (ogrenciList == null || ogrenciList.Count == 0)
                    {
                        Console.WriteLine($"BLLBasvuru.TalepEkleBll: Başvuran öğrenci (ID:{ogrenciId}) bulunamadı!");
                        return -4;
                    }
                    EntityOgrenci ogrenci = ogrenciList[0];
                    if (ogrenci.BAKIYE < (double)dersUcreti)
                    {
                        return -7;
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("BLLBasvuru.TalepEkleBll Kontenjan/Bakiye Kontrol Hatası: " + ex.Message);
                return -99;
            }

            return DALBasvuru.TalepEkle(param);
        }
    }
}