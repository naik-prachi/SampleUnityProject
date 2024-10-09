using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SawControl_Vertical : MonoBehaviour
{

    private float moveDistance;
    [SerializeField] private float speed;
    private bool movingup;
    private float topEdge;
    private float bottomEdge;
    [SerializeField] private GameObject obj;
    private SpriteRenderer sred;

    // Start is called before the first frame update
    void Start()
    {
        sred = obj.GetComponent<SpriteRenderer>();
        moveDistance = sred.bounds.size.y;
        topEdge = transform.position.y;
        bottomEdge = transform.position.y + moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingup)
        {
            if (transform.position.y > topEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y- speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingup = false;
            }
        }
        else
        {
            if (transform.position.y < bottomEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y+ speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingup = true;
            }
        }
    }
}
