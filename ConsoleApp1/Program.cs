using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string strConnection2FB = makeCoonectionString2FB();

            connToDBEntity(strConnection2FB);
        }


        private static DbContext connToDBEntity(string sConnectionString)
        {
            try
            {
                using (var dbContent = new DbAppContext(sConnectionString))
                {



                    var simpleQueryOfVidConnects = dbContent.pO_TEL_VID_CONNECTs.Where(s => s.Id > 0);


                    Console.WriteLine("=================================================");
                    foreach (var oneElement in simpleQueryOfVidConnects)
                    {
                        Console.WriteLine(" Id = {0}  Kod связи {1}  Название вида связи {2}", oneElement.Id, oneElement.KodOfConnect, oneElement.Name);
                    }
                    Console.WriteLine("=================================================");




                    /*var simpleVidConnects = dbContent.pO_TEL_VID_CONNECTs;


                    foreach (var oneTEL_VID_CONNECT in simpleVidConnects)
                    {
                        Console.WriteLine(" Id = {0}  Kod связи {1}  Название вида связи {2}", oneTEL_VID_CONNECT.Id, oneTEL_VID_CONNECT.KodOfConnect, oneTEL_VID_CONNECT.Name);

                    }*/



                    return dbContent;
                }

            }
            catch (Exception ex)
            {

            }
            return null;
        }



        private static string makeCoonectionString2FB()
        {

            string strExePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();


            //dbConnectionStringBuilder["ClientLibrary"] = @"C:\Program Files\Firebird\Firebird_2_5\bin\fbclient.dll";

            dbConnectionStringBuilder["Data Source"] = "localhost";
            //dbConnectionStringBuilder["Initial Catalog"] = @"C:\SSG\PROJECTs\TELET\DB4TELEFONE\sampd_cexs.fdb";//"sampd_cexs";
            dbConnectionStringBuilder["Database"] = Path.Combine(strExePath, "tmp.fdb");

            dbConnectionStringBuilder["User ID"] = "sysdba";
            dbConnectionStringBuilder["Password"] = "masterkey";

            dbConnectionStringBuilder["Charset"] = "UTF8";


            dbConnectionStringBuilder["Embedded"] = FbServerType.Embedded;
            //dbConnectionStringBuilder["integrated Security"] = "SSPI";

            return dbConnectionStringBuilder.ConnectionString;

        }


    }
}
