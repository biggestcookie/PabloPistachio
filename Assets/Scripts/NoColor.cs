using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoColor : MonoBehaviour
{
    private bool isTouching = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && Input.GetMouseButtonDown(0))
        {
            FreeDraw.Drawable.Pen_Colour = new Color(0f, 0f, 0f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isTouching = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isTouching = false;
    }
}
