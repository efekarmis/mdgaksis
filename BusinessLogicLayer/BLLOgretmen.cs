using DataAccessLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class BLLOgretmen
    {
        public static int OgretmenEkleBLL(EntityOgretmen param)
        {
            if (param.OGRTADSOYAD != null)
                return DALOgretmen.OgretmenEkle(param);

            return -1;
        }

        public static List<EntityOgretmen> BllListele()
        {
            return DALOgretmen.OgretmenListele();
        }

        public static bool OgretmenSilBll(int param)
        {
            if (param >= 0)
            {
                return DALOgretmen.OgretmenSil(param);
            }

            return false;
        }
        public static List<EntityOgretmen> BllDetay(int param)
        {
            return DALOgretmen.OgretmenDetay(param);
        }

        public static bool OgretmenGuncelleBll(EntityOgretmen param)
        {
            if (param.OGRTADSOYAD != null && param.OGRTADSOYAD != "" 
                && param.OGRTID > 0)
            {
                return DALOgretmen.OgretmenGuncelle(param);
            }
            return false;
        }
    }
}