namespace AppeHousni
{
    public class Client
    {
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        public Client(string nom, string email, string telephone)
        {
            Nom = nom;
            Email = email;
            Telephone = telephone;
        }

        public override string ToString()
        {
            return $"Client : {Nom}  \nEmail : {Email} \n Téléphone : {Telephone}";
        }
    }
}