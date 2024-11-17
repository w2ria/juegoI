using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject;
    Vector2 startPosition;
    float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    // Calcular la distancia desde el sujeto
    float distanceFromSubject => transform.position.z - subject.position.z;

    // Calcular el plano de recorte, con protección contra valores incorrectos
    float clippingPlane
    {
        get
        {
            float plane = cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane);
            return Mathf.Abs(plane) > 0 ? plane : 1f;  // Evitar que sea 0
        }
    }

    // Factor de paralaje, evitando divisiones por 0
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    public void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    public void Update()
    {
        Vector2 newPos = startPosition + travel * parallaxFactor;

        // Asegurarse de que el nuevo valor no contenga NaN
        if (!float.IsNaN(newPos.x) && !float.IsNaN(newPos.y))
        {
            transform.position = new Vector3(newPos.x, newPos.y, startZ);
        }
        else
        {
            Debug.LogWarning("Parallax: Calculated position resulted in NaN, skipping update.");
        }
    }
}

