using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elefoot : MonoBehaviour
{
    private int flip = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = 30 * Time.deltaTime;
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position - flip * new Vector3(0f, 15f, 0f), step);
        if (this.gameObject.transform.position.y < 2)
        {
            flip = -1;
        }
    }
}
