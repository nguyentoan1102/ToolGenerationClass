using System.Data.SqlClient;

namespace GenerationClass.Code
{
    internal class test
    {
        public void RunCodeSQL(string sql)
        {
            string a = (3 == 5) ? "" : "1";
            SqlParameter[] arr;
            SqlCommand cmd = new SqlCommand(sql);
            cmd.ExecuteNonQuery();
        }
    }
}