using Bank;

namespace Symulacja
{
    class Program
    {
        static void Main(string[] args)
        {
            // Konto class
            Konto konto = new Konto("Jan Kowalski", 1000);
            Console.WriteLine($"Konto: {konto.Klient}, Bilans: {konto.Bilans}, Zablokowane: {konto.Zablokowane}");
            konto.Wplata(500);
            Console.WriteLine($"Po wpłacie 500: Bilans: {konto.Bilans}");
            konto.Wyplata(300);
            Console.WriteLine($"Po wypłacie 300: Bilans: {konto.Bilans}");
            konto.BlokujKonto();
            Console.WriteLine($"Po zablokowaniu konta: Zablokowane: {konto.Zablokowane}");
            konto.OdblokujKonto();
            Console.WriteLine($"Po odblokowaniu konta: Zablokowane: {konto.Zablokowane}");

            // KontoPlus
            KontoPlus kontoPlus = new KontoPlus("Anna Nowak", 2000, 1000);
            Console.WriteLine($"KontoPlus: {kontoPlus.Klient}, Bilans: {kontoPlus.Bilans}, Debet: {kontoPlus.Debet}, Zablokowane: {kontoPlus.Zablokowane}");
            kontoPlus.Wplata(500);
            Console.WriteLine($"Po wpłacie 500: Bilans: {kontoPlus.Bilans}");
            kontoPlus.Wyplata(3000);
            Console.WriteLine($"Po wypłacie 3000: Bilans: {kontoPlus.Bilans}, Zablokowane: {kontoPlus.Zablokowane}");
            kontoPlus.ZwiekszenieDebetu(500);
            Console.WriteLine($"Po zwiększeniu debetu o 500: Debet: {kontoPlus.Debet}");
            kontoPlus.ZmniejszenieDebetu(200);
            Console.WriteLine($"Po zmniejszeniu debetu o 200: Debet: {kontoPlus.Debet}");

            // KontoLimit
            KontoLimit kontoLimit = new KontoLimit("Jakub Wiśniewski", 1000, 500);
            Console.WriteLine($"KontoLimit: {kontoLimit.Klient}, Bilans: {kontoLimit.Bilans}, Limit: {kontoLimit.Limit}, Zablokowane: {kontoLimit.Zablokowane}");
            kontoLimit.Wplata(500);
            Console.WriteLine($"Po wpłacie 500: Bilans: {kontoLimit.Bilans}");
            kontoLimit.Wyplata(1200);
            Console.WriteLine($"Po wypłacie 1200: Bilans: {kontoLimit.Bilans}, Zablokowane: {kontoLimit.Zablokowane}");
            kontoLimit.ZwiekszenieLimitu(300);
            Console.WriteLine($"Po zwiększeniu limitu o 300: Limit: {kontoLimit.Limit}");
            kontoLimit.ZmniejszenieLimitu(200);
            Console.WriteLine($"Po zmniejszeniu limitu o 200: Limit: {kontoLimit.Limit}");
        }
    }
}