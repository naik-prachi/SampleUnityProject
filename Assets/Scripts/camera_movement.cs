using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f,0f,-10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(target.position.x,-1.9f,14.65f),
            Mathf.Clamp(target.position.y,0f,44.1f),
            transform.position.z
        );
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        // transform.position = new Vector3(
        //     Mathf.Clamp(target.position.x,-2.8f,14.4f),
        //     Mathf.Clamp(target.position.y,0.1f,44.5f),
        //     transform.position.z
        // );
    }
}
