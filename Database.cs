using System;
using System.Configuration;

// Không sử dụng namespace để có thể truy cập từ mọi nơi trong dự án
public static class Database
{
    public static string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        }
    }
}
