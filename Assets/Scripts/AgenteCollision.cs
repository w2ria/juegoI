using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AgenteCollision : MonoBehaviour
{
    // Referencias públicas para asignar en el Inspector
    public GameObject agente;       // El GameObject del agente
    public GameObject presidente;  // El GameObject del presidente
    public Tilemap cuerdas;        // El Tilemap que deseas desactivar

    private void Start()
    {
        // Verifica si las referencias están asignadas
        if (agente == null || presidente == null || cuerdas == null)
        {
            Debug.LogWarning("Por favor, asigna las referencias de Agente, Presidente y Tilemap en el Inspector.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto con el que colisiona es el presidente
        if (collision.gameObject == presidente)
        {
            // Desactiva el Tilemap de las cuerdas
            if (cuerdas != null)
            {
                cuerdas.gameObject.SetActive(false);
                Debug.Log("Tilemap cuerdas desactivado.");
            }
            else
            {
                Debug.LogWarning("Tilemap cuerdas no está asignado.");
            }
        }
    }
}
