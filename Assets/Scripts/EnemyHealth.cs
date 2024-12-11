using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int maxHealth = 100;
    private int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibió {damage} de daño. Salud actual: {currentHealth}");

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject); // Elimina al enemigo.
    }
}
