using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class DocumentTest
    {
        [TestMethod]
        public void Document_Constructeur()
        {
            Document document = new Document("00001", "Test", "Test", "00001", "TestGenre", "00001", "TestPublic", "00001", "TestRayon");

            Assert.AreEqual("00001", document.Id);
            Assert.AreEqual("Test", document.Titre);
            Assert.AreEqual("Test", document.Image);
            Assert.AreEqual("00001", document.IdGenre);
            Assert.AreEqual("TestGenre", document.Genre);
            Assert.AreEqual("00001", document.IdPublic);
            Assert.AreEqual("TestPublic", document.Public);
            Assert.AreEqual("00001", document.IdRayon);
            Assert.AreEqual("TestRayon", document.Rayon);

            // Ajout : vérifie que le titre n’est pas null ou vide
            Assert.IsFalse(string.IsNullOrEmpty(document.Titre));

            // Ajout : vérifie que tous les ids sont de longueur 5
            Assert.AreEqual(5, document.Id.Length);
            Assert.AreEqual(5, document.IdGenre.Length);
            Assert.AreEqual(5, document.IdPublic.Length);
            Assert.AreEqual(5, document.IdRayon.Length);
        }
    }
}