using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button : MonoBehaviour
{
    public Object nextLevel;
    private bool isTouching = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && Input.GetMouseButtonDown(0))
        {
            if (this.gameObject.name == "PlayAgain")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (this.gameObject.name == "NextLevel")
            {
                SceneManager.LoadScene(nextLevel.name);
            }
            else if (this.gameObject.name == "Exit")
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isTouching = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isTouching = false;
    }
}
