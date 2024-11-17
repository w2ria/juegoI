using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{

    public Rigidbody2D rbd;
    public float fuerza;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rbd.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
        }
    }
}
