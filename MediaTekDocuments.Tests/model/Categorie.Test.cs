using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class CategorieTest
    {
        [TestMethod]
        public void Categorie_Constructeur()
        {
            Categorie categorie = new Categorie("00001", "neuf");

            Assert.AreEqual("00001", categorie.Id);
            Assert.AreEqual("neuf", categorie.Libelle);

            // Vérifie que le libellé n'est pas vide
            Assert.IsFalse(string.IsNullOrEmpty(categorie.Libelle));

            // Vérifie que l'id est bien du type string
            Assert.IsInstanceOfType(categorie.Id, typeof(string));
        }
    }
}