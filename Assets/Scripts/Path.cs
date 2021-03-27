using UnityEngine;

public class Path : MonoBehaviour
{
    private Collider _collider;
    private Obstacle[] _obstacles;

    private void Start()
    {
        _obstacles = FindObjectsOfType<Obstacle>();
        _collider = GetComponent<Collider>();
        GameManager.instance.path = this;
    }

    public void PathCheck()
    {
        foreach (var obstacle in _obstacles)
        {
            Collider obstacleCollider = obstacle.obstacleCollider;
            if (_collider.bounds.Intersects(obstacleCollider.bounds))
            {
                if (GameManager.instance.playerSphere.inputBlock)
                    GameManager.instance.FailGame();
                return;
            }
        }

        GameManager.instance.CompleteGame();
    }
}