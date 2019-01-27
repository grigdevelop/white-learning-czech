using System;
using System.Collections.Generic;
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
            DeployTestDatabase();
            Console.ReadLine();
        }

        static void DeployTestDatabase()
        {
            
            DacServices sc = new DacServices(@"Server=DESKTOP-D0NSBJ1\SQLEXPRESS;Database=PersonalWebsiteDb_Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            DacDeployOptions options = new DacDeployOptions();
            options.CreateNewDatabase = true;

            Console.WriteLine("Deploying database ...");
            sc.Deploy(DacPackage.Load("PersonalWebsite.Database.dacpac"), "PersonalWebsiteDb_Test", true, options);
            Console.WriteLine("Database deployed.");
            
        }
    }
}
