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

        static  void Main(string[] args)
        {
            connection = new serverconnection("http://localhost:3000");
            start();
            Console.ReadKey();
            

        }
       
        static async void start()
        {
            Console.WriteLine("Mit szeretnél?");
            string input = Console.ReadLine();
            if (input == "vásárlás")
            {
                Console.WriteLine(input);

                Console.WriteLine("kolbász nevét add meg");
                string name = Console.ReadLine();
                Console.WriteLine("kolbász árát add meg");
                int ara = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("kolbász értékét add meg");
                float ertekeles = float.Parse(Console.ReadLine());
                Console.WriteLine(name);
                Console.WriteLine(ara);
                Console.WriteLine(ertekeles);
                create(name, ara, ertekeles);
               

            }
            else if (input == "nézelődni")
            {
               // Console.WriteLine(input);
                List<jasondata> all = await connection.Kolbaszok();
                foreach (jasondata item in all)
                {
                    Console.WriteLine("asd");
                    Console.WriteLine(item.kolbaszNeve, item.kolbaszAra, item.kolbaszErtekelese);
                }
                kolbik();
            }
            else if (input == "törölni")
            {
                Console.WriteLine(input);
                delkolbik();
            }
            else if(input == "szopni")
            {
                Console.WriteLine("akkor halj éhen");
            }


        }
         public static async void create(string inputname, int inputprice,float inputrating)
        {

            try
            {
                bool temp = await connection.Register(inputname, inputrating, inputprice);
                if (temp)
                {
                    Console.WriteLine("sziaati");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public static async void kolbik()
        {
            List<jasondata> all = await connection.Kolbaszok();
            foreach (jasondata item in all)
            {
                Console.WriteLine(item.kolbaszNeve,item.kolbaszAra,item.kolbaszErtekelese);
            }
        }
        public static async void delkolbik()
        {
            foreach (jasondata item in connection.all)
            {
                bool temp = await connection.deleteklbasz(item.id);
            }
            
        }
    }
}
