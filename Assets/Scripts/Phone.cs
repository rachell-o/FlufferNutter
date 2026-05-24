using TESTING;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField] GameObject[] _chara;
    private Animator _anim;
    public Testing_Architect test_archRef;
    void Start()
    {
        foreach (GameObject chara in _chara)
        {
            chara.SetActive(false);
        }
        _anim = GetComponent<Animator>();
    }

    public void ActivateAnim(int num)
    {
        if (num == 0)
        {
            _anim.SetTrigger("OpeningPhone");
        }
        else if (num == 1) _anim.SetTrigger("ShowA");
        else if (num == 2) _anim.SetTrigger("ShowR");
        else if (num == 3) _anim.SetTrigger("ShowZ");
        else if (num == 4)
        {
            Invoke("FinalAnim", 1.5f);
        }
    }

    private void FinalAnim()
    {
        foreach (GameObject chara in _chara)
        {
            chara.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void OpenDaddle()
    {
        _anim.SetTrigger("OpeningDaddle");
        test_archRef.ChangeTextBox(false);
    }
}
