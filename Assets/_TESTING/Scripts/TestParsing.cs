using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        private List<string> _lines = new();
        public List<string> Lines => _lines;
        private List<DIALOGUE_LINE> _dlList = new();
        public List<DIALOGUE_LINE> DlList => _dlList;
        int _textFileCounter = 0;
        string _textFileName = "textFile_";
        void Start()
        {
            SendFilesToParse();
        }

        public void SendFilesToParse()
        {
            List<string> _lines = FileManager.ReadTextAsset(_textFileName + _textFileCounter, false);
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
