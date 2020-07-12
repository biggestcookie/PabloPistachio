using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
        if (!canvas.GetComponent<Canvas>().isClicking || Time.frameCount % 2 == 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0f)
        {
            GameOver();
        }
        if (Input.GetKeyDown("a"))
        {
            Instantiate(bikemen);
            cursor.GetComponent<Cursor>().bumped = true;
        }
        if (Input.GetKeyDown("s"))
        {
            cursor.GetComponent<Cursor>().shaking = true;
        }
        if (Input.GetKeyDown("d"))
        {
            Instantiate(wind);
            canvas.GetComponent<Canvas>().rotate = true;
        }
        if (Input.GetKeyDown("f") && !canvas.GetComponent<Canvas>().jump && !canvas.GetComponent<Canvas>().fall)
        {
            Instantiate(elefun);
            Wait();
            canvas.GetComponent<Canvas>().jump = true;
        }

        if (Input.GetKeyDown("l"))
        {
            canvas.GetComponent<Canvas>().lights = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }

    void GameOver()
    {

    }
}
