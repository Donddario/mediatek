using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class GenreTest
    {
        [TestMethod]
        public void Genre_Constructeur()
        {
            Genre genre = new Genre("00001", "Test");

            Assert.AreEqual("00001", genre.Id);
            Assert.AreEqual("Test", genre.Libelle);

            // Ajout : vérifie que l'id est bien une string de 5 caractères
            Assert.AreEqual(5, genre.Id.Length);

            // Ajout : vérifie que le libellé commence bien par une majuscule
            Assert.IsTrue(char.IsUpper(genre.Libelle[0]));
        }
    }
}