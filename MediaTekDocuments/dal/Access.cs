using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatek/";
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        /// </summary>
        private const string PUT = "PUT";

        private const string DELETE = "DELETE";

        private Dictionary<string, Genre> classeurGenres = new Dictionary<string, Genre>();
        private Dictionary<string, Public> classeurPublics = new Dictionary<string, Public>();
        private Dictionary<string, Rayon> classeurRayons = new Dictionary<string, Rayon>();

        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            String authenticationString;
            try
            {
                authenticationString = "admin:adminpwd";
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if (instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre", null);
            return lesLivres;
        }

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd", null);
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue", null);
            return lesRevues;
        }

        public bool AjouterLivreDvD(object livreDvd)
        {
            try
            {
                var docDict = JObject.FromObject(livreDvd);
                var livreDvdFiltré = new
                {
                    id = docDict.ContainsKey("Id") ? docDict["Id"]?.ToString() : null,
                };

                string jsonPayload = JsonConvert.SerializeObject(
                    livreDvdFiltré,
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }
                );

                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                List<Document> liste = TraitementRecup<Document>(POST, "livres_dvd", formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AjouterRevue(object revue)
        {
            try
            {
                var docDict = JObject.FromObject(revue);
                var revueFiltré = new
                {
                    id = docDict.ContainsKey("Id") ? docDict["Id"]?.ToString() : null,
                    periodicite = docDict.ContainsKey("Periodicite") ? docDict["Periodicite"]?.ToString() : null,
                    delaiMiseADispo = docDict.ContainsKey("DelaiMiseADispo") && int.TryParse(docDict["DelaiMiseADispo"]?.ToString(), out int result) ? result : 0
                };

                string jsonPayload = JsonConvert.SerializeObject(
                    revueFiltré,
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }
                );

                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                List<Document> liste = TraitementRecup<Document>(POST, "revue", formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AjouterLivre(object livre)
        {
            try
            {
                var docDict = JObject.FromObject(livre);
                var livreFiltré = new
                {
                    id = docDict.ContainsKey("Id") ? docDict["Id"]?.ToString() : null,
                    isbn = docDict.ContainsKey("Isbn") ? docDict["Isbn"]?.ToString() : null,
                    auteur = docDict.ContainsKey("Auteur") ? docDict["Auteur"]?.ToString() : null,
                    collection = docDict.ContainsKey("Collection") ? docDict["Collection"]?.ToString() : null
                };

                string jsonPayload = JsonConvert.SerializeObject(
                    livreFiltré,
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }
                );

                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                List<Document> liste = TraitementRecup<Document>(POST, "livre", formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AjouterDocument(object document)
        {
            try
            {
                var docDict = JObject.FromObject(document);
                var documentFiltré = new
                {
                    id = docDict.ContainsKey("Id") ? docDict["Id"]?.ToString() : null,
                    titre = docDict.ContainsKey("Titre") ? docDict["Titre"]?.ToString() : null,
                    idRayon = docDict.ContainsKey("IdRayon") ? docDict["IdRayon"]?.ToString() : null,
                    idPublic = docDict.ContainsKey("IdPublic") ? docDict["IdPublic"]?.ToString() : null,
                    idGenre = docDict.ContainsKey("IdGenre") ? docDict["IdGenre"]?.ToString() : null,
                    image = docDict.ContainsKey("Image") ? docDict["Image"]?.ToString() : null
                };

                string jsonPayload = JsonConvert.SerializeObject(
                    documentFiltré,
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }
                );

                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                List<Document> liste = TraitementRecup<Document>(POST, "document", formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AjouterDvd(object dvd)
        {
            try
            {
                var docDict = JObject.FromObject(dvd);
                var dvdFiltre = new
                {
                    id = docDict.ContainsKey("Id") ? docDict["Id"]?.ToString() : null,
                    synopsis = docDict.ContainsKey("Synopsis") ? docDict["Synopsis"]?.ToString() : null,
                    realisateur = docDict.ContainsKey("Realisateur") ? docDict["Realisateur"]?.ToString() : null,
                    duree = docDict.ContainsKey("Duree") ? docDict["Duree"]?.ToString() : null
                };

                string jsonPayload = JsonConvert.SerializeObject(
                    dvdFiltre,
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    }
                );

                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                List<Document> liste = TraitementRecup<Document>(POST, "dvd", formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomRayon"></param>
        /// <returns></returns>
        public string GetIdByNameOfRayon(string nomRayon)
        {
            if (classeurRayons != null && classeurRayons.Count > 0)
            {
                var rayon = classeurRayons.FirstOrDefault(x => x.Value.Libelle == nomRayon);
                if (!string.IsNullOrEmpty(rayon.Key))
                {
                    return rayon.Key;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomPublic"></param>
        /// <returns></returns>
        public string GetIdByNameOfPublic(string nomPublic)
        {
            if (classeurPublics != null && classeurPublics.Count > 0)
            {
                var pub = classeurPublics.FirstOrDefault(x => x.Value.Libelle == nomPublic);
                if (!string.IsNullOrEmpty(pub.Key))
                {
                    return pub.Key;
                }
            }
            return null;
        }

        public string GetIdByNameOfGenre(string nomGenre)
        {
            if (classeurGenres != null && classeurGenres.Count > 0)
            {
                var genre = classeurGenres.FirstOrDefault(x => x.Value.Libelle == nomGenre);
                if (!string.IsNullOrEmpty(genre.Key))
                {
                    return genre.Key;
                }
            }
            return null;
        }

        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
            return lesExemplaires;
        }

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire", "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DictionnaireGenre()
        {
            List<Categorie> genres = GetAllGenres();
            foreach (var genre in genres)
            {
                classeurGenres[genre.Id] = (Genre)genre;
            }
        }

        public void DictionnairePublic()
        {
            List<Categorie> publics = GetAllPublics();
            foreach (var pub in publics)
            {
                classeurPublics[pub.Id] = (Public)pub;
            }
        }

        public void DictionnaireRayon()
        {
            List<Categorie> rayons = GetAllRayons();
            foreach (var rayon in rayons)
            {
                classeurRayons[rayon.Id] = (Rayon)rayon;
            }
        }

        public bool ModifierRevue(Revue revue)
        {
            try
            {
                if (string.IsNullOrEmpty(revue.Id))
                {
                    return false;
                }

                var revueFiltre = new
                {
                    periodicite = revue.Periodicite,
                    delaiMiseADispo = revue.DelaiMiseADispo
                };

                string jsonPayload = JsonConvert.SerializeObject(revueFiltre);
                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                string url = $"revue/{revue.Id}";
                List<Document> liste = TraitementRecup<Document>(PUT, url, formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModifierDvd(Dvd dvd)
        {
            try
            {
                if (string.IsNullOrEmpty(dvd.Id))
                {
                    return false;
                }

                var dvdFiltre = new
                {
                    synopsis = dvd.Synopsis,
                    realisateur = dvd.Realisateur,
                    duree = dvd.Duree.ToString()
                };

                string jsonPayload = JsonConvert.SerializeObject(dvdFiltre);
                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                string url = $"dvd/{dvd.Id}";
                List<Document> liste = TraitementRecup<Document>(PUT, url, formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModifierLivre(Livre livre)
        {
            try
            {
                if (string.IsNullOrEmpty(livre.Id))
                {
                    return false;
                }

                var livreFiltré = new
                {
                    isbn = livre.Isbn,
                    auteur = livre.Auteur,
                    collection = livre.Collection
                };

                string jsonPayload = JsonConvert.SerializeObject(livreFiltré);
                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                string url = $"livre/{livre.Id}";
                List<Document> liste = TraitementRecup<Document>(PUT, url, formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModifierDocument(Document document)
        {
            try
            {
                if (string.IsNullOrEmpty(document.Id))
                {
                    return false;
                }

                var documentFiltre = new
                {
                    titre = document.Titre,
                    image = document.Image,
                    idRayon = document.IdRayon,
                    idGenre = document.IdGenre,
                    idPublic = document.IdPublic
                };

                string jsonPayload = JsonConvert.SerializeObject(documentFiltre);
                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                string url = $"document/{document.Id}";
                List<Document> liste = TraitementRecup<Document>(PUT, url, formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModifierLivre_DvD(LivreDvd livredvd)
        {
            try
            {
                if (string.IsNullOrEmpty(livredvd.Id))
                {
                    return false;
                }

                var LivreDvDFiltre = new
                {
                    id = livredvd.Titre
                };

                string jsonPayload = JsonConvert.SerializeObject(LivreDvDFiltre);
                string formEncodedPayload = $"champs={Uri.EscapeDataString(jsonPayload)}";
                string url = $"document/{livredvd.Id}";
                List<Document> liste = TraitementRecup<Document>(PUT, url, formEncodedPayload);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SupprimerLivre(Livre livre)
        {
            try
            {
                if (string.IsNullOrEmpty(livre.Id))
                {
                    return false;
                }

                string jsonId = Uri.EscapeDataString($"{{\"id\":\"{livre.Id}\"}}");
                string url = $"livre/{jsonId}";
                List<Livre> liste = TraitementRecup<Livre>(DELETE, url, null);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SupprimerDvd(Dvd dvd)
        {
            try
            {
                if (string.IsNullOrEmpty(dvd.Id))
                {
                    return false;
                }

                string jsonId = Uri.EscapeDataString($"{{\"id\":\"{dvd.Id}\"}}");
                string url = $"dvd/{jsonId}";
                List<Livre> liste = TraitementRecup<Livre>(DELETE, url, null);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SupprimerRevue(Revue revue)
        {
            try
            {
                if (string.IsNullOrEmpty(revue.Id))
                {
                    return false;
                }

                string jsonId = Uri.EscapeDataString($"{{\"id\":\"{revue.Id}\"}}");
                string url = $"revue/{jsonId}";
                List<Livre> liste = TraitementRecup<Livre>(DELETE, url, null);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SupprimerLivre_DvD(LivreDvd livre)
        {
            try
            {
                if (string.IsNullOrEmpty(livre.Id))
                {
                    return false;
                }

                string jsonId = Uri.EscapeDataString($"{{\"id\":\"{livre.Id}\"}}");
                string url = $"livres_dvd/{jsonId}";
                List<Livre> liste = TraitementRecup<Livre>(DELETE, url, null);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SupprimerDocument(Document document)
        {
            try
            {
                if (string.IsNullOrEmpty(document.Id))
                {
                    return false;
                }

                string jsonId = Uri.EscapeDataString($"{{\"id\":\"{document.Id}\"}}");
                string url = $"document/{jsonId}";
                List<Livre> liste = TraitementRecup<Livre>(DELETE, url, null);

                if (liste == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée dans l'url</param>
        /// <param name="parametres">paramètres à envoyer dans le body, au format "chp1=val1&chp2=val2&..."</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T>(String methode, String message, String parametres)
        {
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message, parametres);
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
            }
            catch (Exception e)
            {
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit en json un couple nom/valeur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="valeur"></param>
        /// <returns>couple au format json</returns>
        private String convertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();
            dictionary.Add(nom, valeur);
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}