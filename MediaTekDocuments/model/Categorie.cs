using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Categorie
    /// </summary>
    public class Categorie
    {
        public string Id { get; }
        public string Libelle { get; }

        public Categorie(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        /// <summary>
        /// Récupération du libellé pour l'affichage dans les combos
        /// </summary>
        /// <returns>Libelle</returns>
        public override string ToString()
        {
            return this.Libelle;
        }

    }
}
