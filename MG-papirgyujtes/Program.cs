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

                parancs = @"
                SELECT leadasok.idopont, AVG(leadasok.mennyiseg) AS atlag
                FROM leadasok
                GROUP BY leadasok.idopont
                ORDER BY leadasok.idopont ASC;
                ";

                using (MySqlCommand cmd = new MySqlCommand(parancs, conn))
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
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("5. feladat:\nKészítsen lekérdezést, amely kilistázza, mely osztályokból adtak le papírt 2016. október 28-\r\nán! A listában minden osztály azonosítója csak egyszer szerepeljen növekvő sorrendben!");
                Console.WriteLine();

                parancs = @"
                SELECT tanulok.osztaly
                FROM tanulok
                JOIN leadasok ON leadasok.tanulo = tanulok.tazon
                GROUP BY tanulok.osztaly;
                ";

                using (MySqlCommand cmd = new MySqlCommand(parancs, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"| {"Osztály",-10} |");
                    Console.WriteLine(new string('-', 14));

                    while (reader.Read())
                    {
                        Console.WriteLine($"| {reader["osztaly"],-10} |");
                    }
                }
                Console.WriteLine(new string('-', 14));
                Console.WriteLine();


                //6. feladat

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("6. feladat:\nKészítsen lekérdezést, amely megadja, hogy osztályonként hány mázsa papírt gyűjtöttek a \ntanulók (1 mázsa = 10000 dkg)! Az eredményt rendezze a gyűjtött mennyiség szerint \ncsökkenő rendbe!");
                Console.WriteLine();

                parancs = @"
                SELECT tanulok.osztaly, SUM(leadasok.mennyiseg) / 10000 AS mazsa
                FROM tanulok
                JOIN leadasok ON leadasok.tanulo = tanulok.tazon
                GROUP BY tanulok.osztaly
                ORDER BY mazsa DESC;
                ";

                using (MySqlCommand cmd = new MySqlCommand(parancs, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"| {"Osztály",-30} | {"Mázsa",-20} |");
                    Console.WriteLine(new string('-', 57));

                    while (reader.Read())
                    {
                        Console.WriteLine($"| {reader["osztaly"],-30} | {reader["mazsa"],-20} |");
                    }
                }
                Console.WriteLine(new string('-', 57));
                Console.WriteLine();


                //7. feldat

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("7. feladat: A legtöbb papírt gyűjtő 10 tanuló jutalomban részesül. Készítsen lekérdezést, amely \nmegadja ezen tanulók nevét, osztályát és azt, hogy mekkora mennyiségű papírt gyűjtöttek! \nAz eredményt rendezze a gyűjtött mennyiség szerint csökkenő rendbe!");
                Console.WriteLine();

                parancs = @"
                SELECT tanulok.nev, tanulok.osztaly, SUM(leadasok.mennyiseg) AS osszesen
                FROM tanulok
                JOIN leadasok ON leadasok.tanulo = tanulok.tazon
                GROUP BY tanulok.nev, tanulok.osztaly
                ORDER BY osszesen DESC
                LIMIT 10;
                ";

                using (MySqlCommand cmd = new MySqlCommand(parancs, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"| {"Név",-20} | {"Osztály",-20} | {"Összesen",-20} |");
                    Console.WriteLine(new string('-', 70));

                    while (reader.Read())
                    {
                        Console.WriteLine($"| {reader["nev"],-20} | {reader["osztaly"],-20} | {reader["osszesen"],-20} |");
                    }
                }
                Console.WriteLine(new string('-', 70));
                Console.WriteLine();

            }
        }
    }
}
