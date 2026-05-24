using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonForCursor : MonoBehaviour
{
    [SerializeField] UnityEvent _even;
    [SerializeField] Sprite _highlightedSprite;
    Sprite _normalSprite;
    SpriteRenderer _sr;
    // Image _img;
    
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _normalSprite = _sr.sprite;
    }

    public void ChangeSpirte(bool highlight)
    {
        if(highlight && _sr.sprite != _highlightedSprite) _sr.sprite = _highlightedSprite;
        else if(!highlight && _sr.sprite == _highlightedSprite) _sr.sprite = _normalSprite;
    }

    public void TriggerEvent()
    {
        Debug.Log("Je suis Triggered!");
        _even.Invoke();
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game button pressed!");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
