using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
// using System.Numerics;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        private PlayerInput _pi;
        [SerializeField] private GameObject _zCharac;
        [SerializeField] private GameObject _aCharac;
        [SerializeField] private GameObject _rCharac;
        [SerializeField] private Transform[] _characPos;
        [SerializeField] private DialogueContainer _dialogueContainer = new DialogueContainer();
        public DialogueContainer dialogueContainer => _dialogueContainer;

        private ConversationManager conversationManager = new ConversationManager();
        public bool isRunningConversation => conversationManager.isRunning;

        static DialogueSystem _instance;
        public static DialogueSystem instance => _instance;
        public InputAction _s;
        public InputAction _space;
        public InputAction _a;
        Coroutine _coroutRef;
        private float _characSpeed = 20f;

        void Awake()
        {
            if (!DevenirInstanceSingleton()) return;
            // _pi = GetComponent<PlayerInput>();
            _pi = GameManager.instance.Pi;
            _s = _pi.actions["S"];
            _space = _pi.actions["Space"];
            _a = _pi.actions["A"];
        }

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
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
            if (_instance != null)
            {
                Destroy(gameObject);
                return false; //échec!
            }
            _instance = this;
            return true; //succès!
        }

        public void MoveCharacter(string character, int pos)
        {
            if (character == "z")
            {
                _coroutRef = StartCoroutine(MoveCharacterCorout(_zCharac, _characPos[pos].position));
            }
            else if (character == "a")
            {

            }
            else if (character == "r")
            {

            }
        }

        private IEnumerator MoveCharacterCorout(GameObject charac, Vector3 pos)
        {
            Vector3 posTempo = new();
            while (Vector3.Distance(charac.transform.position, pos) > 0.1f)
            {
                posTempo = Vector3.MoveTowards(charac.transform.position, pos, _characSpeed * Time.deltaTime);
                charac.transform.position = posTempo;
                yield return null;
            }

            yield break;
        }

        public void Jump(string character, int height)
        {
            if (character == "z")
            {
                _coroutRef = StartCoroutine(JumpCorout(_zCharac, height));
            }
            else if (character == "a")
            {

            }
            else if (character == "r")
            {

            }
        }

        private IEnumerator JumpCorout(GameObject charac, int height)
        {
            Vector3 posTempo = new();
            Vector3 initPos = charac.transform.position;
            Vector3 jumpPos = charac.transform.position + Vector3.up * height;

            while (Vector3.Distance(charac.transform.position, jumpPos) > 0.1f)
            {
                posTempo = Vector3.MoveTowards(charac.transform.position, jumpPos, _characSpeed * Time.deltaTime);
                charac.transform.position = posTempo;
                yield return null;
            }

            while (Vector3.Distance(charac.transform.position, initPos) > 0.1f)
            {
                posTempo = Vector3.MoveTowards(charac.transform.position, initPos, _characSpeed * Time.deltaTime);
                charac.transform.position = posTempo;
                yield return null;
            }

            yield break;
        }

        public void Spin(string character, int turns)
        {
            if (character == "z")
            {
                _coroutRef = StartCoroutine(SpinCorout(_zCharac, turns));
            }
            else if (character == "a")
            {

            }
            else if (character == "r")
            {

            }
        }

        private IEnumerator SpinCorout(GameObject charac, int turns)
        {
            Quaternion startRotation = charac.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0, 360 * turns, 0);
            float startRot = 0;
            float endRot = 360*turns;
            float duration = 0.6f;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);

                // charac.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
                float currentY = Mathf.Lerp(startRot, endRot, t);
                charac.transform.rotation = Quaternion.Euler(0f, currentY, 0f);

                yield return null;
            }

            charac.transform.rotation = Quaternion.Euler(0f, 0f, 0f);;
            yield break;
        }
    }
}
