using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class FileManager
{
    public static List<string> ReadTextFile(string filePath, bool includeBlanklines = true)
    {
        if(!filePath.StartsWith('/')) filePath = FilePaths.root + filePath;

        List<string> lines = new List<string>();
        try
        {
            //Utilise le StreamReader de l'ordinateur pour lire le fichier texte situé au chemin filePath
            using(StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream) //Pendant que le Streamreader n'a pas terminé de lire le fichier texte
                {
                    string line = sr.ReadLine();
                    if(includeBlanklines || !string.IsNullOrWhiteSpace(line)) lines.Add(line);
                  
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError($"File not found: '{ex.FileName}'");
        }

        return lines;
    }

    public static List<string> ReadTextAsset(string filePath, bool includeBlanklines = true)
    {
        TextAsset asset = Resources.Load<TextAsset>(filePath); //l'asset représente le fichier texte contenu dans le dossier resources
        if (asset == null) //si le fichier n'est pas trouvé
        {
            Debug.LogError($"Asset not found: '{filePath}'");
            return null;
        }
        return ReadTextAsset(asset, includeBlanklines);
    }

    public static List<string> ReadTextAsset(TextAsset asset, bool includeBlanklines = true)
    {
        List<string> lines = new List<string>();
        using (StringReader sr = new StringReader(asset.text))
        {
            while(sr.Peek() > -1)
            {
                string line = sr.ReadLine();
                if(includeBlanklines || !string.IsNullOrWhiteSpace(line)) lines.Add(line);
            
            }
        }

        return lines;
    }
}
