using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner _spawner;

    public void Init(EnemySpawner spawner)
    {
        _spawner = spawner;

        transform.position = _spawner.transform.position;
        transform.rotation = _spawner.transform.rotation;

        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        _spawner.Release(this);
    }
}
