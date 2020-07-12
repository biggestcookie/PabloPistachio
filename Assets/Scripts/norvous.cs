using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class norvous : MonoBehaviour
{
    private int dir = 1;
    private float step;
    private float jitter_timer = 0.05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        step = Time.deltaTime;
        jitter_timer -= Time.deltaTime;
        if (jitter_timer < 0)
        {
            dir *= -1;
            jitter_timer = 0.05f;
        }
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(dir, 0f, 0f), step);
    }
}
