using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();

    }


    public void OnPlayAgain()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        if (audioSource != null) DontDestroyOnLoad(audioSource);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnPlay()
    {
        Debug.Log("Play");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
