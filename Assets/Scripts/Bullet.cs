using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private new Rigidbody2D rigidbody;

    public float speed = 3;
    public int damage = 20; // Daño que inflige la bala.
    public AudioClip impactSound; // Sonido de impacto

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Verifica si la colisión es con un enemigo
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null) {
            enemy.TakeDamage(damage); // Aplica daño al enemigo
        }

        // Reproduce el sonido de impacto
        PlayImpactSound();

        // Destruye inmediatamente la bala
        Destroy(gameObject);
    }

    void FixedUpdate() {
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }

    private void PlayImpactSound() {
        if (impactSound != null) {
            // Crear un objeto temporal para reproducir el sonido
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
            tempAudioSource.clip = impactSound;
            tempAudioSource.volume = 0.3f; // Ajusta el volumen si es necesario
            tempAudioSource.spatialBlend = 0f; // Para que sea sonido 2D
            tempAudioSource.Play();

            // Destruir el objeto temporal después de que el sonido termine
            Destroy(tempAudio, impactSound.length);
        } else {
            Debug.LogWarning("ImpactSound no está asignado.");
        }
    }
}
