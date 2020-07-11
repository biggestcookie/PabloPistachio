using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Using default sprites for the time being, uses the color from the SpriteRenderer.
//Can easily change later
public class ColorSelector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        FreeDraw.Drawable.Pen_Colour = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
}
