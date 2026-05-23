using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// #TP3
/// Scriptable Object pour le changement de scène. Principalement utilisé sur les boutons
/// Auteurs du code: Alexis Paquette (légèrement inspiré du code de Jonathan Tremblay)
/// Auteur des commentaires: Alexis Paquette & Soraya Thierry
/// </summary>

[CreateAssetMenu(fileName = "myNavigation", menuName = "Navigation")]
public class SONavigation : ScriptableObject
{
    /// <summary>
    /// Changement pour la scène suivante selon l'index
    /// </summary>
    public void GoNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Changement pour la scène précédente selon l'index
    /// </summary>
    public void GoEarlierScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Fonction pour charger une scène par son nom
    /// </summary>
    /// <param name="nomScene">Nom de la scène à charger</param>
    public void GetSceneByName(string nomScene)
    {
        SceneManager.LoadScene(nomScene); 
    }

}
