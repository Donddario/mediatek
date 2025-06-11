using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class UtilisateurTests
    {
        [TestMethod]
        public void Utilisateur_Constructeur()
        {
            Utilisateur user = new Utilisateur(0, "Antoine", "Legrand", "alegrand", "motdepasse", "00003");

            Assert.AreEqual(0, user.Id);
            Assert.AreEqual("Antoine", user.Firstname);
            Assert.AreEqual("Legrand", user.Name);
            Assert.AreEqual("alegrand", user.Login);
            Assert.AreEqual("motdepasse", user.Password);
            Assert.AreEqual("00003", user.IdService);

            // vérifie que le login contient bien le prenom ou le nom
            Assert.IsTrue(user.Login.Contains("ale") || user.Login.Contains("grand"));

            // vérifie que l'IdService a une longueur de 5 caractères
            Assert.AreEqual(5, user.IdService.Length);
        }
    }
}