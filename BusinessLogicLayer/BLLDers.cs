using System;
using DataAccessLayer;
using EntityLayer;
using System.Collections.Generic;


namespace BusinessLogicLayer
{
    public class BLLDers
    {
        public static List<EntityDers> BllListele()
        {
            return DALDers.DersListele();
        }

        public static bool DersSilBll(int param)
        {
            if (param >= 0)
            {
                return DALDers.DersSil(param);
            }

            return false;
        }

        public static List<EntityDers> BllDersDetay(int param)
        {
            return DALDers.DersDetay(param);
        }

        public static bool DersGuncelleBll(EntityDers param)
        {
            if (!String.IsNullOrEmpty(param.DERSAD) && param.ID > 0 &&
                param.MIN >= 0 && param.MAX >= param.MIN && param.DERSUCRET >= 0)
            {
                return DALDers.DersGuncelle(param);
            }
            return false;
        }

        public static int DersEkleBLL(EntityDers param)
        {
            if (param.DERSAD != null && param.MIN >= 0 && param.MAX >= param.MIN && param.DERSUCRET >= 0)
                return DALDers.DersEkle(param);
            return -1;
        }

        public static List<EntityDers> GetDerslerByOgretmenIdBll(int ogretmenId)
        {
            if (ogretmenId <= 0)
            {
                return new List<EntityDers>();
            }
            return DALDers.GetDerslerByOgretmenId(ogretmenId);
        }
    }
}