using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Cursor : MonoBehaviour
{
    public float speed;
    public bool shaking = false;
    public bool bumped = false;
    private int dir = 1;
    public float shaking_timer = 5f;
    private float jitter_timer = 0.05f;
    private float bump_timer = 0.1f;
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
        if (shaking)
        {
            x += dir * .15f;
            step = speed / 4 * Time.deltaTime;
            jitter_timer -= Time.deltaTime;
            shaking_timer -= Time.deltaTime;
            if (jitter_timer < 0)
            {
                dir *= -1;
                jitter_timer = 0.05f;
            }
            if (shaking_timer < 0)
            {
                shaking_timer = 5f;
                shaking = false;
            }
        }
        if (bumped)
        {
            x += .6f;
            bump_timer -= Time.deltaTime;
            if (bump_timer < 0)
            {
                bump_timer = 0.1f;
                bumped = false;
            }
        }
        return Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(x, y, 0), step);
    }
}
