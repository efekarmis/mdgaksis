using System;

namespace EntityLayer
{
    public class EntityMesaj
    {
        public int MesajID { get; set; }
        public int DersID { get; set; }
        public int GonderenKullaniciID { get; set; }
        public int GonderenKullaniciTipi { get; set; }
        public string MesajIcerik { get; set; }
        public DateTime GondermeTarihi { get; set; }
    }
}