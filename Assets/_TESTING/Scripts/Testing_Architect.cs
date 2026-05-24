using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using DIALOGUE;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        [SerializeField] TestFiles _testFiles;
        [SerializeField] TestParsing _testParsing;
        [SerializeField] Phone _phone;
        DialogueSystem _ds;
        TextArchitect _architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;
        private int _lineCounter = 0;
        private int _nbOfLines;
        private bool _canContinue = true;

        void Start()
        {
            _ds = DialogueSystem.instance;
            if (_phone != null) _phone.test_archRef = gameObject.GetComponent<Testing_Architect>();
            // _ds = DialogueContainer.
            _architect = new TextArchitect(_ds.dialogueContainer._dialogueText);
            _architect.buildMethod = TextArchitect.BuildMethod.fade;
        }

        void Update()
        {
            if (bm != _architect.buildMethod)
            {
                _architect.buildMethod = bm;
                _architect.Stop();
            }

            // if (DialogueSystem.instance._s.triggered) _architect.Stop();

            if (DialogueSystem.instance._space.triggered && _canContinue)
            {
                if (_architect.isBuilding)
                {
                    if (!_architect.hurryUp)
                        _architect.hurryUp = true;
                    else _architect.ForceComplete();
                }
                else ShowLines();
                // else _architect.Build(lines[2]);
            }
            // else if(DialogueSystem.instance._a.triggered)
            // {
            //     _architect.Append(lines[2]);
            // }
        }

        private void ShowLines()
        {
            _nbOfLines = _testParsing.DlList.Count;

            if (_lineCounter < _nbOfLines)
            {
                List<DIALOGUE_LINE> theLines = _testParsing.DlList;

                DialogueSystem.instance.dialogueContainer._nameText.text = theLines[_lineCounter].speaker;
                _architect.Build(theLines[_lineCounter].dialogue);


                Regex regex = new Regex(@"(\w+)\((.*?)\)");
                Match match = regex.Match(theLines[_lineCounter].commands);

                if (theLines[_lineCounter].commands != "")
                {
                    if (match.Success && theLines[_lineCounter].commands.Contains("Move"))
                    {
                        string commandName = match.Groups[1].Value;
                        string parameterString = match.Groups[2].Value;

                        Debug.Log("Command: " + commandName);

                        string[] parameters = parameterString.Split(',');

                        DialogueSystem.instance.MoveCharacter(parameters[0], int.Parse(parameters[1]));
                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("Jump")) //===JUMP
                    {
                        string commandName = match.Groups[1].Value;
                        string parameterString = match.Groups[2].Value;

                        Debug.Log("Command: " + commandName);

                        string[] parameters = parameterString.Split(',');

                        DialogueSystem.instance.Jump(parameters[0], int.Parse(parameters[1]));
                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("Spin")) //===SPIN
                    {
                        string commandName = match.Groups[1].Value;
                        string parameterString = match.Groups[2].Value;

                        Debug.Log("Command: " + commandName);

                        string[] parameters = parameterString.Split(',');

                        DialogueSystem.instance.Spin(parameters[0], int.Parse(parameters[1]));
                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("PlaySound"))
                    {

                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("Anim")) //===ANIM
                    {
                        string commandName = match.Groups[1].Value;
                        string parameterString = match.Groups[2].Value;

                        Debug.Log("Command: " + commandName);

                        string[] parameters = parameterString.Split(',');

                        if (_phone != null)
                        {
                            _phone.ActivateAnim(int.Parse(parameters[0]));
                            if (int.Parse(parameters[0]) == 0 || int.Parse(parameters[0]) == 4)
                            {
                                ChangeTextBox(true);
                            }
                        }
                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("ChangeSprite"))
                    {

                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("ChangeBG"))
                    {

                    }
                    else if (match.Success && theLines[_lineCounter].commands.Contains("Fade"))
                    {

                    }
                }
            }

            _lineCounter++;
            if (_lineCounter >= _nbOfLines) Debug.Log("NO MORE LINES!!!");
        }

        public void ChangeTextBox(bool hide)
        {
            if (hide)
            {
                _canContinue = false;
                _ds.dialogueContainer._root.SetActive(false);
            }
            else
            {
                _canContinue = true;
                _ds.dialogueContainer._root.SetActive(true);
            }
        }
    }
}
