using UnityEngine;

public class Path : MonoBehaviour
{
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        GameManager.instance.path = this;
    }

    public void PathCheck()
    {
        foreach (Obstacle obstacle in GameManager.instance.obstacles)
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