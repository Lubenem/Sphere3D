using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip sound, float volume = 1f)
    {
        _audioSource.clip = sound;
        _audioSource.volume = volume;
        _audioSource.Play();
    }
}