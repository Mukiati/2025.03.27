using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025._03._27
{
    class Program
    {
        static serverconnection connection;
        static void Main(string[] args)
        {
            
            start();
            Console.ReadKey();
            connection = new serverconnection("http://localhost:3000");

        }
        static string name;
        static int ara;
        static float ertekeles;
        static void start()
        {
            Console.WriteLine("Mit szeretnél?");
            string input = Console.ReadLine();
            if (input == "vásárlás")
            {
                Console.WriteLine(input);

                Console.WriteLine("kolbász nevét add meg");
                name = Console.ReadLine();
                Console.WriteLine("kolbász árát add meg");
                 ara = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("kolbász értékét add meg");
                ertekeles = float.Parse(Console.ReadLine());
                Console.WriteLine(name);
                Console.WriteLine(ara);
                Console.WriteLine(ertekeles);
                create(name, ara, ertekeles);
               

            }
            else if (input == "nézelődni")
            {
                Console.WriteLine(input);
                kolbik();
            }
            else if (input == "törölni")
            {
                Console.WriteLine(input);
            }


        }
         public static async void create(string inputname, int inputprice,float inputrating)
        {
            try
            {
                bool temp = await connection.Register( inputprice, inputname,inputrating);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        static async void kolbik()
        {
            List<jasondata> all = await connection.Kolbaszok();
            foreach (jasondata item in all)
            {
                Console.WriteLine(item.kolbaszNeve,item.kolbaszAra,item.kolbaszErtekelese);
            }
        }
    }
}
