using Bank;
namespace Test
{
    [TestClass]
    public sealed class Test
    {
        [TestMethod]
        public void TworzenieKonta()
        {
            // Arrage
            var klient = "Jan";
            var bilans = 1000;
            // Act
            var konto = new Konto(klient, bilans);
            // Assert
            Assert.AreEqual(klient, konto.Klient);
            Assert.AreEqual(bilans, konto.Bilans);
        }

        [TestMethod]
        public void TworzenieKontaBilansMniejZero()
        {
            // Arrage
            var klient = "Jan";
            var bilans = -1000;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Konto(klient, bilans));
        }

        [TestMethod]
        public void TworzenieKontaBilansZero()
        {
            // Arrage
            var klient = "Jan";
            var bilans = 0;
            // Act
            var konto = new Konto(klient, bilans);
            // Assert
            Assert.AreEqual(klient, konto.Klient);
            Assert.AreEqual(bilans, konto.Bilans);
        }

        [TestMethod]
        public void TworzenieKontaBrakNazwy()
        {
            // Arrage
            var klient = "";
            var bilans = 1000;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new Konto(klient, bilans));
        }

        [TestMethod]
        public void Wplata()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            var kwota = 100;
            // Act
            konto.Wplata(kwota);
            // Assert
            Assert.AreEqual(1100, konto.Bilans);
        }

        [TestMethod]
        public void WplataMniejZero()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            var kwota = -100;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => konto.Wplata(kwota));
        }

        [TestMethod]
        public void ZablokowanieKonta()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            // Act
            konto.BlokujKonto();
            // Assert
            Assert.IsTrue(konto.Zablokowane);
        }

        public void OdblokowanieKonta()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            // Act
            konto.OdblokujKonto();
            // Assert
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void WplataKontoZablokowane()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            konto.BlokujKonto();
            var kwota = 100;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => konto.Wplata(kwota));
        }

        [TestMethod]
        public void Wyplata()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            var kwota = 100;
            // Act
            konto.Wyplata(kwota);
            // Assert
            Assert.AreEqual(900, konto.Bilans);
        }

        [TestMethod]
        public void WyplataMniejZero()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            var kwota = -100;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(kwota));
        }

        [TestMethod]
        public void WyplataKontoZablokowane()
        {
            // Arrage
            var konto = new Konto("Jan", 1000);
            konto.BlokujKonto();
            var kwota = 100;
            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(kwota));
        }
    }
}
