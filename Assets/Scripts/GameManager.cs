using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject ImageComparer;
    public GameObject bikemen;
    public GameObject angery;
    public GameObject nerbous;
    public GameObject elefun;
    public GameObject wind;
    public float timer = 60f;
    public GameObject cursor;
    public GameObject canvas;
    public GameObject timerBarObj;
    private Slider timerBar;
    // Start is called before the first frame update
    void Start()
    {
        timerBar = timerBarObj.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        angery.SetActive(cursor.GetComponent<Cursor>().bumped);
        nerbous.SetActive(cursor.GetComponent<Cursor>().shaking);
        timerBar.value = timer;
        if (timer <= 0f)
        {
            timer = 0f;
            Finish();
        }
        else
        {
            if (!canvas.GetComponent<Canvas>().isClicking || Time.frameCount % 2 == 0)
            {
                timer -= Time.deltaTime;
            }
        }

    }

    void BumpEvent()
    {
        Instantiate(bikemen);
        cursor.GetComponent<Cursor>().bumped = true;
    }

    void ShakeEvent()
    {
        cursor.GetComponent<Cursor>().shaking = true;
    }

    void RotateEvent()
    {
        Instantiate(wind);
        canvas.GetComponent<Canvas>().rotate = true;
    }

    void JumpEvent()
    {
        if (!canvas.GetComponent<Canvas>().jump && !canvas.GetComponent<Canvas>().fall)
        {
            Instantiate(elefun);
            canvas.GetComponent<Canvas>().jump = true;
        }
    }

    void LightEvent()
    {
        canvas.GetComponent<Canvas>().lights = true;

    }

    void Finish()
    {
        ImageComparer.GetComponent<TestCVScript>().gameEnded = true;
    }
}
