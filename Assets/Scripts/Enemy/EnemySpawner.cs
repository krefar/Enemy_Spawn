using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Vector3[] _spawnPoints;
    [SerializeField] private EnemyTarget _enemyTarget;
    [SerializeField] private float _enemySpeed = 2f;

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
        var enemy = Instantiate(_enemyPrefab);
        enemy.Init(spawnPoint);

        var releaser = enemy.AddComponent<EnemyReleaser>();
        releaser.Init(this);

        var follower = enemy.AddComponent<Follower>();
        follower.Init(_enemyTarget.transform, _enemySpeed);

        return enemy;
    }

    private Enemy SpawnObject()
    {
        return _pool.Get();
    }
}