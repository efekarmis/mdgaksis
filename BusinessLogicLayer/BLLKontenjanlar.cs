using System;
using DataAccessLayer;
using EntityLayer;
using System.Collections.Generic;


namespace BusinessLogicLayer
{
    public class BLLKontenjanlar
    {
        public static List<EntityDersler> BllListele()
        {
            return DALKontenjanlar.DersListele();
        }

        public static int TalepEkleBll(EntityBasvuruForm param)
        {
            if (param.BASVURUOGRID != null && param.BASVURUDERSID != null)
            {
                return DALKontenjanlar.TalepEkle(param);
            }

            return -1;
        }
        public static bool DersSilBll(int param)
        {
            if (param >= 0)
            {
                return DALDersler.DersSil(param);
            }

            return false;
        }
        public static List<EntityDersler> BllDersDetay(int param)
        {
            return DALKontenjanlar.DersDetay(param);
        }

        public static bool DersGuncelleBll(EntityDersler param)
        {
            if (!String.IsNullOrEmpty(param.DERSAD) 
                && param.MIN != 0 
                && param.MAX != 0 
                && param.ID > 0
                && param.MIN < param.MAX)
            {
                return DALKontenjanlar.DersGuncelle(param);
            }
            return false;
        }
        
        public static int DersEkleBLL(EntityDersler param)
        {
            if (param.DERSAD != null && param.MIN != 0 && param.MAX != 0)
                return DALKontenjanlar.DersEkle(param);
            return -1;
        }
    }
}