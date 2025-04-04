namespace Bank
{
    public class Konto
    {
        public string Klient { get; }
        public decimal Bilans { get; protected set; }

        public bool Zablokowane { get; private set; }
        public Konto(string klient, decimal bilans = 0)
        {
            if (bilans < 0)
                throw new ArgumentException("Bilans nie może być ujemny");
            if (string.IsNullOrWhiteSpace(klient))
                throw new ArgumentException("Nazwa klienta nie może być pusta");
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

    public class KontoPlus : Konto
    {
        public decimal Debet { get; private set; }
        public KontoPlus(string klient, decimal bilans = 0, decimal debet = 0) : base(klient, bilans)
        {
            if (debet < 0)
                throw new ArgumentException("Debet nie może być ujemny");
            this.Debet = debet;
        }

        public void ZwiekszenieDebetu(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            this.Debet += kwota;
        }

        public void ZmniejszenieDebetu(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (kwota > this.Debet)
                throw new ArgumentException("Nie można zmniejszyć debetu poniżej zera");
            this.Debet -= kwota;
        }

        public new void Wyplata(decimal kwota)
        {
            if (this.Zablokowane)
                throw new ArgumentException("Konto zablokowane");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (kwota > this.Bilans + this.Debet)
                throw new ArgumentException("Brak środków na koncie");
            this.Bilans -= kwota;
            if (this.Bilans < 0)
            {
                this.BlokujKonto();
            }            
        }

        public new void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            this.Bilans += kwota;
            if (this.Bilans > 0)
            {
                this.OdblokujKonto();
            }
        }
    }
}
