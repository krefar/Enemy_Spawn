using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed;

    void Update()
    {
        transform.position = transform.position + transform.forward * _speed * Time.deltaTime;
    }

    public void SetSpeed(float speedValue)
    {
        _speed = speedValue;
    }
}
