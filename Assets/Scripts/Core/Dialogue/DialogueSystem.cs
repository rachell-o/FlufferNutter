using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        private PlayerInput _pi;
        [SerializeField] private DialogueContainer _dialogueContainer = new DialogueContainer();
        public DialogueContainer dialogueContainer => _dialogueContainer;

        private ConversationManager conversationManager = new ConversationManager();
        public bool isRunningConversation => conversationManager.isRunning;

        static DialogueSystem _instance;
        public static DialogueSystem instance => _instance;
        public InputAction _s;
        public InputAction _space;
        public InputAction _a;
        
        void Awake()
        {
            if(!DevenirInstanceSingleton()) return;
            _pi = GetComponent<PlayerInput>();
            _s = _pi.actions["S"];
            _space = _pi.actions["Space"];
            _a = _pi.actions["A"];
        }

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() {$"{speaker} \"{dialogue}\""};
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }

        /// <summary>
        /// Permet l'initialisation d'un singleton dans la scène ou d'en détruire l'instance si un SoundManager est déjà présent sur la scène
        /// </summary>
        /// <returns> Si l'initialisation d'un singleton unique a réussi</returns>
        bool DevenirInstanceSingleton()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
                return false; //échec!
            }
            _instance = this;
            return true; //succès!
        }
    }
}
