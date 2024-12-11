using UnityEngine;

public class Presidente : MonoBehaviour
{
    public GameObject cuerdas; // Referencia al objeto que quieres ocultar
    public GameObject Agente;   // Referencia directa al objeto Agente

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona es el agente especificado
        if (collision.gameObject == Agente)
        {
            if (cuerdas != null)
            {
                // Oculta el objeto "cuerdas"
                cuerdas.SetActive(false);
                Debug.Log("El objeto 'cuerdas' ha sido ocultado.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado el objeto 'cuerdas' en el inspector.");
            }
        }
    }
}
