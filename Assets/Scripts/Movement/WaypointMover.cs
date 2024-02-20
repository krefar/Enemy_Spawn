using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform[] _waypoints;

    private int _currentWaypoint = 0;

    void Update()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        var step = _speed * Time.deltaTime;
        var target = _waypoints[_currentWaypoint].position;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
        transform.LookAt(target);
    }
}
