using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class CommandeTest
    {
        [TestMethod]
        public void Commande_Constructeur()
        {
            DateTime dateCommande = new DateTime(2025, 3, 28, 15, 41, 25);
            double montant = 1.00;
            Commande commande = new Commande("00001", dateCommande, montant);

            Assert.AreEqual("00001", commande.Id);
            Assert.AreEqual(dateCommande, commande.DateCommande);
            Assert.AreEqual(montant, commande.Montant);

            // Vérifie que le montant n'est pas négatif
            Assert.IsTrue(commande.Montant >= 0);

            // Vérifie que la date de commande n"est pas dans le futur
            Assert.IsTrue(commande.DateCommande <= DateTime.Now);
        }
    }
}