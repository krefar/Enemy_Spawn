using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Init(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        this.gameObject.SetActive(true);
    }

    public void SetDirection(Quaternion direction)
    {
        transform.rotation = direction;
    }
}
