using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private InputHandler input;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public GameObject PauseMenuUI;
    public Button quitButton;

    public bool isPaused = false;

    private void Start()
    {
        input = InputHandler.instance;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void Resume()
    {

        Cursor.lockState = CursorLockMode.Locked;

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        input.pausePressed = false;
        isPaused = false;
    }

    public void Pause()
    {

        //show the mouse
        Cursor.lockState = CursorLockMode.None;

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        input.pausePressed = true;
        isPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void AdvSense()
    {
        input.advSens = true;
    }
}
