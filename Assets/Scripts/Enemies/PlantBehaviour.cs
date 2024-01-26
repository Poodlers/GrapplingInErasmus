using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float shootInterval = 2f;

    private float shootTimer = 0f;

    public float yOffSet = 0f;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Shoot()
    {
        animator.SetTrigger("isAttacking");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position + Vector3.up * yOffSet;
        bullet.GetComponent<BulletBehaviour>().SetDirection(spriteRenderer.flipX ? Vector2.right : Vector2.left);
    }
    // Update is called once per frame
    void Update()
    {
        if ((shootTimer += Time.deltaTime) >= shootInterval)
        {
            shootTimer = 0f;
            Shoot();
        }

    }
}
