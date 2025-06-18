using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTimeAtPoint = 1f;

    private int currentWaypointIndex = 0;
    private float waitTimer = 0f;

    private Vector3 lastPosition;
    private List<Transform> passengers = new List<Transform>();

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        MovePlatform();
        MovePassengers();
        lastPosition = transform.position;
    }

    void MovePlatform()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 0.1f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtPoint)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                waitTimer = 0f;
            }
        }
        else
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void MovePassengers()
    {
        Vector3 platformDelta = transform.position - lastPosition;
        foreach (Transform passenger in passengers)
        {
            passenger.position += platformDelta;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!passengers.Contains(other.transform.root))
            {
                passengers.Add(other.transform.root);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            passengers.Remove(other.transform.root);
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);

                // Linie zum nächsten Wegpunkt
                int nextIndex = (i + 1) % waypoints.Length;
                if (waypoints[nextIndex] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[nextIndex].position);
                }
            }
        }
    }
}
