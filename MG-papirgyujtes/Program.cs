using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace papirgyujtes
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            string kapcsolat = "server=localhost;user=root;password=mysql;database=papirgyujtes;";
            using (MySqlConnection conn = new MySqlConnection(kapcsolat))
            {
                conn.Open();
                Console.WriteLine("Sikeres kapcsolódás az adatbázishoz!");

                //3. feladat
               
                Console.WriteLine("Készítsen lekérdezést, amely megadja, hogy az első osztályos tanulók mikor és mennyi \npapírt adtak le a gyűjtési időszakban! A lekérdezésben a tanuló neve, osztálya, a leadás \nidőpontja és a leadott papírmennyiség jelenjen meg!");
                Console.WriteLine();

                string parancs = @"
                SELECT tanulok.nev, tanulok.osztaly, leadasok.idopont, leadasok.mennyiseg
                FROM tanulok
                JOIN leadasok ON leadasok.tanulo = tanulok.tazon
                WHERE tanulok.osztaly LIKE '1%';
                ";

                using (MySqlCommand cmd = new MySqlCommand(parancs, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"{"Név",-20} | {"Osztály",-10} | {"Időpont",-20} | {"Mennyiség",10} |");
                    Console.WriteLine(new string('-', 71));

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["nev"],-20} | {reader["osztaly"],-10} | {reader["idopont"],-20} | {reader["mennyiseg"],10} |");
                    }
                }

                Console.WriteLine();
                //4. feladat
                
                //5. feladat
                
                //6. feladat
                
                //7. feldat
                
            }
        }
    }
}
