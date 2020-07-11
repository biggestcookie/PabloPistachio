using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;
//Using default sprites for the time being, uses the color from the SpriteRenderer.
//Can easily change later
public class ColorSelector : MonoBehaviour
{
    public GameObject canvas;
    private Drawable drawable;

    // Start is called before the first frame update
    void Start()
    {
        drawable = canvas.GetComponent<Drawable>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        drawable.Pen_Colour = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
}
