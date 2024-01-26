using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameWinUI;
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Level End");
            gameWinUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
