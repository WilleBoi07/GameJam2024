using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardToPoint : MonoBehaviour
{
    [SerializeField]
    public RectTransform target;
    [SerializeField]
    float speed = 100;
    RectTransform rectTransform;
    public bool shouldMove = false;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldMove = true;
            
        }
        if (shouldMove)
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, target.position, speed*Time.deltaTime);
        }
    }
}
