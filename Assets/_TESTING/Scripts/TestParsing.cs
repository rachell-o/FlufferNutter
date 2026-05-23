using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        void Start()
        {
            // string line = "Speaker \"Dialogue \\\"Goes In\\\" Here!\" Command(arguments here)";

            SendFilesToParse();

            // DialogueParser.Parse(line);
        }

        void SendFilesToParse()
        {
            List<string> lines = FileManager.ReadTextAsset("textFile", false);

            foreach(string line in lines)
            {
                // if(line == string.Empty)
                //     continue;
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }
    }
}
