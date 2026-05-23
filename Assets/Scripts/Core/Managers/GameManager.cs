using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager instance => _instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!BecomeSingleton()) return;
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
        return true; //succès!
    }
}
