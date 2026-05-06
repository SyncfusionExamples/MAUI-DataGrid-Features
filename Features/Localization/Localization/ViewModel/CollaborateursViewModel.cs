using Localization.Model;
using System.Collections.ObjectModel;

namespace Localization.ViewModel
{
    public class CollaborateursViewModel
    {
        public ObservableCollection<Collaborateur> Collaborateurs { get; }

        private readonly string[] noms = { "Martin", "Dupont", "Bernard", "Petit", "Lefevre", "Moreau" };
        private readonly string[] prenoms = { "Claire", "Julien", "Sophie", "Thomas", "Nicolas", "Laura" };
        private readonly string[] postes = { "Ingénieur Logiciel", "Analyste", "Chef de Projet", "Consultant", "Responsable RH" };
        private readonly string[] departements = { "Informatique", "Finance", "Ressources Humaines", "Marketing" };
        private readonly string[] villes = { "Paris", "Lyon", "Toulouse", "Nantes", "Lille" };

        public CollaborateursViewModel()
        {
            Collaborateurs = new ObservableCollection<Collaborateur>();
            InitialiserDonnees();
        }

        private void InitialiserDonnees()
        {
            var random = new Random();

            for (int i = 1; i <= 100; i++)
            {
                string nom = noms[random.Next(noms.Length)];
                string prenom = prenoms[random.Next(prenoms.Length)];

                Collaborateurs.Add(new Collaborateur
                {
                    Identifiant = i,
                    Nom = nom,
                    Prenom = prenom,
                    DateNaissance = DateTime.Today.AddYears(-random.Next(23, 60)).AddDays(random.Next(365)),
                    Poste = postes[random.Next(postes.Length)],
                    Departement = departements[random.Next(departements.Length)],
                    Ville = villes[random.Next(villes.Length)],
                    EmailProfessionnel = $"{prenom.ToLower()}.{nom.ToLower()}@entreprise.fr",
                    SalaireMensuel = random.Next(2800, 6500),
                    EstEnActivite = random.Next(0, 2) == 1
                });
            }
        }
    }
}
