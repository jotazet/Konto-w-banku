namespace Bank
{
    public class Konto
    {
        public string Klient { get; }
        private decimal bilans;
        public decimal Bilans { get; private set; }

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
        public new decimal Bilans { get; private set; }
        private bool debetUzyty;
        private decimal debet;
        public decimal Debet
        {
            get => debet;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Debet musi być dodatni lub równy zero");
                debet = value;
            }
        }
        public KontoPlus(string klient, decimal bilans = 0, decimal debet = 0) : base(klient, bilans)
        {
            this.Debet = debet;
        }
        public void ZwiekszenieLimitu(decimal debet)
        {
            if (debet <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (debet > Debet)
                debetUzyty = false;
            Debet += debet;
        }

        public void ZmniejszenieLimitu(decimal kwota)
        {
            if (kwota > Debet)
                throw new ArgumentException("Kwota nie może być większa niż aktualny debet");        
            Debet -= kwota;
        }

        public new void Wplata(decimal kwota)
        {
            base.Wplata(kwota);
            if (Bilans > 0)
            {
                OdblokujKonto();
                debetUzyty = false;
            }
        }
        public new void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new ArgumentException("Konto zablokowane");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (kwota > Bilans)
            {
                if (debetUzyty || kwota > base.Bilans + Debet)
                    throw new ArgumentException("Brak środków na koncie");
                debetUzyty = true;
                BlokujKonto();
            }
            this.Bilans -= kwota;
        }
    }
}
