using System;

namespace AppeHousni
{
    public class LigneFacture
    {
        // champs existants (conservés)
        private string libelle;
        private decimal prixUnitaireHT;
        private int quantite;
        private listeEnnum.CategorieProduit categorie;

        // Propriétés publiques pour permettre l'accès et la sérialisation si besoin
        public string Libelle
        {
            get => libelle;
            set => libelle = value;
        }

        public decimal PrixUnitaireHT
        {
            get => prixUnitaireHT;
            set => prixUnitaireHT = value;
        }

        public int Quantite
        {
            get => quantite;
            set => quantite = value;
        }

        // Expose la catégorie pour que Facture / autres composants puissent en tenir compte
        public listeEnnum.CategorieProduit Categorie
        {
            get => categorie;
            set => categorie = value;
        }

        // Constructeur pratique
        public LigneFacture(string libelle, decimal prixUnitaireHT, int quantite, listeEnnum.CategorieProduit categorie)
        {
            this.libelle = libelle ?? string.Empty;
            this.prixUnitaireHT = prixUnitaireHT;
            this.quantite = quantite;
            this.categorie = categorie;
        }
        public LigneFacture() { } // Constructeur sans paramètre pour la sérialisation JSON


        // Calcule le montant HT de la ligne
        public decimal CalculerHT()
        {
            return Math.Round(prixUnitaireHT * quantite, 2, MidpointRounding.AwayFromZero);
        }

        // Calcule la TVA automatiquement selon la catégorie du produit
        public decimal CalculerTVA()
        {
            var taux = TauxTVAParCategorie(categorie);
            var tva = CalculerHT() * taux;
            return Math.Round(tva, 2, MidpointRounding.AwayFromZero);
        }

        // Mappe une catégorie à un taux de TVA. Ajustez les cases pour correspondre à votre enum réel.
        private static decimal TauxTVAParCategorie(listeEnnum.CategorieProduit cat)
        {
            // Exemples de correspondances (modifiez selon vos règles / enum)
            switch (cat)
            {
                // Remplacez les noms ci-dessous par ceux présents dans votre enum `CategorieProduit`
                case listeEnnum.CategorieProduit.Informatique:   // ex: produits alimentaires
                    return 0.15m; // 5.5%
                case listeEnnum.CategorieProduit.Telephonie:         // ex: livres
                    return 0.20m; // 20%
                case listeEnnum.CategorieProduit.Electromenager: // ex: électronique, dispositifs
                    return 0.11m;  // 11%
                case listeEnnum.CategorieProduit.Accessoire:      // ex: prestations de service
                    return 0.20m;  // 20%
                case listeEnnum.CategorieProduit.Autre:     
                    return 0.10m;  // 10%
                default:
                    return 0.10m;  // taux par défaut
            }
        }

        public override string ToString()
        {
            return $"{libelle} ({categorie}): {quantite} x {prixUnitaireHT}e = {CalculerHT():0.00}e HT ";
        }
    }
}