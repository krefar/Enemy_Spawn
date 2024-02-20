using UnityEngine;

public class EnemyReleaser : MonoBehaviour
{
    private EnemySpawner _spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (_spawner != null && TryGetComponent(out Enemy enemy))
        {
            _spawner.Release(enemy);
        }
    }

    public void Init(EnemySpawner spawner)
    {
        _spawner = spawner;
    }
}