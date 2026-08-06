using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AppeHousni
{
    public class Facture
    {
        public Client Client { get; set; }
        public List<LigneFacture> Lignes { get; set; }

        private decimal remisePourcentage; 

        public Facture(Client client)
        {
            Client = client;
            Lignes = new List<LigneFacture>();
            remisePourcentage = 0m;
        }

        public void AjouterLigne(LigneFacture ligne)
        {
            Lignes.Add(ligne);
        }

        public bool SupprimerLigne(int index)
        {
            if (index < 0 || index >= Lignes.Count)
                return false;

            Lignes.RemoveAt(index);
            return true;
        }

        public decimal CalculerTotalHT()
        {
            decimal totalHT = 0m;

            foreach (var ligne in Lignes)
                totalHT += ligne.CalculerHT();

            return totalHT;
        }

        public decimal CalculerTotalTVA()
        {
            decimal totalTVA = 0m;

            foreach (var ligne in Lignes)
                totalTVA += ligne.CalculerTVA();

            return totalTVA;
        }

        public decimal CalculerTotalTTC()
        {
            return CalculerTotalHT() + CalculerTotalTVA();
        }

        public void DefinirRemise(decimal pourcentage)
        {
            if (pourcentage >= 0 && pourcentage <= 100)
                remisePourcentage = pourcentage;
        }

        public decimal Remise
        {
            get { return remisePourcentage; }
        }

        public decimal CalculerMontantRemise()
        {
            return CalculerTotalTTC() * (remisePourcentage / 100m);
        }

        public decimal CalculerTotalFinal()
        {
            return CalculerTotalTTC() - CalculerMontantRemise();
        }
        public string serialisation ()
        {
            string filname = "facture.json";
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(filname, json);
            return json;
            
        }
        public static Facture deserialisation()
        {
            string filname = "facture.json";
            string json = File.ReadAllText("facture.json");
            Facture facture = JsonSerializer.Deserialize<Facture>(json);
            return facture;
        }
        }
}