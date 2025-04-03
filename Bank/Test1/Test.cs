using Bank;
namespace Test
{
    [TestClass]
    public sealed class KontoTest
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
        [TestClass]
        public sealed class KontoPlusTest
        {
            [TestMethod]
            public void TworzenieKontaPlus()
            {
                // Arrage
                var klient = "Jan";
                var bilans = 1000;
                var debet = 100;
                // Act
                var konto = new KontoPlus(klient, bilans, debet);
                // Assert
                Assert.AreEqual(klient, konto.Klient);
                Assert.AreEqual(bilans, konto.Bilans);
                Assert.AreEqual(debet, konto.Debet);
            }
            [TestMethod]
            public void TworzenieKontaPlusDebetMniejZero()
            {
                // Arrage
                var klient = "Jan";
                var bilans = 1000;
                var debet = -100;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => new KontoPlus(klient, bilans, debet));
            }
            [TestMethod]
            public void TworzenieKontaPlusDebetZero()
            {
                // Arrage
                var klient = "Jan";
                var bilans = 1000;
                var debet = 0;
                // Act
                var konto = new KontoPlus(klient, bilans, debet);
                // Assert
                Assert.AreEqual(klient, konto.Klient);
                Assert.AreEqual(bilans, konto.Bilans);
                Assert.AreEqual(debet, konto.Debet);
            }
            [TestMethod]
            public void TworzenieKontaPlusBrakNazwy()
            {
                // Arrage
                var klient = "";
                var bilans = 1000;
                var debet = 100;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => new KontoPlus(klient, bilans, debet));
            }
            [TestMethod]
            public void ZwiekszenieLimitu()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var debet = 100;
                // Act
                konto.ZwiekszenieDebetu(debet);
                // Assert
                Assert.AreEqual(200, konto.Debet);
            }
            [TestMethod]
            public void ZwiekszenieLimituMniejZero()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var debet = -100;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.ZwiekszenieDebetu(debet));
            }
            [TestMethod]
            public void ZwiekszenieLimituDebetZero()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var debet = 0;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.ZwiekszenieDebetu(debet));
            }
            [TestMethod]
            public void ZmniejszenieLimitu()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var debet = 50;
                // Act
                konto.ZmniejszenieDebetu(debet);
                // Assert
                Assert.AreEqual(50, konto.Debet);
            }
            [TestMethod]
            public void ZmniejszenieLimituMniejZero()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var debet = -50;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.ZmniejszenieDebetu(debet));
            }
            [TestMethod]
            public void Wplata()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
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
                var konto = new KontoPlus("Jan", 1000, 100);
                var kwota = -100;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.Wplata(kwota));
            }
            [TestMethod]
            public void WplataKontoZablokowane()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
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
                var konto = new KontoPlus("Jan", 1000, 100);
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
                var konto = new KontoPlus("Jan", 1000, 100);
                var kwota = -100;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(kwota));
            }
            [TestMethod]
            public void WyplataKontoZablokowane()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                konto.BlokujKonto();
                var kwota = 100;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(kwota));
            }
            [TestMethod]
            public void WyplataDebet()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var kwota = 1100;
                // Act
                konto.Wyplata(kwota);
                // Assert
                Assert.AreEqual(-100, konto.Bilans);
            }
            [TestMethod]
            public void WyplataDebetBrakSrodkow()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var kwota = 1200;
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(kwota));
            }
            [TestMethod]
            public void WyplataDebetBrakSrodkowDebet()
            {
                // Arrage
                var konto = new KontoPlus("Jan", 1000, 100);
                var kwota = 1100;
                konto.Wyplata(kwota);
                // Act
                // Assert
                Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(100));
            }
        }
    }
}
