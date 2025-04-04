using Bank;

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
            Assert.ThrowsException<ArgumentException>(() => new KontoPlus("Jakub", -1000, 500));
        }

        [TestMethod]
        public void KontoPlus_Konstruktor_UjemnyDebet()
        {
            Assert.ThrowsException<ArgumentException>(() => new KontoPlus("Jakub", 1000, -500));
        }

        [TestMethod]
        public void KontoPlus_Wplata_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.Wplata(500);
            Assert.AreEqual(1500, kontoPlus.Bilans);
        }

        [TestMethod]
        public void KontoPlus_Wplata_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wplata(-500));
        }

        [TestMethod]
        public void KontoPlus_Wplata_Zero()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wplata(0));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.Wyplata(1200);
            Assert.AreEqual(-200, kontoPlus.Bilans);
            Assert.IsTrue(kontoPlus.Zablokowane);
        }

        [TestMethod]
        public void KontoPlus_Wyplata_WiekszaNizBilansIDebet()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(1600));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(-500));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_Zero()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.Wyplata(0));
        }

        [TestMethod]
        public void KontoPlus_ZwiekszenieDebetu_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.ZwiekszenieDebetu(200);
            Assert.AreEqual(700, kontoPlus.Debet);
        }

        [TestMethod]
        public void KontoPlus_ZwiekszenieDebetu_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZwiekszenieDebetu(-200));
        }

        [TestMethod]
        public void KontoPlus_ZwiekszenieDebetu_Zero()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZwiekszenieDebetu(0));
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_PrawidlowaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.ZmniejszenieDebetu(200);
            Assert.AreEqual(300, kontoPlus.Debet);
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_WiekszaNizDebet()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoPlus.ZmniejszenieDebetu(600));
        }

        [TestMethod]
        public void KontoPlus_ZmniejszenieDebetu_UjemnaKwota()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
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

        [TestMethod]
        public void KontoPlus_KontoZaplokowaneOdblokowane()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.BlokujKonto();
            Assert.IsTrue(kontoPlus.Zablokowane);
            kontoPlus.OdblokujKonto();
            Assert.IsFalse(kontoPlus.Zablokowane);
        }

        [TestMethod]
        public void KontoPlus_WyplacWplacZero()
        {
            var kontoPlus = new KontoPlus("Jakub", 1000, 500);
            kontoPlus.Wyplata(1500);
            Assert.AreEqual(-500, kontoPlus.Bilans);
            kontoPlus.Wplata(500);
            Assert.AreEqual(0, kontoPlus.Bilans);
        }
    }

    [TestClass]
    public sealed class KontoLimitTest
    {
        [TestMethod]
        public void KontoLimit_Konstruktor_PrawidloweParametry()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.AreEqual("Jakub", kontoLimit.Klient);
            Assert.AreEqual(1000, kontoLimit.Bilans);
            Assert.AreEqual(500, kontoLimit.Limit);
            Assert.IsFalse(kontoLimit.Zablokowane);
        }

        [TestMethod]
        public void KontoLimit_Konstruktor_UjemnyBilans()
        {
            Assert.ThrowsException<ArgumentException>(() => new KontoLimit("Jakub", -1000, 500));
        }

        [TestMethod]
        public void KontoLimit_Konstruktor_UjemnyDebet()
        {
            Assert.ThrowsException<ArgumentException>(() => new KontoLimit("Jakub", 1000, -500));
        }

        [TestMethod]
        public void KontoLimit_Wplata_PrawidlowaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.Wplata(500);
            Assert.AreEqual(1500, kontoLimit.Bilans);
        }

        [TestMethod]
        public void KontoLimit_Wplata_UjemnaKwota()
        {
            var kontoLimit = new KontoPlus("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.Wplata(-500));
        }

        [TestMethod]
        public void KontoLimit_Wplata_Zero()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.Wplata(0));
        }

        [TestMethod]
        public void KontoLimit_Wyplata_PrawidlowaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.Wyplata(1200);
            Assert.AreEqual(-200, kontoLimit.Bilans);
            Assert.IsTrue(kontoLimit.Zablokowane);
        }

        [TestMethod]
        public void KontoLimit_Wyplata_WiekszaNizBilansIDebet()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.Wyplata(1600));
        }

        [TestMethod]
        public void KontoPlus_Wyplata_UjemnaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.Wyplata(-500));
        }

        [TestMethod]
        public void KontoLimit_Wyplata_Zero()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.Wyplata(0));
        }

        [TestMethod]
        public void KontoLimit_ZwiekszenieDebetu_PrawidlowaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.ZwiekszenieLimitu(200);
            Assert.AreEqual(700, kontoLimit.Limit);
        }

        [TestMethod]
        public void KontoLimit_ZwiekszenieDebetu_UjemnaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.ZwiekszenieLimitu(-200));
        }

        [TestMethod]
        public void KontoLimit_ZwiekszenieDebetu_Zero()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.ZwiekszenieLimitu(0));
        }

        [TestMethod]
        public void KontoLimit_ZmniejszenieDebetu_PrawidlowaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.ZmniejszenieLimitu(200);
            Assert.AreEqual(300, kontoLimit.Limit);
        }

        [TestMethod]
        public void KontoLimit_ZmniejszenieDebetu_WiekszaNizDebet()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.ZmniejszenieLimitu(600));
        }

        [TestMethod]
        public void KontoLimit_ZmniejszenieDebetu_UjemnaKwota()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.ZmniejszenieLimitu(-200));
        }

        [TestMethod]
        public void KontoLimit_ZmniejszenieDebetu_Zero()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.ZmniejszenieLimitu(0));
        }

        [TestMethod]
        public void KontoLimit_KontoZablokowaneWyplata()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.BlokujKonto();
            Assert.ThrowsException<ArgumentException>(() => kontoLimit.Wyplata(100));
        }

        [TestMethod]
        public void KontoLimit_KontoZaplokowaneOdblokowane()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.BlokujKonto();
            Assert.IsTrue(kontoLimit.Zablokowane);
            kontoLimit.OdblokujKonto();
            Assert.IsFalse(kontoLimit.Zablokowane);
        }

        [TestMethod]
        public void KontoLimit_WyplacWplacZero()
        {
            var kontoLimit = new KontoLimit("Jakub", 1000, 500);
            kontoLimit.Wyplata(1500);
            Assert.AreEqual(-500, kontoLimit.Bilans);
            kontoLimit.Wplata(500);
            Assert.AreEqual(0, kontoLimit.Bilans);
        }
    }

}