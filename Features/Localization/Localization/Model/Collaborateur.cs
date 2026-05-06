namespace Localization.Model
{
    public class Collaborateur
    {
        public int Identifiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Poste { get; set; }
        public string Departement { get; set; }
        public string Ville { get; set; }
        public string EmailProfessionnel { get; set; }
        public decimal SalaireMensuel { get; set; }
        public bool EstEnActivite { get; set; }
    }
}
