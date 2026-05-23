using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogueSystem _dialogueSystem => DialogueSystem.instance;
        private Coroutine _process = null;
        private bool _isRunning => _process != null;
        public bool isRunning => _isRunning;

        public void StartConversation(List<string> conversation)
        {
            StopConversation();

            _process = _dialogueSystem.StartCoroutine(RunningConversation(conversation));
        }

        public void StopConversation()
        {
            if(!_isRunning)
                return;

            _dialogueSystem.StopCoroutine(_process);
            _process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            
            yield return null;
        }
    }
}
