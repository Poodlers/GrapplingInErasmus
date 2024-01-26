using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 5;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    public Sprite deadSprite;

    public HPBar hpBar;

    public GameObject gameOverMenu;
    public bool isDead = false;

    public float invincibleTime = 1f;

    private float invincibleTimer = 0f;

    private bool isInvincible = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInvincible)
        {
            if ((invincibleTimer += Time.deltaTime) >= invincibleTime)
            {
                invincibleTimer = 0f;
                isInvincible = false;
            }
        }
    }

    public void ActivateGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;
        health -= damage;
        isInvincible = true;
        hpBar.DestroyHeart();
        if (health <= 0)
        {

            isDead = true;
            animator.SetTrigger("death");
            spriteRenderer.sprite = deadSprite;

        }
        else
        {
            animator.SetTrigger("isHurt");
        }

    }
}
