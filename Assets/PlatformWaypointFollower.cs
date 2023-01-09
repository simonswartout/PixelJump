using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    public GameObject CurrentWaypoint => waypoints[currentWaypointIndex];
    [SerializeField] float speed = 1f;
    void Update()
    {
        if (Vector3.Distance(transform.position, CurrentWaypoint.transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint.transform.position, speed * Time.deltaTime);
    }
}

