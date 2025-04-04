namespace Bank
{
    public interface InterfaceKonto
    {
        string Klient { get; }
        decimal Bilans { get; }
        bool Zablokowane { get; }
        void BlokujKonto();
        void OdblokujKonto();
        void Wplata(decimal kwota);
        void Wyplata(decimal kwota);
    }
    public class Konto : InterfaceKonto
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

        public virtual void Wplata(decimal kwota)
        {
            if (this.Zablokowane)
                throw new ArgumentException("Konto zablokowane");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            this.Bilans += kwota;
        }

        public virtual void Wyplata(decimal kwota)
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

        public KontoLimit ToKontoLimmit()
        {
            return new KontoLimit(this.Klient, this.Bilans);
        }

        public KontoPlus ToKontoPlus()
        {
            return new KontoPlus(this.Klient, this.Bilans);
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

        public override void Wyplata(decimal kwota)
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

        public override void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            this.Bilans += kwota;
            if (this.Bilans >= 0)
            {
                this.OdblokujKonto();
            }
        }
        public Konto ToKonto()
        {
            return new Konto(this.Klient, this.Bilans);
        }
    }  

    public class KontoLimit : InterfaceKonto
    {
        private Konto konto;
        private decimal limit;
        private decimal bilans;

        public KontoLimit(string klient, decimal bilans = 0, decimal limit = 0)
        {
            if (limit < 0)
                throw new ArgumentException("Limit nie może być ujemny");
            this.konto = new Konto(klient, bilans);
            this.bilans = bilans;
            this.limit = limit;
        }
        public string Klient => this.konto.Klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => this.konto.Zablokowane;
        public decimal Limit => this.limit;

        public void BlokujKonto() => this.konto.BlokujKonto();
        public void OdblokujKonto() => this.konto.OdblokujKonto();

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            bilans += kwota;
            if (this.Bilans >= 0)
            {
                this.OdblokujKonto();
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (this.konto.Zablokowane)
                throw new ArgumentException("Konto zablokowane");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (kwota > this.Bilans + this.limit)
                throw new ArgumentException("Brak środków na koncie");
            bilans -= kwota;
            if (this.Bilans < 0)
            {
                this.konto.BlokujKonto();
            }
        }

        public void ZwiekszenieLimitu(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            this.limit += kwota;
        }

        public void ZmniejszenieLimitu(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być dodatnia");
            if (kwota > this.limit)
                throw new ArgumentException("Nie można zmniejszyć limitu poniżej zera");
            this.limit -= kwota;
        }

        public Konto ToKonto()
        {
            return new Konto(this.Klient, this.Bilans);
        }

    }
}
