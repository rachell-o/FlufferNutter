using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        private List<string> _lines = new();
        public List<string> Lines => _lines;
        private List<DIALOGUE_LINE> _dlList = new();
        public List<DIALOGUE_LINE> DlList => _dlList;
        string _textFileCounter = "0";
        string _textFileName = "textFile_";
        void Awake()
        {
            // SendFilesToParse(_textFileName);
            CheckSceneForTextFile();
        }

        private void CheckSceneForTextFile()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName.Contains("A1"))
            {
                _textFileCounter = "A1";
            }
            else if(sceneName.Contains("A2"))
            {
                _textFileCounter = "A2";
            }
            else if(sceneName.Contains("BEA"))
            {
                _textFileCounter = "BEA";
            }
            else if(sceneName.Contains("GEA"))
            {
                _textFileCounter = "GEA";
            }
            else if(sceneName.Contains("Z1"))
            {
                _textFileCounter = "Z1";
            }
            else if(sceneName.Contains("Z2"))
            {
                _textFileCounter = "Z2";
            }
            else if(sceneName.Contains("BEZ"))
            {
                _textFileCounter = "BEZ";
            }
            else if(sceneName.Contains("GEZ"))
            {
                _textFileCounter = "GEZ";
            }
            else if(sceneName.Contains("R1"))
            {
                _textFileCounter = "R1";
            }
            else if(sceneName.Contains("R2"))
            {
                _textFileCounter = "R2";
            }
            else if(sceneName.Contains("BER"))
            {
                _textFileCounter = "BER";
            }
            else if(sceneName.Contains("GER"))
            {
                _textFileCounter = "GER";
            }else _textFileCounter = "0";
            SendFilesToParse(_textFileName);
        }

        public void SendFilesToParse(string fileName)
        {
            List<string> _lines = FileManager.ReadTextAsset(fileName + _textFileCounter, false);
            _dlList = new();

            foreach (string line in _lines)
            {
                // if(line == string.Empty)
                //     continue;
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
                _dlList.Add(dl);
            }
        }
    }
}
