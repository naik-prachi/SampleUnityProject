using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SawControl_Horizontal : MonoBehaviour
{

    private float moveDistance;
    [SerializeField] private float speed;
    private bool movingup;
    private float leftEdge;
    private float rightEdge;
    [SerializeField] private GameObject obj;
    private SpriteRenderer sred;

    // Start is called before the first frame update
    void Start()
    {
        sred = obj.GetComponent<SpriteRenderer>();
        moveDistance = sred.bounds.size.x;
        leftEdge = transform.position.x;
        rightEdge = transform.position.x + moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingup)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x+ speed * Time.deltaTime , transform.position.y, transform.position.z);
            }
            else
            {
                movingup = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x- speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingup = true;
            }
        }
    }
}
