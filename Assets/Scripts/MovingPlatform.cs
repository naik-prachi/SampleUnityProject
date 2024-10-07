using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;

    int direction = 1;
    public float speed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        Vector2 target = CurrentMovementTarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 CurrentMovementTarget()
    {
        if (direction == 1)
        {
            // move to endPoint
            return endPoint.position;
        }
        else
        {
            // move to startPoint
            return startPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        // just for Debug visualisation
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
}
