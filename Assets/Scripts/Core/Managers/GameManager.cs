using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SONavigation _nav;
    public SONavigation Nav => _nav;
    static GameManager _instance;
    public static GameManager instance => _instance;
    private PlayerInput _pi;
    public PlayerInput Pi => _pi;
    private Camera _camera;
    public Camera Camera => _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!BecomeSingleton()) return;
        _pi = GetComponent<PlayerInput>();
        FindNewCamera();
    }

    public void FindNewCamera()
    {
        _camera = Camera.main;
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
