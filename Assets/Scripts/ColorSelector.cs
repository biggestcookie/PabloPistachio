using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Using default sprites for the time being, uses the color from the SpriteRenderer.
//Can easily change later
public class ColorSelector : MonoBehaviour
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
            FreeDraw.Drawable.Pen_Colour = this.gameObject.GetComponent<SpriteRenderer>().color;
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
