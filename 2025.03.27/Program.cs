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
                    
                    //Console.WriteLine(item.kolbaszNeve);
                }
                kolbik();
            }
            else if (input == "törölni")
            {
                Console.Write("Kolbász ID-je: ");
                int id = Convert.ToInt32(Console.ReadLine());
                deleteKolbasz(id);
            }
            else if (input == "szopni")
            {
                Console.WriteLine("akkor halj éhen");
            }


        }
        public static async void create(string name, int price, float rating)
        {

            try
            {
                bool temp = await connection.Register(name, price,rating);
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
                Console.WriteLine(item.kolbaszNeve +','+item.kolbaszAra+',' + item.kolbaszErtekelese);
                
            }
        }
       public  static async void deleteKolbasz(int id)
        {
            try
            {
                bool temp = await connection.deleteKolbi(id);
                if (temp)
                {
                    
                    Console.WriteLine("Sikeres törlés");
                   

                }
            }
            catch (Exception e)
            {
               
                Console.WriteLine(e.Message);
                
            }
        }
    }
}