using System;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;
using System.Linq;

namespace BusinessLogicLayer
{
    public static class BLLMesaj
    {
        public static int MesajEkleBLL(EntityMesaj mesaj, int sessionKullaniciID, int sessionKullaniciTipi)
        {
            if (mesaj == null || string.IsNullOrWhiteSpace(mesaj.MesajIcerik) || mesaj.DersID <= 0)
            {
                return -2;
            }

            if (mesaj.GonderenKullaniciID != sessionKullaniciID || mesaj.GonderenKullaniciTipi != sessionKullaniciTipi)
            {
                System.Diagnostics.Debug.WriteLine($"Yetkisiz Mesaj Gönderme Denemesi! SessionID: {sessionKullaniciID}, GelenID: {mesaj.GonderenKullaniciID}");
                return -3;
            }

            bool yetkiliMi = false;
            if (sessionKullaniciTipi == 0)
            {
                yetkiliMi = DALBasvuru.IsOgrenciKayitli(sessionKullaniciID, mesaj.DersID);
            }
            else if (sessionKullaniciTipi == 1)
            {
                List<EntityDers> ogretmeninDersleri = DALDers.GetDerslerByOgretmenId(sessionKullaniciID);
                yetkiliMi = ogretmeninDersleri?.Any(d => d.ID == mesaj.DersID) ?? false;
            }

            if (!yetkiliMi)
            {
                System.Diagnostics.Debug.WriteLine($"Yetkisiz Mesaj Gönderme Denemesi! KullaniciID: {sessionKullaniciID}, DersID: {mesaj.DersID}");
                return -3;
            }

            return DALMesaj.MesajEkle(mesaj);
        }


        public static List<EntityMesaj> GetMesajlarByDersIdBLL(int dersId, int sessionKullaniciID, int sessionKullaniciTipi)
        {
            bool yetkiliMi = false;
            if (sessionKullaniciTipi == 0)
            {
                yetkiliMi = DALBasvuru.IsOgrenciKayitli(sessionKullaniciID, dersId);
            }
            else if (sessionKullaniciTipi == 1)
            {
                List<EntityDers> ogretmeninDersleri = DALDers.GetDerslerByOgretmenId(sessionKullaniciID);
                yetkiliMi = ogretmeninDersleri?.Any(d => d.ID == dersId) ?? false;
            }

            if (!yetkiliMi)
            {
                System.Diagnostics.Debug.WriteLine($"Yetkisiz Mesaj Görüntüleme Denemesi! KullaniciID: {sessionKullaniciID}, DersID: {dersId}");
                return new List<EntityMesaj>();
            }

            return DALMesaj.GetMesajlarByDersId(dersId);
        }
    }
}