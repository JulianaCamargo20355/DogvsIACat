using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCat : MonoBehaviour
{
    public float leftX = -3f;   // Limite da esquerda
    public float rightX = 4f;   // Limite da direita
    public float speed = 2f;

    private bool movingRight = true;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
 
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // Limite da direita
        if (transform.position.x >= rightX && movingRight)
        {
            movingRight = false;
            sr.flipX = true;  
        }

        // Limite da esquerda
        if (transform.position.x <= leftX && !movingRight)
        {
            movingRight = true;
            sr.flipX = false; 
        }
    }
}
