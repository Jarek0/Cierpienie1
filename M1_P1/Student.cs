using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using static System.Int32;

namespace M1_P1
{
    public class Student: Osoba
    {
        private readonly Regex formatNumeruAlbumu = new Regex(@"^\d{4,8}$");
        private readonly Regex formatKwotaStypendium = new Regex(@"^\d+(\.\d{1,2})?$");
        
        private int numerAlbumu;
        private bool czyPobieraStypendiumNaukowe;
        private decimal kwotaStypendiumNaukowego;
        private bool czyUprawiaSport;
        private string uprawianaDyscyplinaSportowa;

        // konstruktor jest prywatny, aby nie mozna bylo stworzyc nieprawidlowego studenta (studenta mozna stworzyc tylko przy uzyciu kretora)
        private Student() { }
        
        public void UstawNumerAlbumu(string podanyNumerAlbumu)
        {
            if (!formatNumeruAlbumu.IsMatch(podanyNumerAlbumu))
            {
                throw new ArgumentException("Podany numer albumu ma nieprawidlowy format (Numer albumu musi skladac sie z od 4 do 8 cyfr)");
            }
            numerAlbumu = Parse(podanyNumerAlbumu);
        }

        public void UstawCzyPobieraStypendiumNaukowe(bool podaneCzyPobieraStypendiumNaukowe)
        {
            czyPobieraStypendiumNaukowe = podaneCzyPobieraStypendiumNaukowe;
        }

        public void UstawKwoteStypendium(decimal podanaKwotaStypendiumNaukowego)
        {
            if (!czyPobieraStypendiumNaukowe)
            {
                throw new ArgumentException("Student nie pobiera stypendium naukowego");
            }
            if (!formatKwotaStypendium.IsMatch(podanaKwotaStypendiumNaukowego.ToString(CultureInfo.InvariantCulture)))
            {
                throw new ArgumentException("Nieprawidlowy format waluty");
            }

            this.kwotaStypendiumNaukowego = podanaKwotaStypendiumNaukowego;
        }
        
        public void UstawyCzyUprawiaSport(bool podaneCzyUprawiaSport)
        {
            czyUprawiaSport = podaneCzyUprawiaSport;
        }
        
        public void UstawUprawianaDysciplineSportowa(string podanaUprawianaDyscyplinaSportowa)
        {
            if (!czyUprawiaSport)
            {
                throw new ArgumentException("Student nie uprawia sportu");
            }

            this.uprawianaDyscyplinaSportowa = podanaUprawianaDyscyplinaSportowa;
        }
        
        public override string ToString()
        {
            var informacjeOStypendiumNaukowym = "ktory/a " + 
                                                (czyPobieraStypendiumNaukowe ? 
                                                    $"pobiera stypendium naukowe w wysokosci: {kwotaStypendiumNaukowego} zl" : 
                                                    "nie pobiera stypendium naukowego");
            var informacjeOUprawianymSporcie = "i ktory/a " + 
                                                (czyUprawiaSport ? 
                                                    $"uprawia sport: {uprawianaDyscyplinaSportowa}" : 
                                                    "nie uprawia zadnego sportu");
            return $"{base.ToString()}, numer albumu: {numerAlbumu}, {informacjeOStypendiumNaukowym} {informacjeOUprawianymSporcie}.";
        }
        
        public class KonsolowyKreatorStudenta
        {
            private static readonly Regex formatWartosciBool = new Regex(@"^[T|N]{1}$");

