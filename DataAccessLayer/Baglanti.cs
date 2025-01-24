using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class Baglanti
    {
        public static MySqlConnection bgl =
            new MySqlConnection(@"server=127.0.0.1;uid=root;pwd=ff3AE6q_x9ik;database=dbyazokulu");
    }
}