using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikemen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float step = 40 * Time.deltaTime;
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(20f, 0f, 0f), step);
    }
}
