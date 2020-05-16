using System;
using System.Collections.Generic;

namespace M1_P1 {
    class Program {
        static void Main(string[] args) {
            /**
            var m = new Osoba();
            var k = new Osoba();
            m.Nazwisko = "Kowalski";
            m.Imie = "Jan";
            k.Nazwisko = "Nowak";
            k.Imie = "Anna";
            m.UstawRokUrodzenia(1985);
            k.UstawRokUrodzenia(1989);
            Console.WriteLine("k - {0}", k);
            Console.WriteLine("m - {0}", m);
            Console.WriteLine("Naciśnij dowolny klawisz");
            Console.ReadKey();
            var dziecko = new Osoba();
            Console.WriteLine(DateTime.Now.Year);
            // Console.ReadKey();
            // dziecko.UstawRokUrodzenia(DateTime.Now.Year - 16); //zgloszeniewyjatku osoba niepełnoletnia
            **/
            var stworzeniStudenci = new List<Student>();
            for (var i = 1; i <= 4; i++)
            {
                try
                {
                    Console.WriteLine($"Rozpoczeto tworzenie studenta - {i}");
                    var student = Student.KonsolowyKreatorStudenta.StworzStudenta();
                    Console.WriteLine($"student {i} - {student}");
                    stworzeniStudenci.Add(student);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine($"Stworzeni studenci:");
            foreach (var student in stworzeniStudenci)
            {
                Console.WriteLine(student);
            }
        }
    }
}