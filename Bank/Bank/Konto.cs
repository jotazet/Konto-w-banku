namespace Bank
{
    public class Konto
    {
        public string Klient { get; }
        public decimal Bilans { get; private set; }
        public bool Zablokowane { get; private set; }
        public Konto(string klient, decimal bilans = 0)
        {
            if (string.IsNullOrWhiteSpace(klient))
                throw new ArgumentException("Nazwa klienta nie może być pusta");
            if (bilans < 0)
                throw new ArgumentException("Kwota musi być dodatnia lub równa zero");
            this.Klient = klient;
            this.Bilans = bilans;
        }

        public void Wplata(decimal kwota)
        {
            if (this.Zablokowane)
                throw new ArgumentException("Konto zablokowane");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            this.Bilans += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (this.Zablokowane)
                throw new ArgumentException("Konto zablokowane");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (kwota > this.Bilans)
                throw new ArgumentException("Brak środków na koncie");
            this.Bilans -= kwota;
        }

        public void BlokujKonto()
        {
            this.Zablokowane = true;
        }

        public void OdblokujKonto()
        {
            this.Zablokowane = false;
        }
    }
}
