using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject heartPrefab;

    private Stack<GameObject> hearts = new Stack<GameObject>();

    public float xOffSet = 0.5f;


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        for (int i = 0; i < playerHealth.health; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Push(heart);
            heart.transform.position = new Vector3(transform.position.x + i * xOffSet, transform.position.y, transform.position.z);
        }
    }

    public void DestroyHeart()
    {
        GameObject heart = hearts.Pop();
        Destroy(heart);
    }
}
