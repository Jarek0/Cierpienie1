using System;

namespace M1_P1 
{
    public class Osoba 
    {
        public string Imie;
        public string Nazwisko;
        private int rokUrodzenia;

        public void UstawRokUrodzenia(int rokUrodzenia) 
        {
            if (DateTime.Now.Year - rokUrodzenia < 18)
                throw new ArgumentException("Osoba musi być pełnoletnia");
            this.rokUrodzenia = rokUrodzenia;
        }

        private bool czyKobieta() 
        {
            if (Imie.EndsWith("a")) {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var tytul = "";
            if (Imie == null) return $"{tytul} {Imie} {Nazwisko} ur. w {rokUrodzenia} roku";
            tytul = czyKobieta() ? "Pani" : "Pan";

            return $"{tytul} {Imie} {Nazwisko} ur. w {rokUrodzenia} roku";
        }
    }
}