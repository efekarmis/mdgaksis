namespace EntityLayer
{
    public class EntityOgretmen
    {
        public int OGRTID { get; set; }
        public string OGRTAD { get; set; }
        public string OGRTSOYAD { get; set; }
        public string OGRTFOTOGRAF { get; set; }
        public string OGRTSIFRE { get; set; }

        public string OGRTADSOYAD
        {
            get { return OGRTAD + " " + OGRTSOYAD; }
        }
    }
}