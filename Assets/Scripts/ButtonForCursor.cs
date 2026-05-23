using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonForCursor : MonoBehaviour
{
    [SerializeField] AudioClip _clicSound;
    [SerializeField] UnityEvent _even;
    [SerializeField] Sprite _highlightedSprite;
    Sprite _normalSprite;
    SpriteRenderer _sr;
    // Image _img;

    void Start()
    {
        if (_highlightedSprite != null)
        {
            _sr = GetComponent<SpriteRenderer>();
            _normalSprite = _sr.sprite;
        }
    }

    public void ChangeSpirte(bool highlight)
    {
        if (_highlightedSprite != null)
        {
            if (highlight && _sr.sprite != _highlightedSprite) _sr.sprite = _highlightedSprite;
            else if (!highlight && _sr.sprite == _highlightedSprite) _sr.sprite = _normalSprite;
        }
    }

    public void TriggerEvent()
    {
        Debug.Log("Je suis Triggered!");
        SoundManager.instance.PlaySound(_clicSound);
        _even.Invoke();
    }
}
