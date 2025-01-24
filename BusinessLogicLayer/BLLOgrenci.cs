using DataAccessLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class BLLOgrenci
    {
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
    }
}