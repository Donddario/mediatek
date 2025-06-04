using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            return access.GetExemplairesRevue(idDocument);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Récupère l'identifiant d'un rayon à partir de son libellé
        /// </summary>
        /// <param name="idRayon">Libellé du rayon</param>
        /// <returns>L'identifiant du rayon ou null si non trouvé</returns>
        public string GetIdByNameOfRayon(string idRayon)
        {
            return access.GetIdByNameOfRayon(idRayon);
        }

        /// <summary>
        /// Récupère l'identifiant d'un public à partir de son libellé
        /// </summary>
        /// <param name="idPublic">Libellé du public</param>
        /// <returns>L'identifiant du public ou null si non trouvé</returns>
        public string GetIdByNameOfPublic(string idPublic)
        {
            return access.GetIdByNameOfPublic(idPublic);
        }

        /// <summary>
        /// Récupère l'identifiant d'un genre à partir de son libellé
        /// </summary>
        /// <param name="idGenre">Libellé du genre</param>
        /// <returns>L'identifiant du genre ou null si non trouvé</returns>
        public string GetIdByNameOfGenre(string idGenre)
        {
            return access.GetIdByNameOfGenre(idGenre);
        }

        /// <summary>
        /// Ajoute un document (livre, DVD, etc.) dans la base de données via l'API.
        /// </summary>
        /// <param name="livre">L'objet livre à ajouter</param>
        /// <returns>True si l'ajout a réussi, False sinon</returns>
        public bool AjouterLivre(Livre livre)
        {
            access.AjouterDocument(livre);
            access.AjouterLivreDvD(livre);
            return access.AjouterLivre(livre);
        }

        /// <summary>
        /// Ajoute un document (livre, DVD, etc.) dans la base de données via l'API.
        /// </summary>
        /// <param name="revue">L'objet revue à ajouter</param>
        /// <returns>True si l'ajout a réussi, False sinon</returns>
        public bool AjouterRevue(Revue revue)
        {
            access.AjouterDocument(revue);
            return access.AjouterRevue(revue);
        }

        /// <summary>
        /// Ajoute un document (livre, DVD, etc.) dans la base de données via l'API.
        /// </summary>
        /// <param name="dvd">L'objet DVD à ajouter</param>
        /// <returns>True si l'ajout a réussi, False sinon</returns>
        public bool AjouterDvd(Dvd dvd)
        {
            access.AjouterDocument(dvd);
            access.AjouterLivreDvD(dvd);
            return access.AjouterDvd(dvd);
        }

        /// <summary>
        /// Modifie un livre et son document associé dans la base de données via l'API.
        /// </summary>
        /// <param name="livre">L'objet livre à modifier</param>
        /// <param name="document">L'objet document associé à modifier</param>
        /// <returns>True si la modification a réussi, False sinon</returns>
        public bool ModifierLivre(Livre livre, Document document)
        {
            access.ModifierDocument(document);
            access.ModifierLivre_DvD(livre);
            return access.ModifierLivre(livre);
        }

        /// <summary>
        /// Modifie un DVD et son document associé dans la base de données via l'API.
        /// </summary>
        /// <param name="dvd">L'objet DVD à modifier</param>
        /// <param name="document">L'objet document associé à modifier</param>
        /// <returns>True si la modification a réussi, False sinon</returns>
        public bool ModifierDvd(Dvd dvd, Document document)
        {
            access.ModifierDocument(document);
            access.ModifierLivre_DvD(dvd);
            return access.ModifierDvd(dvd);
        }

        /// <summary>
        /// Modifie une revue et son document associé dans la base de données via l'API.
        /// </summary>
        /// <param name="revue">L'objet revue à modifier</param>
        /// <param name="document">L'objet document associé à modifier</param>
        /// <returns>True si la modification a réussi, False sinon</returns>
        public bool ModifierRevue(Revue revue, Document document)
        {
            access.ModifierDocument(document);
            return access.ModifierRevue(revue);
        }

        /// <summary>
        /// Récupère tous les dictionnaires de genres, publics et rayons.
        /// </summary>
        public void GetAllDictionnaries()
        {
            access.DictionnaireGenre();
            access.DictionnairePublic();
            access.DictionnaireRayon();
        }

        /// <summary>
        /// Supprime un livre et son document associé de la base de données via l'API.
        /// </summary>
        /// <param name="livre">L'objet livre à supprimer</param>
        /// <param name="document">L'objet document associé à supprimer</param>
        /// <returns>True si la suppression a réussi, False sinon</returns>
        public bool SupprimerLivre(Livre livre, Document document)
        {
            access.SupprimerLivre(livre);
            access.SupprimerLivre_DvD(livre);
            return access.SupprimerDocument(document);
        }

        /// <summary>
        /// Supprime une revue et son document associé de la base de données via l'API.
        /// </summary>
        /// <param name="revue">L'objet revue à supprimer</param>
        /// <param name="document">L'objet document associé à supprimer</param>
        /// <returns>True si la suppression a réussi, False sinon</returns>
        public bool SupprimerRevue(Revue revue, Document document)
        {
            access.SupprimerRevue(revue);
            return access.SupprimerDocument(document);
        }

        /// <summary>
        /// Supprime un DVD et son document associé de la base de données via l'API.
        /// </summary>
        /// <param name="dvd">L'objet DVD à supprimer</param>
        /// <param name="document">L'objet document associé à supprimer</param>
        /// <returns>True si la suppression a réussi, False sinon</returns>
        public bool SupprimerDvd(Dvd dvd, Document document)
        {
            access.SupprimerDvd(dvd);
            access.SupprimerLivre_DvD(dvd);
            return access.SupprimerDocument(document);
        }
    }
}