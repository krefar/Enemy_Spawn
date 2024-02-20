using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Init(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;

        this.gameObject.SetActive(true);
    }
}
