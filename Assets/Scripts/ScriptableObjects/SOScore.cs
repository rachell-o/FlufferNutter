using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Runtime.InteropServices;

/// <summary>
/// base de données qui contient le nom et le score du joueur
/// possède une variable qui va déterminer si le score est celui du joueur actuel
/// </summary>
[System.Serializable]
public class DonneeTableau
{
    public string name;
    public string charac;
    public bool won;
    public bool isActualScore;
}

public class TableauScores
{
    public List<DonneeTableau> scores = new List<DonneeTableau>();
}

/// <summary>
/// Scrpitable object qui permet de lire un fichier
/// il remplace également les anciennes informations sauvegardées par les nouvelles
/// </summary>
[CreateAssetMenu(menuName = "Score/DonneeClassement", fileName = "DonneeClassement")] //va créer une scriptable object dans le menu
public class SOScore : ScriptableObject
{
    [SerializeField] List<DonneeTableau> _lesDonneesTableau = new List<DonneeTableau>(); //liste qui va contenir le score et le nom
    public List<DonneeTableau> LesDonneesTableau
    {
        get => _lesDonneesTableau;
        set => _lesDonneesTableau = value;
    }

    [DllImport("__Internal")]
    private static extern void SynchroniserWebGL();

    [SerializeField] string _fichier = "score.json"; //fichier qui va contenir les informations

    /// <summary>
    /// #tp4 Soraya
    /// lis le fichier nommé score.tim et y récolte les informations
    /// Remplace les les anciennes informations de liste par les nouvelles
    /// </summary>
    public void LireFichier()
    {
        // string fichierEtChemin = Application.persistentDataPath + "/" + _fichier; //chemin de l'emplacement du fichier
        string chemin = Path.Combine(Application.persistentDataPath, _fichier); ; //chemin de l'emplacement du fichier
        Debug.Log(chemin);

        if (File.Exists(chemin)) //si le chemin du fichier
        {
            string contenu = File.ReadAllText(chemin);
            TableauScores data =
                JsonUtility.FromJson<TableauScores>(contenu);
            _lesDonneesTableau = data.scores;
            // JsonUtility.FromJsonOverwrite(contenu, _lesDonneesTableau);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this); //pour que unity sache que le fichier a été modifié
            UnityEditor.AssetDatabase.SaveAssets(); //sauvegarde les changements
#endif
            Debug.Log(contenu);
        }
        else Debug.LogWarning("attention, ce chemin n'existe pas");
    }

    /// <summary>
    /// #tp4 Soraya
    /// Va écrire les informations récoltées dans la liste pour les écrire dans la liste
    /// </summary>
    public void EcrireFichier()
    {
        string chemin = Path.Combine(Application.persistentDataPath, _fichier); //cherche le chemin du fichier score.tim
        Debug.Log("Le chemin!" + chemin);

        TableauScores data = new TableauScores();
        data.scores = _lesDonneesTableau;

        string contenu = JsonUtility.ToJson(data, true); //transorme la liste en Json
        File.WriteAllText(chemin, contenu);

#if UNITY_WEBGL && !UNITY_EDITOR
        SynchroniserWebGL();
#endif

        // if (Application.platform == RuntimePlatform.WebGLPlayer) // Vérifie si l'application est exécutée sur la plateforme WebGL
        // {
        //     SynchroniserWebGL(); // Appelle une fonction JavaScript spécifique pour synchroniser les données avec WebGL
        //     Debug.Log("Hoi webGL!");
        // }

        Debug.Log("Fichier écrit");
    }

    public void AjouterScore(
        string nom,
        string personnage,
        bool victoire,
        bool scoreActuel)
    {
        DonneeTableau nouveauScore = new DonneeTableau();

        nouveauScore.name = nom;
        nouveauScore.charac = personnage;
        nouveauScore.won = victoire;
        nouveauScore.isActualScore = scoreActuel;

        _lesDonneesTableau.Add(nouveauScore);

        EcrireFichier();
    }

    public void ViderScores()
    {
        string chemin =
                Path.Combine(Application.persistentDataPath, _fichier);

        _lesDonneesTableau.Clear();

        if (File.Exists(chemin))
        {
            File.Delete(chemin);

#if UNITY_WEBGL && !UNITY_EDITOR
        SynchroniserWebGL();
#endif

            Debug.Log("Fichier supprimé");
        }

        Debug.Log("Nombre scores restants : " +
                  _lesDonneesTableau.Count);
    }
}
