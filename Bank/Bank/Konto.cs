namespace Bank
{
    public class Konto
    {
        public string Klient { get; }
        public decimal Bilans { get; private set; }
        public bool Zablokowane { get; private set; }
        public Konto(string klient, decimal bilans = 0)
        {
            this.Klient = klient;
            this.Bilans = bilans;
        }

        void Wplata(decimal kwota)
        {
            if (this.Zablokowane)
                throw new System.Exception("Konto zablokowane");
            if (kwota <= 0)
                throw new System.Exception("Kwota musi być dodatnia");
            this.Bilans += kwota;
        }

        void Wyplata(decimal kwota)
        {
            if (this.Zablokowane)
                throw new System.Exception("Konto zablokowane");
            if (kwota <= 0)
                throw new System.Exception("Kwota musi być dodatnia");
            if (kwota > this.Bilans)
                throw new System.Exception("Brak środków na koncie");
            this.Bilans -= kwota;
        }

    }
}
