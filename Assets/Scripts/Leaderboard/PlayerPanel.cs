using UnityEngine;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameText;  // Texte affichant le score du joueur.
    [SerializeField] TextMeshProUGUI _lIText;  // Texte affichant le score du joueur.
    [SerializeField] TextMeshProUGUI _wonText;  // Texte affichant le score du joueur.
    [SerializeField] SOScore _donneesScore;
    public SOScore donneesScore { get => _donneesScore; set => _donneesScore = value; }
    
    void Start()
    {

    }

    /// <summary>
    /// Affiche le nom et le score du joueur dans le panneau.
    /// détermine si le score est celui du joueur actuel.
    /// </summary>
    public void AfficherDonnees(int nb)
    {
        _nameText.text = _donneesScore.LesDonneesTableau[nb].name;
        _lIText.text = _donneesScore.LesDonneesTableau[nb].charac;
        if(_donneesScore.LesDonneesTableau[nb].won) _wonText.text = "won!";
        else _wonText.text = "lost...";
        // if (_donneesScore.lesDonneesTableau[nb].estScoreActuel && nb < 5) ChangerEtatBouton(true); // Active le bouton pour enregistrer et le champ de texte du joueur actuel s'il est dans les 5 premiers
        // else ChangerEtatBouton(false);//sinon on désactive l'interaction avec le bouton et le champ de texte des scores non actuels et de celui en dernière position
    }
}
