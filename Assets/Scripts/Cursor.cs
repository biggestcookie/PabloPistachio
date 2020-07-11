using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Cursor : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = MouseLocation();
    }

    Vector3 MouseLocation()
    {
        float x = Input.GetAxis("Mouse X") / 10;
        float y = Input.GetAxis("Mouse Y") / 10;
        float step = speed * Time.deltaTime;
        return Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(x, y, 0), step);
    }
}
