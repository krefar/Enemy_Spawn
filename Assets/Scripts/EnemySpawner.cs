using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool _enabled;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private Enemy _enemyPrefab;
    

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => CreateEnemy(),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 20
            );
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(nameof(SpawnObjects));
    }

    public void Release(Enemy enemy)
    {
        _pool.Release(enemy);
        
    }

    private IEnumerator SpawnObjects()
    {
        while (_enabled)
        {
            SpawnObject();

            yield return new WaitForSeconds(_repeatRate);
        }
    }

    private Enemy CreateEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.Init(this);

        return enemy;
    }

    private Enemy SpawnObject()
    {
        transform.position = new Vector3(
            Random.Range(-2, 2),
            0,
            Random.Range(-2, 2)
        );

        transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

        return _enabled ? _pool.Get() : null;
    }
}