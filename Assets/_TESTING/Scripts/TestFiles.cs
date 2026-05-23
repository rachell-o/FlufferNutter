using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

public class TestFiles : MonoBehaviour
{
    [SerializeField] private TextAsset _fileName; //nom du fichier text à lire
    private List<string> _lines = new();
    public List<string> Lines => _lines;

    void Start()
    {
        StartCoroutine(Run());
    }

    /// <summary>
    /// Coroutine qui débute la lecture du fichier texte et affiche les lignes obtenues dans la console
    /// </summary>
    /// <returns></returns>
    IEnumerator Run()
    {
        _lines = FileManager.ReadTextAsset(_fileName, false);

        foreach(string line in _lines) Debug.Log(line);
        yield break;
    }
}
