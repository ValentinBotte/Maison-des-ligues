using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.ObjectModel;
using ComposantNuite;
using BaseDeDonnees;
namespace MaisonDesLigues
{
    public partial class FrmPrincipale : Form
    {

        /// <summary>
        /// constructeur du formulaire
        /// </summary>
        public FrmPrincipale()
        {
            InitializeComponent();
        }
        private Bdd UneConnexion;
        private String TitreApplication;
        private String IdStatutSelectionne = "";
        private String IdAtelierSelectionne = "";
        private List<GroupBox> listGroupBox = new List<GroupBox>();
        /// <summary>
        /// création et ouverture d'une connexion vers la base de données sur le chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrincipale_Load(object sender, EventArgs e)
        {
            UneConnexion = ((FrmLogin)Owner).UneConnexion;
            TitreApplication = ((FrmLogin)Owner).TitreApplication;
            this.Text = TitreApplication;
            GrpBenevole.Visible = false;
            GrpIntervenant.Visible = false;
            GrpLicence.Visible = false;
            /// Ajout de Collection de groupeBox pour affichage plus facile .
            listGroupBox.Add(grbAjoutAtelier);
            listGroupBox.Add(grbAjoutTheme);
            listGroupBox.Add(grbAjoutVacation);
            listGroupBox.Add(grbModifVacation);
            TxtNom.Text = "";
            TxtPrenom.Text = "";
            TxtAdr1.Text = "";
            TxtAdr2.Text = "";
            TxtCp.Text = "";
            TxtVille.Text = "";
            txtTel.Text = "";
            TxtMail.Text = "";
        }
        /// <summary>
        /// gestion de l'événement click du bouton quitter.
        /// Demande de confirmation avant de quitter l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application ?", ConfigurationManager.AppSettings["TitreApplication"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UneConnexion.FermerConnexion();
                Application.Exit();
            }
        }

        private void RadTypeParticipant_Changed(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "RadBenevole":
                    this.GererInscriptionBenevole();
                    break;
                case "RadLicencie":
                    this.GererInscriptionLicencie();
                    break;
                case "RadIntervenant":
                    this.GererInscriptionIntervenant();
                    break;
                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un licencié.
        /// </summary>
        private void GererInscriptionLicencie()
        {

            GrpBenevole.Visible = false;
            GrpIntervenant.Visible = false;
            GrpLicence.Visible = true;
            GrpLicence.Left = 23;
            GrpLicence.Top = 264;
            Utilitaire.CreerDesControles(this, UneConnexion, "VATELIER01", "Rad_", PanFonctionLicencie, "RadioButton", this.rdbStatutLicencie_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbQualiteLicencie, "VQUALITE01");
            CmbQualiteLicencie.Text = "Choisir";
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un intervenant.
        /// </summary>
        private void GererInscriptionIntervenant()
        {

            GrpBenevole.Visible = false;
            GrpLicence.Visible = false;
            GrpIntervenant.Visible = true;
            PanFonctionIntervenant.Visible = true;
            GrpIntervenant.Left = 23;
            GrpIntervenant.Top = 264;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanFonctionIntervenant, "RadioButton", this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierIntervenant, "VATELIER01");
            CmbAtelierIntervenant.Text = "Choisir";

        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        private void GererInscriptionBenevole()
        {

            GrpBenevole.Visible = true;
            GrpBenevole.Left = 23;
            GrpBenevole.Top = 264;
            GrpIntervenant.Visible = false;
            GrpLicence.Visible = false;

            Utilitaire.CreerDesControles(this, UneConnexion, "VDATEBENEVOLAT01", "ChkDateB_", PanelDispoBenevole, "CheckBox", this.rdbStatutIntervenant_StateChanged);
            // on va tester si le controle à placer est de type CheckBox afin de lui placer un événement checked_changed
            // Ceci afin de désactiver les boutons si aucune case à cocher du container n'est cochée
            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox")
                {
                    CheckBox UneCheckBox = (CheckBox)UnControle;
                    UneCheckBox.CheckedChanged += new System.EventHandler(this.ChkDateBenevole_CheckedChanged);
                }
            }


        }
        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutIntervenant_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }
        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutLicencie_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            this.IdAtelierSelectionne = ((RadioButton)sender).Name.Split('_')[1];
        }
        /// <summary>
        /// Permet d'intercepter le click sur le bouton d'enregistrement d'un bénévole.
        /// Cetteméthode va appeler la méthode InscrireBenevole de la Bdd, après avoir mis en forme certains paramètres à envoyer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreBenevole_Click(object sender, EventArgs e)
        {
            Collection<Int16> IdDatesSelectionnees = new Collection<Int16>();
            Int64? NumeroLicence;
            if (TxtLicenceBenevole.MaskCompleted)
            {
                NumeroLicence = System.Convert.ToInt64(TxtLicenceBenevole.Text);
            }
            else
            {
                NumeroLicence = null;
            }


            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).Checked)
                {
                    /* Un name de controle est toujours formé come ceci : xxx_Id où id représente l'id dans la table
                     * Donc on splite la chaine et on récupére le deuxième élément qui correspond à l'id de l'élément sélectionné.
                     * on rajoute cet id dans la collection des id des dates sélectionnées
                        
                    */
                    IdDatesSelectionnees.Add(System.Convert.ToInt16((UnControle.Name.Split('_'))[1]));
                }
            }
            UneConnexion.InscrireBenevole(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToDateTime(TxtDateNaissance.Text), NumeroLicence, IdDatesSelectionnees);

        }
        /// <summary>
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un bénévole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkDateBenevole_CheckedChanged(object sender, EventArgs e)
        {
            BtnEnregistreBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }
        /// <summary>
        /// Méthode qui permet d'afficher ou masquer le controle panel permettant la saisie des nuités d'un intervenant.
        /// S'il faut rendre visible le panel, on teste si les nuités possibles ont été chargés dans ce panel. Si non, on les charges 
        /// On charge ici autant de contrôles ResaNuit qu'il y a de nuits possibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdbNuiteIntervenant_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Name == "RdbNuiteIntervenantOui")
            {
                PanNuiteIntervenant.Visible = true;
                if (PanNuiteIntervenant.Controls.Count == 0) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 5 + (24 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        PanNuiteIntervenant.Controls.Add(unResaNuit);
                    }

                }

            }
            else
            {
                PanNuiteIntervenant.Visible = false;

            }
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();

        }

        /// <summary>
        /// Méthode qui permet d'afficher ou masquer le controle panel permettant la saisie des nuités d'un intervenant.
        /// S'il faut rendre visible le panel, on teste si les nuités possibles ont été chargés dans ce panel. Si non, on les charges 
        /// On charge ici autant de contrôles ResaNuit qu'il y a de nuits possibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdbNuiteLicencie_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Name == "RdbNuiteLicencieOui")
            {
                PanNuiteLicencie.Visible = true;
                if (PanNuiteLicencie.Controls.Count == 0) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 5 + (24 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        PanNuiteLicencie.Controls.Add(unResaNuit);
                    }

                }

            }
            else
            {
                PanNuiteLicencie.Visible = false;

            }
        }

        /// <summary>
        /// Cette procédure va appeler la procédure .... qui aura pour but d'enregistrer les éléments 
        /// de l'inscription d'un intervenant, avec éventuellment les nuités à prendre en compte        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrerIntervenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteIntervenantOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanNuiteIntervenant.Controls)
                    {
                        if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                        {
                            // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                            //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                            CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                            HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                            NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                         }

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MessageBox.Show("Si vous avez sélectionné que l'intervenant avait des nuités\n in faut qu'au moins une nuit soit sélectionnée");
                    }
                    else
                    {
                        UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes);
                        MessageBox.Show("Inscription intervenant effectuée");
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne);
                    MessageBox.Show("Inscription intervenant effectuée");
                    
                }

                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Méthode privée testant le contrôle combo et la variable IdStatutSelectionne qui contient une valeur
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <returns></returns>
        private Boolean VerifBtnEnregistreIntervenant()
        {
            return CmbAtelierIntervenant.Text !="Choisir" && this.IdStatutSelectionne.Length > 0;
        }
        /// <summary>
        /// Méthode permettant de définir le statut activé/désactivé du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAtelierIntervenant_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }
        /// <summary>
        /// Méthode privée testant les contrôles combos
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerLicencie
        /// </summary>
        /// <returns></returns>
        private Boolean VerifBtnEnregistreLicencie()
        {
            return CmbQualiteLicencie.Text != "Choisir";
        }
        /// <summary>
        /// Méthode permettant de définir le statut activé/désactivé du bouton BtnEnregistrerLicencie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAtelierLicencie_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// gestion de l'événement click du bouton quitter.
        /// Demande de confirmation avant de quitter l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitter2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application ?", ConfigurationManager.AppSettings["TitreApplication"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UneConnexion.FermerConnexion();
                Application.Exit();
            }
        }
        /// <summary>
        /// procedure privée permettant de faire appel à des procédure lorsque l'utilisateur check l'un des bouton radio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radGestionTypeTable_changed(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "radAtelier":
                    this.gererGroupBox(grbAjoutAtelier);
                    break;
                case "radTheme":
                    if (radTheme.Checked == true)
                    {
                        this.gererGroupBox(grbAjoutTheme);
                        if (UneConnexion.ObtenirDonnesOracle("atelier").Rows.Count > 0)
                        {
                            cmbAtelierTheme.DataSource = UneConnexion.ObtenirDonnesOracle("ATELIER");
                            cmbAtelierTheme.DisplayMember = "LIBELLEATELIER";
                            cmbAtelierTheme.ValueMember = "ID";
                            cmbAtelierTheme.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Il existe aucun atelier");
                        }
                    }
                    break;
                case "radVacation":
                    if (radVacation.Checked == true)
                    {
                        this.gererGroupBox(grbAjoutVacation);
                        if (UneConnexion.ObtenirDonnesOracle("atelier").Rows.Count > 0)
                        {
                            cmbAtelierVacation.DataSource = UneConnexion.ObtenirDonnesOracle("ATELIER");
                            cmbAtelierVacation.DisplayMember = "LIBELLEATELIER";
                            cmbAtelierVacation.ValueMember = "ID";
                            cmbAtelierVacation.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Il existe aucun atelier");
                        }
                    }
                    break;
                case "radVacModif":
                    if (radVacModif.Checked == true)
                    {
                        this.gererGroupBox(grbModifVacation);
                        if (UneConnexion.ObtenirDonnesOracle("atelier").Rows.Count > 0)
                        {
                            cmbAtelierModif.DataSource = UneConnexion.ObtenirDonnesOracle("ATELIER");
                            cmbAtelierModif.DisplayMember = "LIBELLEATELIER";
                            cmbAtelierModif.ValueMember = "ID";
                            cmbAtelierModif.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Il existe aucun atelier");
                        }
                    }
                    break;
                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }
        private void gererGroupBox(GroupBox gbox)
        {
            this.listGroupBox.ForEach(x => x.Hide());
            gbox.Visible = true;
            gbox.Location = new System.Drawing.Point(46, 145);
        }


        private void btnAjouterAtelier_Click(object sender, EventArgs e)
        {
            if (this.txtLibelleAtelier.Text.Length > 0 && Convert.ToInt32(this.numAtPlacesMax.Value) > 0)
            {
                UneConnexion.ajoutAtelier(Convert.ToString(this.txtLibelleAtelier.Text), Convert.ToInt32(this.numAtPlacesMax.Value));

            }
            else
            {
                MessageBox.Show("Merci de renseigner tout les champs");
            }
        }

        private void btnAjoutTheme_Click(object sender, EventArgs e)
        {
            UneConnexion.ajoutTheme(Convert.ToInt32(this.cmbAtelierTheme.SelectedValue), Convert.ToInt32(this.numAjoutTheme.Value), Convert.ToString(this.txtAjoutTheme.Text));
        }

        private void btnAjoutVacation_Click(object sender, EventArgs e)
        {

            /**Lors du clique bouton, les date et heure de vacation sont convertis en chaîne de caractères afins de les passer en paramètres
             * de la méthode ajoutvacation**/
            DateTime dateHeureDebut; ;
            DateTime dateHeureFin;
            dateHeureDebut = new DateTime(this.dateAjoutVacation.Value.Year, this.dateAjoutVacation.Value.Month, this.dateAjoutVacation.Value.Day, Convert.ToInt32(this.heureDebut.Value), Convert.ToInt32(this.minDebut.Value), 00);
            dateHeureFin = new DateTime(this.dateAjoutVacation.Value.Year, this.dateAjoutVacation.Value.Month, this.dateAjoutVacation.Value.Day, Convert.ToInt32(this.heureFin.Value), Convert.ToInt32(this.minFin.Value), 00);

            if (this.dateAjoutVacation.Value != null && this.cmbAtelierVacation.SelectedValue != null && this.numAjoutVacation.Value != 0)
            {
                string dDebut = Convert.ToString(dateHeureDebut.Day) + "/" + Convert.ToString(dateHeureDebut.Month) + "/" +
                    Convert.ToString(dateHeureDebut.Year) + " " + Convert.ToString(dateHeureDebut.Hour) + ":" +
                    Convert.ToString(dateHeureDebut.Minute) + ":00";


                string dFin = Convert.ToString(dateHeureFin.Day) + "/" + Convert.ToString(dateHeureFin.Month) + "/" +
                    Convert.ToString(dateHeureFin.Year) + " " + Convert.ToString(dateHeureFin.Hour) + ":" +
                    Convert.ToString(dateHeureFin.Minute) + ":00";

                UneConnexion.ajoutVacation(Convert.ToInt32(this.cmbAtelierVacation.SelectedValue), Convert.ToInt32(this.numAjoutVacation.Value), dDebut, dFin);
            }
            else
            {
                MessageBox.Show("Merci de renseigner tout les champs");
            }
        }

        private void btnModifVac_Click(object sender, EventArgs e)
        {
            /**Lors du click, les dates et heures de vacations sont convertis en chaîne de caracteres afin de les passer en parametres de la mathode modif vacation.**/
            DateTime dateHeureDebut; ;
            DateTime dateHeureFin;
            dateHeureDebut = new DateTime(this.dateModifVacation.Value.Year, this.dateModifVacation.Value.Month, this.dateModifVacation.Value.Day, Convert.ToInt32(this.heureDeb.Value), Convert.ToInt32(this.minDeb.Value), 00);
            dateHeureFin = new DateTime(this.dateModifVacation.Value.Year, this.dateModifVacation.Value.Month, this.dateModifVacation.Value.Day, Convert.ToInt32(this.heureEnd.Value), Convert.ToInt32(this.minEnd.Value), 00);

            string dDebut = Convert.ToString(dateHeureDebut.Day) + "/" + Convert.ToString(dateHeureDebut.Month) + "/" +
                Convert.ToString(dateHeureDebut.Year) + " " + Convert.ToString(dateHeureDebut.Hour) + ":" +
                Convert.ToString(dateHeureDebut.Minute) + ":00";


            string dFin = Convert.ToString(dateHeureFin.Day) + "/" + Convert.ToString(dateHeureFin.Month) + "/" +
                Convert.ToString(dateHeureFin.Year) + " " + Convert.ToString(dateHeureFin.Hour) + ":" +
                Convert.ToString(dateHeureFin.Minute) + ":00";

            UneConnexion.modifVacation(Convert.ToInt32(this.cmbAtelierModif.SelectedValue), Convert.ToInt32(this.numModifVacation.Value), dDebut, dFin);

        }

        private void TxtNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void GrpIntervenant_Enter(object sender, EventArgs e)
        {

        }

        private void CmbAtelierIntervenant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ChkDateBenevole_CheckedChanged(object sender, KeyEventArgs e)
        {

        }

        private void BtnEnregistrerLicencie_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteLicencieOui.Checked)
                {
                   
                }
                else
                { // inscription sans les nuitées et sans restauration
                    UneConnexion.InscrireLicencie(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt64(TxtLicenceLicencie.Text), System.Convert.ToInt16(CmbQualiteLicencie.SelectedValue), System.Convert.ToInt32(TxtNumeroCheque.Text), System.Convert.ToInt32(TxtMontantCheque.Text), System.Convert.ToChar("T"));
                    MessageBox.Show("Inscription Licencié effectuée");

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CmbQualiteLicencie_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerLicencie.Enabled = VerifBtnEnregistreLicencie();
        }

        private void PanFonctionIntervenant_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
