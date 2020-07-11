using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Cursor : MonoBehaviour
{
    public float speed;
    public float accel;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Screen.lockCursor = true;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") / 10;
        float y = Input.GetAxis("Mouse Y") / 10;
        float x_dir = x / Mathf.Abs(x);
        float y_dir = y / Mathf.Abs(y);
        Debug.Log("Mouse X: " + x + ", Mouse Y: " + y);
        float step = speed * Time.deltaTime;
        //x = Mathf.MoveTowards(x, speed * Input.GetAxisRaw("Mouse X"), accel * Time.deltaTime);
        //y = Mathf.MoveTowards(y, speed * Input.GetAxisRaw("Mouse Y"), accel * Time.deltaTime);
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(x, y, 0), step);
        //this.gameObject.transform.move += new Vector3(Input.GetAxis("Mouse X") * .25f, Input.GetAxis("Mouse Y") * .25f, 0);
    }

    void OnMouseDown()
    {

    }
}
