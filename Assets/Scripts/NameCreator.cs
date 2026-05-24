using UnityEngine;
using TMPro;

public class NameCreator : MonoBehaviour
{
    private string[] _adjectives = new string[] { "Fluffy", "Fuzzy", "Cuddly", "Snuggly", "Soft", "Warm", "Cozy", "Huggable", "Adorable", "Cute", "Bizzare", "Strange", "Miraculous", "Evil", "Angry", "Sad", "Super", "Ultra", "Mega", "Normal", "Weird", "Studious", "Meticulous", "Mad", "Impressive", "Happy", "Sleepy", "Bashful"};
    private string[] _nouns = new string[] { "Bastard", "Kyle", "Puppy", "Destroyer", "Bear", "Panda", "Koala", "Sloth", "Fox", "Otter", "Joe", "Desk", "Cable", "Singer", "Artist", "Mastermind", "Colonel", "Legend", "Monk", "Detective", "Cop", "President", "Minister", "Lawyer", "Something", "What", "Denis", "Conrad", "Sophie", "Jessica", "Jenna", "Supper", "Diner", "Desert", "Dessert", "Breakfast" };
    [SerializeField] private TextMeshProUGUI _nameText;
    private string _currentName;

    void Start()
    {
        GenerateRandomName();
    }

    public void GenerateRandomName()
    {
        _currentName = _adjectives[Random.Range(0, _adjectives.Length)] + " " + _nouns[Random.Range(0, _nouns.Length)];
        _nameText.text = _currentName;
    }

    public void SaveName()
    {
        GameManager.instance.Player_name = _currentName;
    }
}
