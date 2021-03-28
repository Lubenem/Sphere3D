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
    [SerializeField] private float _minObstacleDist = 0.35f;

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
        KillAllObstacles();

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 newObstaclePos;

            do
            {
                newObstaclePos = new Vector3(Random.Range(-1.5f, 1.5f),
                0f, Random.Range(-1.7f, 2.3f));
            } while (!DistCheck(newObstaclePos));

            Obstacle obstacle = Instantiate(_obstaclePrefab, _obstacleParent);
            obstacle.transform.localPosition = newObstaclePos;
            obstacles.Add(obstacle);
        }
    }

    private void KillAllObstacles()
    {
        foreach (Transform item in _obstacleParent)
            Destroy(item.gameObject);
    }

    private bool DistCheck(Vector3 newPos)
    {
        foreach (Obstacle obstacle in obstacles)
        {
            if (Vector3.Distance(obstacle.transform.localPosition, newPos) < _minObstacleDist)
                return false;
        }

        return true;
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
        StartCoroutine(RestartRoutine());
    }

    public void FailGame()
    {
        if (!_isGameActive)
            return;

        SoundManager.instance.Play(_failGameSound);
        playerSphere.inputBlock = true;
        _isGameActive = false;
        StartCoroutine(RestartRoutine());
    }

    private IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(3f);
        _isGameActive = true;
        playerSphere.SetStartValues();
        GenerateObstacles();
    }
}