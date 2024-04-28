using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 0; // Speed of the enemy.
    public List<Transform> waypoints;  // List of waypoints that the enemy will follow.

    private int waypointIndex;  // Index of the current waypoint.
    private float range;  // Distance range for reaching a waypoint.

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Move the enemy towards the current waypoint.
    void Move()
    {
        // Look at the current waypoint.
        transform.LookAt(waypoints[waypointIndex]);

        // Move towards the current waypoint.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the enemy has reached the current waypoint.
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < range)
        {
            // Switch to the next waypoint.
            waypointIndex++;

            // If there are no more waypoints, go back to the first one.
            if (waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
        }
    }
}
