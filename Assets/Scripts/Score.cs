using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private bool temp = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!temp && TestCVScript.gameEnded)
        {
            this.GetComponent<UnityEngine.UI.Text>().text = "Your score is: " + TestCVScript.score.ToString();
            temp = true;
        }

    }
}
