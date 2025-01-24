using System;
using DataAccessLayer;
using EntityLayer;
using System.Collections.Generic;


namespace BusinessLogicLayer
{
    public class BLLDersler
    {
        public static List<EntityDersler> BllListele()
        {
            return DALDersler.DersListele();
        }

        public static int TalepEkleBll(EntityBasvuruForm param)
        {
            if (param.BASVURUOGRID != null && param.BASVURUDERSID != null)
            {
                return DALDersler.TalepEkle(param);
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
            return DALDersler.DersDetay(param);
        }

        public static bool DersGuncelleBll(EntityDersler param)
        {
            if (!String.IsNullOrEmpty(param.DERSAD) 
                && param.OGRETMENID > 0
                && param.ID > 0
                )
            {
                return DALDersler.DersGuncelle(param);
            }
            return false;
        }
        
        public static int DersEkleBLL(EntityDersler param)
        {
            if (param.DERSAD != null && param.MIN != 0 && param.MAX != 0)
                return DALDersler.DersEkle(param);
            return -1;
        }
    }
}