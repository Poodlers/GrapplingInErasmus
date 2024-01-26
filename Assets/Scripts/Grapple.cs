using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;

    private GameObject player;

    private PlayerHealth playerHealth;

    private DistanceJoint2D distanceJoint2D;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        distanceJoint2D = player.GetComponent<DistanceJoint2D>();
        distanceJoint2D.enabled = false;
        _lineRenderer.enabled = false;

    }

    // Update is called once per frame
    public void OnThrowHook()
    {

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, player.transform.position);
        distanceJoint2D.connectedAnchor = transform.position;

        distanceJoint2D.enabled = true;
        _lineRenderer.enabled = true;

    }
    public void onUnThrowHook()
    {
        distanceJoint2D.enabled = false;
        _lineRenderer.enabled = false;

    }

    void Update()
    {

        if (distanceJoint2D.enabled)
        {

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, player.transform.position);

        }
    }





}
