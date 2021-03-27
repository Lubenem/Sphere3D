using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private bool _isGameActive = true;

    [SerializeField] private AudioClip _completeGameSound;
    [SerializeField] private AudioClip _failGameSound;
    [SerializeField] private UnityEvent _onComplete;
    [SerializeField] private int enemyCount = 35;
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private Transform _obstacleParent;

    public static GameManager instance;
    public Path path;
    public PlayerSphere playerSphere;

    public List<Obstacle> obstacles;

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
        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        obstacles = new List<Obstacle>();

        for (int i = 0; i < enemyCount; i++)
        {
            float xPos = Random.Range(-1.5f, 1.5f);
            float zPos = Random.Range(-1.7f, 2.3f);

            Obstacle obstacle = Instantiate(_obstaclePrefab, _obstacleParent);
            obstacle.transform.localPosition = new Vector3(xPos, transform.localPosition.y, zPos);
            obstacles.Add(obstacle);
        }
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