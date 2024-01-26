using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;

    private Vector2 direction = Vector2.zero;

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * this.direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
