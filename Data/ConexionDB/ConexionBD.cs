using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using DAO.ConexionDB;

namespace DAO.ConexionDB
{
    public class ConexionBD : IConexionBD
    {        
        private static IConfigurationRoot configuration;

        public ConexionBD()
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();
        }

        public string GetDbConnection()
        {
            return configuration.GetSection("ConnectionStrings").GetSection("ContPaqi001").Value;
        }
    }
}
