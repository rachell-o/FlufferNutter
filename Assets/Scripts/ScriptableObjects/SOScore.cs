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
    public Dictionary<string, string> characAndscore;
    public bool isActualScore;
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

    [SerializeField] string _fichier = "score.tim"; //fichier qui va contenir les informations

    /// <summary>
    /// #tp4 Soraya
    /// lis le fichier nommé score.tim et y récolte les informations
    /// Remplace les les anciennes informations de liste par les nouvelles
    /// </summary>
    public void LireFichier()
    {
        string fichierEtChemin = Application.persistentDataPath + "/" + _fichier; //chemin de l'emplacement du fichier
        Debug.Log(fichierEtChemin);

        if (File.Exists(fichierEtChemin)) //si le chemin du fichier
        {
            string contenu = File.ReadAllText(fichierEtChemin);
            JsonUtility.FromJsonOverwrite(contenu, _lesDonneesTableau); 
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
        string fichierEtChemin = Application.persistentDataPath + "/" + _fichier; //cherche le chemin du fichier score.tim
        string contenu = JsonUtility.ToJson(this); //transorme la liste en Json
        File.WriteAllText(fichierEtChemin, contenu);

        if (Application.platform == RuntimePlatform.WebGLPlayer) // Vérifie si l'application est exécutée sur la plateforme WebGL
        {
            SynchroniserWebGL(); // Appelle une fonction JavaScript spécifique pour synchroniser les données avec WebGL
            Debug.Log("Hoi webGL!");
        }

        Debug.Log("Fichier écrit");
    }
}
