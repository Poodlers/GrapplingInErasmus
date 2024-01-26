using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 2;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    public float invincibleTime = 3f;

    private float invincibleTimer = 0f;

    private bool isInvincible = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {

            if ((invincibleTimer += Time.deltaTime) >= invincibleTime)
            {
                invincibleTimer = 0f;
                isInvincible = false;
                StopCoroutine(Blink());
            }
        }
    }

    //blink animation
    IEnumerator Blink()
    {
        while (isInvincible)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (isInvincible) return;

        if (other.gameObject.tag == "Player")
        {
            //check if collision is from above
            if (other.gameObject.transform.position.y - 1f > transform.position.y)
            {
                animator.SetTrigger("isHit");

                health--;
                isInvincible = true;
                StartCoroutine(Blink());
                if (health <= 0)
                {

                    Destroy(gameObject);
                }
            }
            else
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            }
        }
    }
}
