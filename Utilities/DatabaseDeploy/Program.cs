using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac;

namespace DatabaseDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DeployTestDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        static void DeployTestDatabase()
        {
            
            DacServices sc = new DacServices(@"Server=DESKTOP-D0NSBJ1\SQLEXPRESS;Database=PersonalWebsiteDb_Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            DacDeployOptions options = new DacDeployOptions();
            options.CreateNewDatabase = true;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PersonalWebsite.Database.dacpac");

            Console.WriteLine("Deploying database ...");
            sc.Deploy(DacPackage.Load(path), "PersonalWebsiteDb_Test", true, options);
            Console.WriteLine("Database deployed.");
            
        }
    }
}
