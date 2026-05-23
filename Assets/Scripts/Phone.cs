using TESTING;
using UnityEngine;

public class Phone : MonoBehaviour
{
    private Animator _anim;
    public Testing_Architect test_archRef;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void ActivateAnim(int num)
    {
        if (num == 0)
        {
            _anim.SetTrigger("OpeningPhone");
        }
    }

    public void OpenDaddle()
    {
        _anim.SetTrigger("OpeningDaddle");
        test_archRef.ChangeTextBox(false);
    }
}
