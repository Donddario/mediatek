﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Formulaire d'ajout des Dvd
    /// </summary>
    public partial class FrmAjouterDvD : Form
    {
        // Création des variables
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();
        private FrmMediatekController controller = new FrmMediatekController();
        private FrmMediatek frmMediatek;

        /// <summary>
        /// Initialisation de la fenêtre
        /// </summary>
        /// <param name="frmMediatek"></param>
        public FrmAjouterDvD(FrmMediatek frmMediatek)
        {
            InitializeComponent();
            this.frmMediatek = frmMediatek;
        }

        /// <summary>
        /// Bouton qui appelle différentes méthodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ajouterDvD_Click(object sender, EventArgs e)
        {
            AjouterDvD();
            frmMediatek.lesDvd = controller.GetAllDvd();
            frmMediatek.RemplirDvdListeComplete();
        }

        /// <summary>
        /// Méthode d'ajout d'un DvD
        /// </summary>
        private void AjouterDvD()
        {
            // Vérifie que tous les champs sont remplis
            if (string.IsNullOrWhiteSpace(txbDvdTitre.Text) ||
                cb_genre.SelectedItem == null ||
                cb_public.SelectedItem == null ||
                cb_rayon.SelectedItem == null ||
                txbId == null)
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dvd dvd = new Dvd(
                txbId.Text,
                txbDvdTitre.Text,
                txbDvdImage.Text,
                int.Parse(txbDuree.Text),
                txbRealisateur.Text,
                txbSynopsis.Text,
                controller.GetIdByNameOfGenre(cb_genre.Text), cb_genre.Text,
                controller.GetIdByNameOfPublic(cb_public.Text), cb_public.Text,
                controller.GetIdByNameOfRayon(cb_rayon.Text), cb_rayon.Text

            );
            // Appelle l'API pour ajouter le livre
            bool succes = controller.AjouterDvd(dvd);

            if (succes)
            {
                MessageBox.Show("Dvd ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout du dvd.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Actions au chargement de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAjouterDvD_Load(object sender, EventArgs e)
        {
            //remplir le cb_genre de tous les genres
            RemplirCombo(controller.GetAllGenres(), bdgGenres, cb_genre);

            //remplir le cb_public de tous les différents publics
            RemplirCombo(controller.GetAllPublics(), bdgPublics, cb_public);

            //remplir le cb_rayon de tous les différents rayons
            RemplirCombo(controller.GetAllRayons(), bdgRayons, cb_rayon);
        }

        /// <summary>
        /// Méthode qui remplit les différents combobox
        /// </summary>
        /// <param name="lesCategories"></param>
        /// <param name="bdg"></param>
        /// <param name="cbx"></param>
        public void RemplirCombo(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Actions sur le bouton Parcourir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_parcourir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Configurer le filtre pour afficher uniquement les fichiers image
                openFileDialog.Filter = "Images (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tous les fichiers (*.*)|*.*";
                openFileDialog.Title = "Sélectionner une image";

                // Ouvrir la boîte de dialogue et vérifier si l'utilisateur a sélectionné un fichier
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Mettre à jour le champ texte avec le chemin du fichier sélectionné
                    txbDvdImage.Text = openFileDialog.FileName;

                    try
                    {
                        // Charger et afficher l'image dans le PictureBox
                        pcbLivresImage.Image = new Bitmap(openFileDialog.FileName);
                        pcbLivresImage.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors du chargement de l'image : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}