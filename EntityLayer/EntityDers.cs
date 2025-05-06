namespace EntityLayer
{
    public class EntityDers
    {
        public string DERSAD { get; set; }

        public int MIN { get; set; }

        public int MAX { get; set; }

        public int ID { get; set; }
        
        public int OGRETMENID { get; set; }
        
        public EntityOgretmen OGRETMEN { get; set; }

        public decimal DERSUCRET { get; set; }

        public string DERSACIKLAMA { get; set; }
    }
}