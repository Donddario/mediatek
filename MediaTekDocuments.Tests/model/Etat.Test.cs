using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class EtatTest
    {
        [TestMethod]
        public void Etat_Constructeur()
        {
            Etat etat = new Etat("00001", "neuf");

            Assert.AreEqual("00001", etat.Id);
            Assert.AreEqual("neuf", etat.Libelle);

            // Vérifie que l'id n'est pas vide
            Assert.IsFalse(string.IsNullOrEmpty(etat.Id));

            // Vérifie que le libellé ne contient pas de chiffre
            Assert.IsTrue(!System.Text.RegularExpressions.Regex.IsMatch(etat.Libelle, @"\d"));
        }
    }
}