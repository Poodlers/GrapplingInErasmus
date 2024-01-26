using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private Transform currentPoint;
    public GameObject pointA;

    private Rigidbody2D rigidbody2d;
    public GameObject pointB;

    public float speed = 1f;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        Debug.Log("Current point: " + currentPoint.position);
        if (currentPoint == pointB.transform)
        {

            rigidbody2d.velocity = new Vector2(speed, 0);
        }
        else
        {

            rigidbody2d.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position)
         < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
        else if (Vector2.Distance(transform.position, currentPoint.position)
         < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
    }
}
