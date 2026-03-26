using FlexiDesk.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Infrastructure.Factories
{
    public class SqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory<SqlConnection>
    {
        private readonly IConfiguration _configuration = configuration;
        public SqlConnection Get(string destination)
        {
            SqlConnection connection = null;
            string connectionString = "";
            switch (destination) 
            {
                case "FlexiDesk":
                #if TARGET_LINUX && !TARGET_ANDROID
                #if DEBUG
                    connectionString =_configuration.GetSection("DB:FlexiDesk:LinuxDev").Value;
                    return new SqlConnection(_configuration.GetConnectionString(connectionString);
                #else
                    connectionString =_configuration.GetSection("DB:FlexiDesk:LinuxProd").Value;
                    return new SqlConnection(_configuration.GetConnectionString("connectionString"));
                #endif
                #else
                #if DEBUG
                    connectionString = _configuration.GetSection("DB:FlexiDesk:WindowsDev").Value;
                    return new SqlConnection(connectionString);

                #else
                    connectionString = _configuration.GetSection("DB:FlexiDesk:WindowsProd").Value;
                    return new SqlConnection(connectionString);
                #endif
                #endif
                    break;

            }
            return connection;
        }
    }
}
