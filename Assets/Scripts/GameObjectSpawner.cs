using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField] private bool _enabled;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _gameObjectSpeed = 2f;
    [SerializeField] private int _countObjects;
    [SerializeField] private int _maxSize;

    private ObjectPool<GameObject> _pool;

    public bool SpawnEnabled { get => _enabled; }

    public void Release(Collider other)
    {
        _pool.Release(other.gameObject);
    }

    public void EnableSpawn(bool enable)
    {
        _enabled = enable;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0.0f, _repeatRate);
    }

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_gameObject),
            actionOnGet: (gameObject) => ActionOnGet(gameObject),
            actionOnRelease: (gameObject) => gameObject.SetActive(false),
            actionOnDestroy: (gameObject) => Destroy(gameObject),
            collectionCheck: false,
            defaultCapacity: _countObjects,
            maxSize: _maxSize
            );
    }

    private void ActionOnGet(GameObject gameObject)
    {
        gameObject.transform.position = transform.position;
        gameObject.transform.rotation = transform.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Mover>().SetSpeed(_gameObjectSpeed);
        gameObject.SetActive(true);
    }
    
    private GameObject SpawnObject()
    {
        return _enabled ? _pool.Get() : null;
    }
}
