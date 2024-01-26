using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coinCollection : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinValue = 1;

    public TextMeshProUGUI cointext;

    public AudioClip coinSound;

    private PlayerCoins playerCoins;

    void Start()
    {
        playerCoins = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCoins>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) // when the player collides with the coin
    {
        if (other.gameObject.tag == "Player") // if the player collides with the coin
        {
            playerCoins.GainCoins(coinValue); // add the coin value to the player's total coins
            cointext.text = "COINS: " + playerCoins.totalCoins.ToString(); // update the text
            GameObject speaker = new GameObject();
            speaker.AddComponent<AudioSource>();
            speaker.GetComponent<AudioSource>().clip = coinSound;
            speaker.GetComponent<AudioSource>().Play();
            float sfxLength = speaker.GetComponent<AudioSource>().clip.length;
            Destroy(speaker, sfxLength);
            Destroy(gameObject); // destroy the coin
        }
    }
}
