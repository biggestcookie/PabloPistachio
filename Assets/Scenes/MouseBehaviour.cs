using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class MouseBehaviour : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(double X, double Y);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int rand = Random.Range(0, 1000);
        if (rand == 1)
        {//change is done randomly
            StartCoroutine(WaitAndDoSomething());
        }
    }

    IEnumerator WaitAndDoSomething()
    {
        FreeDraw.Drawable.Pen_Width = 10;
        yield return new WaitForSeconds(1f);//change duration using this
        FreeDraw.Drawable.Pen_Width = 3;
    }
}