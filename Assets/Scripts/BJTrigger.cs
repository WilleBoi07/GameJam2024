using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BJTrigger : MonoBehaviour
{

    bool in_blackjack = false;
    private void OnTriggerEnter2D(Collider Other)
    {
        in_blackjack = true;
    }

    private void OnTriggerExit2D(Collider other)
    {
        in_blackjack = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (in_blackjack = true)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}

