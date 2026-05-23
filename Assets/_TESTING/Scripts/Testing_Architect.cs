using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DIALOGUE;
using UnityEngine.InputSystem;
using System.Diagnostics;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        [SerializeField] TestFiles _testFiles;
        DialogueSystem _ds;
        TextArchitect _architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;
        private int _lineCounter = 0;
        private int _nbOfLines;
        string[] lines = new string[5]
        {
            "Hey hey there my dear oldie friend, This is a strange feeling isn't it?",
            "Not being able to breathe- not being able to see-",
            "Bakadam bakadazoum I'm trying to make a long line as an example by the way did you know that eating fish is super duper mega cool depending on the season.",
            "not being able to feel-",
            "but still beaing able to live."
        };

        void Start()
        {
            _ds = DialogueSystem.instance;
            // _ds = DialogueContainer.
            _architect = new TextArchitect(_ds.dialogueContainer._dialogueText);
            _architect.buildMethod = TextArchitect.BuildMethod.fade;
        }

        // Update is called once per frame
        void Update()
        {
            if(bm != _architect.buildMethod)
            {
                _architect.buildMethod = bm;
                _architect.Stop();
            }

            if(DialogueSystem.instance._s.triggered) _architect.Stop();

            if(DialogueSystem.instance._space.triggered)
            {
                if(_architect.isBuilding)
                {
                    if (!_architect.hurryUp)
                        _architect.hurryUp = true;
                    else _architect.ForceComplete();
                }
                else ShowAllTheLines();
                // else _architect.Build(lines[2]);
            }
            // else if(DialogueSystem.instance._a.triggered)
            // {
            //     _architect.Append(lines[2]);
            // }
        }

        private void ShowAllTheLines()
        {
            _nbOfLines = _testFiles.Lines.Count;
            List<string> theLines = _testFiles.Lines;

            _architect.Build(theLines[_lineCounter]);
            _lineCounter++;
            // if(_lineCounter >= _nbOfLines) Debug.Log("NO MORE LINES!!!");
        }
    }
}
