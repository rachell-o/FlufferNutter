using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;
    public static SoundManager instance => _instance;
    private AudioSource _as;
    private AudioClip[] _musics;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!BecomeSingleton()) return;
        _as = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Permet l'initialisation d'un singleton dans la scène ou d'en détruire l'instance si un SoundManager est déjà présent sur la scène
    /// </summary>
    /// <returns> Si l'initialisation d'un singleton unique a réussi</returns>
    bool BecomeSingleton()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return false; //échec!
        }
        _instance = this;
        // DontDestroyOnLoad(gameObject);
        return true; //succès!
    }

    public void PlaySound(AudioClip clip)
    {
        _as.PlayOneShot(clip);
    }

    public void ChangeMusic(int musicNb)
    {
        // _as.
    }
}
