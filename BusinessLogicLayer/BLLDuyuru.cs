using System;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public static class BLLDuyuru
    {
        public static List<EntityDuyuru> AktifDuyurulariGetirBLL(int kullaniciTipi)
        {
            if (kullaniciTipi != 0 && kullaniciTipi != 1)
            {
            }
            return DALDuyuru.AktifDuyurulariGetir(kullaniciTipi);
        }

        public static List<EntityDuyuru> TumDuyurulariGetirBLL()
        {
            return DALDuyuru.TumDuyurulariGetir();
        }

        public static int DuyuruEkleBLL(EntityDuyuru duyuru)
        {
            if (duyuru == null || string.IsNullOrWhiteSpace(duyuru.Baslik) || string.IsNullOrWhiteSpace(duyuru.Icerik))
            {
                return -2;
            }
            if (duyuru.HedefKitle < 0 || duyuru.HedefKitle > 2)
            {
                duyuru.HedefKitle = 2;
            }
            if (duyuru.OnemDerecesi < 0 || duyuru.OnemDerecesi > 1)
            {
                duyuru.OnemDerecesi = 0;
            }
            return DALDuyuru.DuyuruEkle(duyuru);
        }

        public static bool DuyuruSilBLL(int duyuruId)
        {
            if (duyuruId <= 0) return false;
            return DALDuyuru.DuyuruSil(duyuruId);
        }

        public static EntityDuyuru DuyuruGetirBLL(int duyuruId)
        {
            if (duyuruId <= 0) return null;
            return DALDuyuru.DuyuruGetir(duyuruId);
        }

        public static bool DuyuruGuncelleBLL(EntityDuyuru duyuru)
        {
            if (duyuru == null || duyuru.DuyuruID <= 0 || string.IsNullOrWhiteSpace(duyuru.Baslik) || string.IsNullOrWhiteSpace(duyuru.Icerik))
            {
                return false;
            }
            if (duyuru.HedefKitle < 0 || duyuru.HedefKitle > 2)
            {
                return false;
            }
            if (duyuru.OnemDerecesi < 0 || duyuru.OnemDerecesi > 1)
            {
                return false;
            }

            return DALDuyuru.DuyuruGuncelle(duyuru);
        }

        public static List<EntityDuyuru> TumOgrenciDuyurulariGetirBLL()
        {
            return DALDuyuru.TumOgrenciDuyurulariGetir();
        }
    }
}