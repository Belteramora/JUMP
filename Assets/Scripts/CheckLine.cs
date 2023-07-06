using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    Green,
    Red,
    Yellow,
    Blue
}

public class CheckLine : MonoBehaviour
{
    private GameObject green;
    private GameObject red;
    private GameObject yellow;
    private GameObject blue;

    private bool greenBtnPressed;
    private bool redBtnPressed;
    private bool yellowBtnPressed;
    private bool blueBtnPressed;

    private float lastGreenBtnTime;
    private float lastRedBtnTime;
    private float lastYellowBtnTime;
    private float lastBlueBtnTime;

    public float holdTime;

    private void Update()
    {
        if(Time.time >= lastGreenBtnTime + holdTime)
        {
            greenBtnPressed = false;
        }

        if (Time.time >= lastRedBtnTime + holdTime)
        {
            redBtnPressed = false;
        }

        if (Time.time >= lastYellowBtnTime + holdTime)
        {
            yellowBtnPressed = false;
        }

        if (Time.time >= lastBlueBtnTime + holdTime)
        {
            blueBtnPressed = false;
        }

        if(green != null && greenBtnPressed)
        {
            Destroy(green);
        }

        if (red != null && redBtnPressed)
        {
            Destroy(red);
        }

        if (yellow != null && yellowBtnPressed)
        {
            Destroy(yellow);
        }

        if (blue != null && blueBtnPressed)
        {
            Destroy(blue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GreenNote"))
        {
            green = collision.gameObject;
        }

        if (collision.CompareTag("RedNote"))
        {
            red = collision.gameObject;
        }

        if (collision.CompareTag("YellowNote"))
        {
            yellow = collision.gameObject;
        }

        if (collision.CompareTag("BlueNote"))
        {
            blue = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GreenNote"))
        {
            green = null;
        }

        if (collision.CompareTag("RedNote"))
        {
            red = null;
        }

        if (collision.CompareTag("YellowNote"))
        {
            yellow = null;
        }

        if (collision.CompareTag("BlueNote"))
        {
            blue = null;
        }

    }

    public void OnButtonDown(string buttonType)
    {
        switch (buttonType)
        {
            case "Green":
                greenBtnPressed = true;
                lastGreenBtnTime = Time.time;
                break;
            case "Red":
                redBtnPressed = true;
                lastRedBtnTime = Time.time;
                break;
            case "Yellow":
                yellowBtnPressed = true;
                lastYellowBtnTime = Time.time;
                break;
            case "Blue":
                blueBtnPressed = true;
                lastBlueBtnTime = Time.time;
                break;
        }
    }

    public void OnButtonUp(string buttonType)
    {
        switch (buttonType)
        {
            case "Green":
                greenBtnPressed = false;
                break;
            case "Red":
                redBtnPressed = false;
                break;
            case "Yellow":
                yellowBtnPressed = false;
                break;
            case "Blue":
                blueBtnPressed = false;
                break;
        }
    }

    public void OnGreenButtonPressed()
    {
        greenBtnPressed = true;
        lastGreenBtnTime = Time.time;
    }

    public void OnRedButtonPressed()
    {
        redBtnPressed = true;
        lastRedBtnTime = Time.time;
    }

    public void OnYellowButtonPressed()
    {
        yellowBtnPressed = true;
        lastYellowBtnTime = Time.time;
    }

    public void OnBlueButtonPressed()
    {
        blueBtnPressed = true;
        lastBlueBtnTime = Time.time;
    }
}