            public static Student StworzStudenta()
            {
                Console.WriteLine("Otworzyles konsolowy kreator studenta");
                var tworzonyStudent = new Student();
                UstawDanaStudentaZKonsoli(
                    podaneImie => tworzonyStudent.Imie = podaneImie,
                    "Podaj imie studenta i wcisnij Enter. (ESC aby wyjsc):",
                    "Nie udalo sie podac imienia studenta",
                    "Podano imie studenta"
                );
                UstawDanaStudentaZKonsoli(
                    podaneNazwisko => tworzonyStudent.Nazwisko = podaneNazwisko,
                    "Podaj nazwisko studenta i wcisnij Enter. (ESC aby wyjsc):",
                    "Nie udalo sie podac nazwiska studenta",
                    "Podano nazwisko studenta"
                );
                UstawDanaStudentaZKonsoli(
                    podanyRokUrodzenia => tworzonyStudent.UstawRokUrodzenia(
                        konwertujDanaCalkowita(podanyRokUrodzenia)
                        ),
                    "Podaj rok urodzenia studenta i wcisnij Enter. (ESC aby wyjsc):",
                    "Nie udalo sie podac roku urodzenia studenta",
                    "Podano rok urodzenia studenta studenta"
                );
                UstawDanaStudentaZKonsoli(
                    podanyNumerAlbumu => tworzonyStudent.UstawNumerAlbumu(podanyNumerAlbumu),
                    "Podaj numer albumu studenta i wcisnij Enter. (ESC aby wyjsc):",
                    "Nie udalo sie podac numeru albumu studenta",
                    "Podano numer albumu studenta"
                );
                UstawDanaStudentaZKonsoli(
                    podaneCzyPobieraStypendiumNaukowe => 
                        tworzonyStudent.UstawCzyPobieraStypendiumNaukowe(
                            konwertujDanaBool(podaneCzyPobieraStypendiumNaukowe)
                        ),
                    "Podaj informacje czy student pobiera stypendium naukowe (wartosc T lub N) i wcisnij Enter. (ESC aby wyjsc):",
                    "Nie udalo sie podac informacji czy student pobiera stypendium naukowe",
                    "Podano informacje czy student pobiera stypendium naukowe"
                );
                if (tworzonyStudent.czyPobieraStypendiumNaukowe)
                {
                    UstawDanaStudentaZKonsoli(
                        podanaKwotaStypendiumNaukowego => tworzonyStudent.UstawKwoteStypendium(
                            konwertujDanaZmiennoprzecinkowa(podanaKwotaStypendiumNaukowego)
                            ),
                        "Podaj kwote stypendium naukowego i wcisnij Enter. (ESC aby wyjsc):",
                        "Nie udalo sie podac kwoty stypendium naukowego",
                        "Podano kwote stypendium naukowego"
                    );
                }
                UstawDanaStudentaZKonsoli(
                    podaneCzyUprawiaSport => 
                        tworzonyStudent.UstawyCzyUprawiaSport(
                            konwertujDanaBool(podaneCzyUprawiaSport)
                        ),
                    "Podaj informacje czy student uprawia sport (wartosc T lub N) i wcisnij Enter. (ESC aby wyjsc):",
                    "Nie udalo sie podac informacji czy student uprawia sport",
                    "Podano informacje czy student uprawia sport"
                );
                if (tworzonyStudent.czyUprawiaSport)
                {
                    UstawDanaStudentaZKonsoli(
                        podanaUprawianaDyscyplinaSportowa => tworzonyStudent.UstawUprawianaDysciplineSportowa(
                            podanaUprawianaDyscyplinaSportowa
                        ),
                        "Podaj uprawiana dysciplne sportowa przez studenta i wcisnij Enter. (ESC aby wyjsc):",
                        "Nie udalo sie podac uprawianej dyscypliny sportowej przez studenta",
                        "Podano dyscipline sportowa uprawiana przez studenta"
                    );
                }
                return tworzonyStudent;
            }

            private static bool konwertujDanaBool(string podanaWartoscBool)
            {
                if (!formatWartosciBool.IsMatch(podanaWartoscBool))
                {
                    throw new ArgumentException("Podany wartosc ma nieprawidlowy format (Podaj wartosc T lub N)");
                }

                return podanaWartoscBool.Equals("T");
            }
            
            private static int konwertujDanaCalkowita(string podanaWartoscCalkowita)
            {
                try
                {
                    if (Int32.TryParse(podanaWartoscCalkowita, out var wartoscCalkowita))
                    {
                        return wartoscCalkowita;
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Podany wartosc ma nieprawidlowy format (Podaj liczbe calkowita)");
                }
                throw new ArgumentException("Podany wartosc ma nieprawidlowy format (Podaj liczbe calkowita)");
            }
            
            private static decimal konwertujDanaZmiennoprzecinkowa(string podanaWartoscZmiennoPrzecinkowa)
            {
                try
                {
                    if (decimal.TryParse(podanaWartoscZmiennoPrzecinkowa, out var wartoscZmiennoPrzecinkowa))
                    {
                        return wartoscZmiennoPrzecinkowa;
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Podany wartosc ma nieprawidlowy format (Podaj liczbe zmiennoprzecinkowa)");
                }
                throw new ArgumentException("Podany wartosc ma nieprawidlowy format (Podaj liczbe zmiennoprzecinkowa)");
            }
            
            private static string UstawDanaStudentaZKonsoli(
                Action<string> funkcjaUstawiajacaDane, 
                string komunikatPoczatkowy, 
                string komunikatBledu, 
                string komunikatKoncowy
                )
            {
                Console.WriteLine(komunikatPoczatkowy);
                while (true)
                {
                    try
                    {
                        var dana = OdczytajDanaZKonsoli();
                        Console.WriteLine();
                        if (dana == null)
                        {
                            throw new SystemException("Tworzenie studenta nieudane (Zakonczone wyjsciem przez uzytkownika)");
                        }
                        funkcjaUstawiajacaDane.Invoke(dana);
                        Console.WriteLine(komunikatKoncowy);
                        return dana;
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(komunikatBledu);
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Sproboj ponownie. (ESC aby wyjsc)");
                    }
                }
            }
            
            private static string OdczytajDanaZKonsoli()
            {
                string rezultat = null;

                var builder = new StringBuilder();

                var info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
                {
                    if (info.Key == ConsoleKey.Backspace)
                    {
                        if (builder.Length > 0)
                        {
                            Console.Write("\b \b");
                            builder.Length --;
                        }
                    }
                    else
                    {
                        Console.Write(info.KeyChar);
                        builder.Append(info.KeyChar);
                    }
                    
                    info = Console.ReadKey(true);
                } 

                if (info.Key == ConsoleKey.Enter)
                {
                    rezultat = builder.ToString();
                }

                return rezultat;
            }
        }
    }
}