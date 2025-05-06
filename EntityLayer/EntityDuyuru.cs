using System;

namespace EntityLayer
{
    public class EntityDuyuru
    {
        public int DuyuruID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public int HedefKitle { get; set; }
        public short OnemDerecesi { get; set; }
        public DateTime YayinTarihi { get; set; }
    }
}