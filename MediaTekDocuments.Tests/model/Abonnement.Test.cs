using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class AbonnementTest
    {
        [TestMethod]
        public void Abonnement_Constructeur()
        {
            DateTime dateCommande = new DateTime(2025, 3, 28, 15, 41, 25);
            Commande commande = new Commande("0001", dateCommande, 1);
            Abonnement abonnement = new Abonnement("00001", dateCommande, "00001", commande);

            abonnement.CompleterAvecCommande(commande);

            Assert.AreEqual("00001", abonnement.Id);
            Assert.AreEqual(dateCommande, abonnement.DateFinAbonnement);
            Assert.AreEqual("00001", abonnement.IdRevue);
            Assert.AreEqual("0001", abonnement.IdCommande);
            Assert.AreEqual(dateCommande, abonnement.DateCommande);
            Assert.AreEqual(1, abonnement.Montant);

            Assert.IsTrue(abonnement.Montant >= 0);
        }

        [TestMethod]
        public void Abonnement_CompleterAvecCommande_ModifieValeurs()
        {
            DateTime date1 = new DateTime(2025, 5, 1);
            DateTime date2 = new DateTime(2025, 6, 10, 10, 30, 0);
            Commande cmd1 = new Commande("C01", date1, 5);
            Commande cmd2 = new Commande("C02", date2, 7.5);
            Abonnement abo = new Abonnement("A01", date1, "R01", cmd1);

            abo.CompleterAvecCommande(cmd2);

            Assert.AreEqual("C02", abo.IdCommande);
            Assert.AreEqual(date2, abo.DateCommande);
            Assert.AreEqual(7.5, abo.Montant);
        }
    }
}