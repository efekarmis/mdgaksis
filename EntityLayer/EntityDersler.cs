namespace EntityLayer
{
    public class EntityDersler
    {
        public string DERSAD { get; set; }

        public int MIN { get; set; }

        public int MAX { get; set; }

        public int ID { get; set; }
        
        public int OGRETMENID { get; set; }
        
        public EntityOgretmen OGRETMEN { get; set; }
    }
}