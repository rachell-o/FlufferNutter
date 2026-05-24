using UnityEngine;
using TMPro;
// using System.Threading.Tasks.Dataflow;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] GameObject _singlePanelScore;
    [SerializeField] SOScore _donneesScore; //Contient les données des joueurs.


    void Start()
    {
        _donneesScore.AjouterScore(GameManager.instance.Player_name, GameManager.instance.Love_Interest, GameManager.instance.Won_The_MiniGame, true);  // Charge les données de score à partir du fichier
        // _donneesScore.LesDonneesTableau.Add(new DonneeTableau() { name = GameManager.instance.Player_name, charac = GameManager.instance.Love_Interest, won = GameManager.instance.Won_The_MiniGame, isActualScore = true });  // Ajoute les données du joueur au tableau de score
        _donneesScore.LireFichier();  // Charge les données de score à partir du fichier
        // _donneesScore.lesDonneesTableau.Sort((x, y) => y.nom.CompareTo(x.nom));
        // _donneesScore.lesDonneesTableau.Sort((x, y) => y.score.CompareTo(x.score));

        // if (_donneesScore.lesDonneesTableau.Count > 6) _donneesScore.lesDonneesTableau.RemoveRange(6, 1); // Si le tableau de score dépasse 6 entrées de joueur, on supprime celui en dernière position

        int nbMax = Mathf.Min(_donneesScore.LesDonneesTableau.Count, 10); // Affiche les 5 premiers scores du tableau de score
        for (int i = 0; i < nbMax; i++) // Instancie les panneaux joueurs avec leurs noms et scores
        {
            GameObject panneauJoueur = Instantiate(_singlePanelScore, transform.position, Quaternion.identity, transform);
            PlayerPanel ScrptPanneauJoueur = panneauJoueur.GetComponent<PlayerPanel>(); // Récupère les informations du panneau instantié dans le script du panneau joueur.
            ScrptPanneauJoueur.AfficherDonnees(i);

            // if (i == _donneesScore.lesDonneesTableau.Count - 1) ScrptPanneauJoueur.DesactiverDernierePosition(); // Désactive le panneau joueur en dernière position
        }
    }
}
