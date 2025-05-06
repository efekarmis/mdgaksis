using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public static class BLLKullanici
    {
        public static EntityKullanici KullaniciKontrol(string kullaniciID, string sifre, int kullaniciTipi)
        {
            return DALKullanici.KullaniciKontrol(kullaniciID, sifre, kullaniciTipi);
        }

        public static void KullaniciTablosunuKontrolEt()
        {
            DALKullanici.KullaniciTablosunuKontrolEt();
        }
    }
}