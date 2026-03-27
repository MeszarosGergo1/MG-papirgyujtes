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
                Console.WriteLine();
                Console.WriteLine();
                //3. feladat

                Console.WriteLine("3. feladat: \nKészítsen lekérdezést, amely megadja, hogy az első osztályos tanulók mikor és mennyi \npapírt adtak le a gyűjtési időszakban! A lekérdezésben a tanuló neve, osztálya, a leadás \nidőpontja és a leadott papírmennyiség jelenjen meg!");
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
                Console.WriteLine(new string('-', 71));
                Console.WriteLine();


                //4. feladat
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("4. feladat: \nListázza ki, hogy az átvételre megjelölt napokon átlagosan mennyi papírt adtak le! A \nszámított mező címkéje „napi atlag” legyen!");
                Console.WriteLine();

                string sql_script = @"
                SELECT leadasok.idopont, AVG(leadasok.mennyiseg) AS atlag
                FROM leadasok
                GROUP BY leadasok.idopont
                ORDER BY leadasok.idopont ASC;
                ";

                using (MySqlCommand cmd = new MySqlCommand(sql_script, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"| {"Időpont",-30} | {"Napi átlag",-20} |");
                    Console.WriteLine(new string('-', 57));

                    while (reader.Read())
                    {
                        Console.WriteLine($"| {reader["idopont"],-30} | {reader["atlag"],-20} |");
                    }
                }
                Console.WriteLine(new string('-', 57));
                Console.WriteLine();
                //5. feladat

                //6. feladat

                //7. feldat

            }
        }
    }
}
