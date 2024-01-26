using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform playerTransform;

    public float speed = 1f;

    public float enragedSpeed = 2f;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private Rigidbody2D rigidbody2d;

    private Transform currentPoint;

    public float rangetoPlayer = 5f;

    public GameObject pointA;
    public GameObject pointB;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentPoint = pointB.transform;
        animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentPoint == pointB.transform)
        {
            spriteRenderer.flipX = true;
            rigidbody2d.velocity = new Vector2(speed, 0);
        }
        else
        {
            spriteRenderer.flipX = false;
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


    void ChasePlayer()
    {
        //move towards player
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        if (playerTransform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
            rigidbody2d.velocity = new Vector2(-enragedSpeed, 0);
        }
        else
        {
            spriteRenderer.flipX = true;
            rigidbody2d.velocity = new Vector2(enragedSpeed, 0);
        }
    }
}
