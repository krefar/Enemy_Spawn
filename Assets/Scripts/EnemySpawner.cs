using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Vector3[] _spawnPoints;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () =>
            {
                var spawnPoint = GetRandomSpawnPoint();

                return CreateEnemy(spawnPoint);
            },
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 20
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = GetRandomSpawnPoint();
        enemy.gameObject.SetActive(true);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        var spawnPointIndex = Random.Range(0, _spawnPoints.Length);
        var spawnPoint = _spawnPoints[spawnPointIndex];

        return spawnPoint;
    }

    public void Release(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private IEnumerator SpawnObjects()
    {
        while (enabled)
        {
            SpawnObject();

            yield return new WaitForSeconds(_repeatRate);
        }
    }

    private Enemy CreateEnemy(Vector3 spawnPoint)
    {
        var enemyDirection = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        var enemy = Instantiate(_enemyPrefab);

        enemy.Init(spawnPoint);
        enemy.SetDirection(enemyDirection);

        var releaser = enemy.AddComponent<EnemyReleaser>();
        releaser.Init(this);

        return enemy;
    }

    private Enemy SpawnObject()
    {
        return _pool.Get();
    }
}