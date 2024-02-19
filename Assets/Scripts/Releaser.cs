using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Releaser : MonoBehaviour
{
    [SerializeField] GameObjectSpawner _objectSpawner;

    private void OnTriggerEnter(Collider other)
    {
        _objectSpawner.Release(other);
    }
}
