using AppeHousni;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using static AppeHousni.listeEnnum;

class Program
{
    static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");

        Console.WriteLine("===== CREATION DU CLIENT =====");

        Console.Write("Nom du client : ");
        string nom = Console.ReadLine();

        Console.Write("Email du client : ");
        string email = Console.ReadLine();
        Console.Write("N° telephone du client : ");
        string telephone = Console.ReadLine();

        Client client = new Client(nom, email, telephone);
        //Facture facture = new Facture(client);
        Facture facture = Facture.deserialisation();
        facture.serialisation();
        bool quitter = false;

        while (!quitter)
        {
            Console.WriteLine("\n====================================");
            Console.WriteLine("----------- FACTURATION ------------");
            Console.WriteLine("====================================");
            Console.WriteLine("1) Ajouter une ligne");
            Console.WriteLine("2) Supprimer une ligne");
            Console.WriteLine("3) Afficher la facture");
            Console.WriteLine("4) Ajouter une remise");
            Console.WriteLine("0) Quitter");
            Console.Write("Choix : ");

            string choix = Console.ReadLine()?.Trim() ?? "";

            switch (choix)
            {
                case "1":
                    Console.Write("Libellé : ");
                    string libelle = Console.ReadLine();

                    Console.WriteLine("Choisir catégorie :");
                    Console.WriteLine("1) Informatique");
                    Console.WriteLine("2) Telephonie");
                    Console.WriteLine("3) Electromenager");
                    Console.WriteLine("4) Accessoire");
                    Console.WriteLine("5) Autre");

                    int choixCat = LireInt("Votre choix : ");

                    CategorieProduit categorie = (CategorieProduit)(choixCat - 1);

                    decimal prix = LireDecimal("Prix unitaire HT : ");
                    int quantite = LireInt("Quantité : ");

          

                    LigneFacture ligne = new LigneFacture(libelle, prix, quantite, categorie);
                    facture.AjouterLigne(ligne);
                    Console.WriteLine("Ligne ajoutée avec succès !");
                    facture.serialisation();
                    break;

                case "2":
                    if (facture.Lignes.Count == 0)
                    {
                        Console.WriteLine("Aucune ligne à supprimer.");
                        break;
                    }

                    Console.WriteLine("\nLignes actuelles :");
                    for (int i = 0; i < facture.Lignes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}) {facture.Lignes[i]}");
                    }

                    int index = LireInt("Numéro de ligne à supprimer : ") - 1;

                    bool supprime = facture.SupprimerLigne(index);

                    if (supprime)
                        Console.WriteLine("Ligne supprimée.");
                    else
                        Console.WriteLine("Index invalide !");
                    facture.serialisation();
                    break;

                case "3":
                    Console.WriteLine("\n========== FACTURE ==========");
                    Console.WriteLine(facture.Client.ToString());
                    Console.WriteLine("-----------------------------");

                    if (facture.Lignes.Count == 0)
                    {
                        Console.WriteLine("(Aucune ligne)");
                    }
                    else
                    {
                        for (int i = 0; i < facture.Lignes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {facture.Lignes[i]}");
                        }

                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"Total HT  : {facture.CalculerTotalHT():0.00}e");
                        Console.WriteLine($"Total TVA : {facture.CalculerTotalTVA():0.00}e");
                        Console.WriteLine($"Total TTC : {facture.CalculerTotalTTC():0.00}e");

                        if (facture.Remise > 0)
                        {
                            Console.WriteLine($"Remise    : {facture.Remise:0.00}%");
                            Console.WriteLine($"Montant remise : -{facture.CalculerMontantRemise():0.00}e");
                            Console.WriteLine($"Total final : {facture.CalculerTotalFinal():0.00}e");
                        }

                        Console.WriteLine("=============================");
                    }
                    break;
                case "4":
                    decimal remise = LireDecimal("Pourcentage de remise : ");
                    facture.DefinirRemise(remise);
                    Console.WriteLine("Remise appliquée !");
                    facture.serialisation();
                    break;

                case "0":
                    quitter = true;
                    break;

                default:
                    Console.WriteLine("Choix invalide !");
                    break;
            }
        }
    }

    static int LireInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string s = Console.ReadLine()?.Trim() ?? "";

            if (int.TryParse(s, out int valeur) && valeur >= 0)
                return valeur;

            Console.WriteLine("Entrée invalide, recommence.");
        }
    }

    static decimal LireDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            string s = Console.ReadLine()?.Trim() ?? "";

            if (decimal.TryParse(s, out decimal valeur) && valeur >= 0)
                return valeur;

            Console.WriteLine("Entrée invalide, recommence.");
        }
    }
}