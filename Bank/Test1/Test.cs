using Bank;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public sealed class KontoTest
    {
        [TestMethod]
        public void Konto_Konstruktor()
        {
            var konto = new Konto("Jakub", 1000);
            Assert.AreEqual("Jakub", konto.Klient);
            Assert.AreEqual(1000, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void Konto_Konstruktor_UjemnyBilans()
        {
            Assert.ThrowsException<ArgumentException>(() => new Konto("Jakub", -1000));
        }

        [TestMethod]
        public void Konto_Konstruktor_PustaNazwa()
        {
            Assert.ThrowsException<ArgumentException>(() => new Konto("", 1000));
        }

        [TestMethod]
        public void Konto_Konstruktor_TylkoNazwa()
        {
            var konto = new Konto("Jakub");
            Assert.AreEqual(0, konto.Bilans);
        }

        [TestMethod]
        public void Konto_Wplata()
        {
            var konto = new Konto("Jakub", 1000);
            konto.Wplata(500);
            Assert.AreEqual(1500, konto.Bilans);
        }

        [TestMethod]
        public void Konto_Wplata_Ujemna()
        {
            var konto = new Konto("Jakub", 1000);
            Assert.ThrowsException<ArgumentException>(() => konto.Wplata(-500));
        }

        [TestMethod]
        public void Konto_Wplata_Zero()
        {
            var konto = new Konto("Jakub", 1000);
            Assert.ThrowsException<ArgumentException>(() => konto.Wplata(0));
        }

        [TestMethod]
        public void Konto_Wplata_KontoZablokowane()
        {
            var konto = new Konto("Jakub", 1000);
            konto.BlokujKonto();
            Assert.ThrowsException<ArgumentException>(() => konto.Wplata(500));
        }

        [TestMethod]
        public void Konto_Wyplata_PrawidlowaKwota()
        {
            var konto = new Konto("Jakub", 1000);
            konto.Wyplata(500);
            Assert.AreEqual(500, konto.Bilans);
        }

        [TestMethod]
        public void Konto_Wyplata_WiekszaNizBilans()
        {
            var konto = new Konto("Jakub", 1000);
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(1500));
        }

        [TestMethod]
        public void Konto_Wyplata_UjemnaKwota()
        {
            var konto = new Konto("Jakub", 1000);
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(-500));
        }

        [TestMethod]
        public void Konto_Wyplata_Zero()
        {
            var konto = new Konto("Jakub", 1000);
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(0));
        }

        [TestMethod]
        public void Konto_Wyplata_KontoZablokowane()
        {
            var konto = new Konto("Jakub", 1000);
            konto.BlokujKonto();
            Assert.ThrowsException<ArgumentException>(() => konto.Wyplata(500));
        }

        [TestMethod]
        public void Konto_BlokujKonto()
        {
            var konto = new Konto("Jakub", 1000);
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Konto_OdblokujKonto()
        {
            var konto = new Konto("Jakub", 1000);
            konto.BlokujKonto();
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }
    }

    [TestClass]
    public sealed class KontoPlusTest
    {
        [TestMethod]
        public void KontoPlus_Konstruktor_PrawidloweParametry()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.AreEqual("Jakub", kontoPlus.Klient);
            Assert.AreEqual(1000, kontoPlus.Bilans);
            Assert.AreEqual(500, kontoPlus.Debet);
            Assert.IsFalse(kontoPlus.Zablokowane);
        }

        [TestMethod]
        public void KontoPlus_Konstruktor_UjemnyBilans()
        {
            Assert.ThrowsException<ArgumentException>(() => new KontoPlus("Jane Doe", -1000, 500));
        }

        [TestMethod]
        public void KontoPlus_Konstruktor_UjemnyDebet()
        {
            Assert.ThrowsException<ArgumentException>(() => new KontoPlus("Jane Doe", 1000, -500));
        }

        [TestMethod]
        public void KontoPlus_Wplata_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            kontoPlus.Wplata(500);
            Assert.AreEqual(1500, kontoPlus.Bilans);
        }

        [TestMethod]
        public void KontoPlus_Wplata_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wplata(-500));
        }

        [TestMethod]
        public void KontoPlus_Wplata_Zero()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wplata(0));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            kontoPlus.Wyplata(1200);
            Assert.AreEqual(-200, kontoPlus.Bilans);
            Assert.IsTrue(kontoPlus.Zablokowane);
        }

        [TestMethod]
        public void KontoPlus_Wyplata_WiekszaNizBilansIDebet()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(1600));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(-500));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_Zero()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(0));
        }

        [TestMethod]
        public void KontoPlus_ZwiekszenieDebetu_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            kontoPlus.ZwiekszenieDebetu(200);
            Assert.AreEqual(700, kontoPlus.Debet);
        }

        [TestMethod]
        public void KontoPlus_ZwiekszenieDebetu_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZwiekszenieDebetu(-200));
        }

        [TestMethod]
        public void KontoPlus_ZwiekszenieDebetu_Zero()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZwiekszenieDebetu(0));
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            kontoPlus.ZmniejszenieDebetu(200);
            Assert.AreEqual(300, kontoPlus.Debet);
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_WiekszaNizDebet()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZmniejszenieDebetu(600));
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jane Doe", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZmniejszenieDebetu(-200));
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_Zero()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZmniejszenieDebetu(0));
        }

        [TestMethod]
        public void KontoPlus_KontoZablokowaneWyplata()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.BlokujKonto();
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(100));
        }
    }
}