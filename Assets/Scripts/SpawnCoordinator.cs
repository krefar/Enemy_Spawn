using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCoordinator : MonoBehaviour
{

    [SerializeField] private float _repeatRate;
    [SerializeField] private GameObjectSpawner[] _spawners;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(EnableRandomSpawner), 0.0f, _repeatRate);
    }

    void EnableRandomSpawner()
    {
        DisableAllSpawners();

        var randomIndex = Random.Range(0, _spawners.Length);

        _spawners[randomIndex].EnableSpawn(true);

    }

    private void DisableAllSpawners()
    {
        var enabledSpawners = _spawners.Where(x => x.SpawnEnabled).ToList();

        foreach (var spawner in enabledSpawners)
        {
            spawner.EnableSpawn(false);
        }
    }
}
