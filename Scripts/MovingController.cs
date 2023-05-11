using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    private float speed = 0f;
    [SerializeField] float speedFOrInspector;
    private Vector3 direction;

    private Rigidbody rb;

    private void Awake()
    {

    }
    void Start()
    {
        speedFOrInspector = speed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //rb.velocity = Vector3.MoveTowards(transform.position, direction, 40) * speed;
        rb.velocity = direction.normalized * speed;
        //transform.position = Vector3.MoveTowards(transform.position, direction, speedFOrInspector);

    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TagList.Wall))
        {
            speed = 0;
        }
    }
}
