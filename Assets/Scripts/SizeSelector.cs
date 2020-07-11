using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SizeSelector : MonoBehaviour
{
    public InputField input;
    private int result;
    // Start is called before the first frame update
    void Start()
    {
        result = FreeDraw.Drawable.Pen_Width;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (int.TryParse(input.text, out result))
        {
            FreeDraw.Drawable.Pen_Width = result;
        }
    }
}
