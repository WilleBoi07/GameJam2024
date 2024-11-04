using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] int Money = 0;
    int speed = 5;

    [SerializeField] private TextMeshProUGUI CoinText;


    // Update is called once per frame
    void Update()
    {
        CoinText.text = "Credits: " + Money;

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed *  Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
