using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        public GameObject _root; //Gameobject parent des objets des texte
        public TextMeshProUGUI _nameText; //Objet de texte du nom de personnage dans la scène
        public TextMeshProUGUI _dialogueText; //Objet de texte du dialogue dans la scène
    }
}
