using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector2 vel;
    public bool isPickedUp;
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0,1,0);
        if (isPickedUp)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position+offset,ref vel,smoothTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
