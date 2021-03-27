using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private bool _isGameActive = true;

    [SerializeField] private AudioClip _completeGameSound;
    [SerializeField] private AudioClip _failGameSound;
    [SerializeField] private UnityEvent _onComplete;

    public static GameManager instance;
    public Path path;
    public PlayerSphere playerSphere;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void CompleteGame()
    {
        if (!_isGameActive)
            return;

        SoundManager.instance.Play(_completeGameSound);
        playerSphere.KillCurrBullet();
        playerSphere.inputBlock = true;
        _isGameActive = false;
        _onComplete?.Invoke();
    }

    public void FailGame()
    {
        if (!_isGameActive)
            return;

        SoundManager.instance.Play(_failGameSound);
        playerSphere.inputBlock = true;
        _isGameActive = false;
    }
}