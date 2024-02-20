using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform _target;

    void Update()
    {
        var step = _speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
        transform.LookAt(_target.position);
    }

    public void Init(Transform target, float speed)
    {
        _target = target;
        _speed = speed;
    }
}