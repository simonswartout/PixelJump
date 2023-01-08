using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    public GameObject CurrentWaypoint => waypoints[currentWaypointIndex];
    [SerializeField] float speed = 1f;
    [SerializeField] float rotateSpeed = 1f;

    void Awake()
    {
        foreach (GameObject waypoint in waypoints)
        {
            waypoint.transform.parent = null;
        } //deparents all waypoints so that they are no longer children of the waypoint follower
         
    }

    // Update is called once per frame
    bool IsAtWaypoint => Vector3.Distance(transform.position, CurrentWaypoint.transform.position) < 0.1f;
   
    void Update()
    {
        if(IsAtWaypoint)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0;
            
        }
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(CurrentWaypoint.transform.position - transform.position), rotateSpeed * Time.deltaTime);
    }


}
