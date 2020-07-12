using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timer = 60f;
    private bool slowDown = false;
    private bool slowFlip = true;
    private bool shaking = false;
    private bool bumped = false;
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
        timerBar.value = timer;
        Debug.Log(timer);
        if (canvas.GetComponent<Canvas>().isClicking)
        {
            slowDown = true;
        }
        else
        {
            slowDown = false;
        }
        if (!slowDown || Time.frameCount % 2 == 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0f)
        {
            GameOver();
        }
        if (Input.GetKeyDown("a"))
        {
            cursor.GetComponent<Cursor>().bumped = true;
        }
        if (Input.GetKeyDown("s"))
        {
            cursor.GetComponent<Cursor>().shaking = true;
        }
        if (Input.GetKeyDown("d"))
        {
            canvas.GetComponent<Canvas>().rotate = true;
        }
        if (Input.GetKeyDown("f") && !canvas.GetComponent<Canvas>().jump && !canvas.GetComponent<Canvas>().fall)
        {
            canvas.GetComponent<Canvas>().jump = true;
        }
    }

    void GameOver()
    {

    }
}
