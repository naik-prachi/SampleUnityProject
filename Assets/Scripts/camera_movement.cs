using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f,0f,-10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public float a_x = 4.26f;
    public float b_x = 7.8f;
    public float a_y = 3f;
    public float b_y = 46.2f;

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(target.position.x,a_x,b_x),
            Mathf.Clamp(target.position.y,a_y,b_y),
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
