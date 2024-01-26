using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;

    public float yOffSet = 1f;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,
        yOffSet + target.position.y, -10f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector3.Slerp(transform.position,
        newPos, followSpeed * Time.deltaTime);
    }
}
