using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource audioSource;


    public GameObject optionsMenuInstructor;
    public GameObject canGrappleUI;
    public GameObject HPBar;

    public Slider slider;

    public Text volumeText;
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        audioSource.volume = slider.value;
    }

    public void OnVolumeChange(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnResume()
    {
        gameObject.SetActive(false);
        optionsMenuInstructor.SetActive(true);
        canGrappleUI.gameObject.SetActive(true);
        HPBar.SetActive(true);
        Time.timeScale = 1;
    }
}
