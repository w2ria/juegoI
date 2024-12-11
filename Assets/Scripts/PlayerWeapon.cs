using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    public new Camera camera;
    public Transform spawner;
    public GameObject bulletPrefab;

    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip shootSound;    // Clip de sonido del disparo
    public AudioClip reloadSound;   // Clip de sonido de recarga

    private bool isWeaponVisible = true; // Estado del arma

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Si no se asigna un AudioSource manualmente, intenta obtenerlo del objeto
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update() {
        RotateTowardsMouse();
        CheckFiring();
        CheckWeaponVisibility();
    }

    private void RotateTowardsMouse() {
        if (!isWeaponVisible) return;

        float angle = GetAngleTowardsMouse();
        transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteRenderer.flipY = angle >= 90 && angle <= 270;
    }

    private float GetAngleTowardsMouse() {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;
        return angle;
    }

    private void CheckFiring() {
        if (Input.GetMouseButtonDown(0)) {
            // Disparar bala solo si el arma está visible
            if (isWeaponVisible) {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = spawner.position;
                bullet.transform.rotation = transform.rotation;
                Destroy(bullet, 2f);
            }

            // Reproducir el sonido del disparo (independiente de la visibilidad)
            PlayShootSound();
        }
    }

    private void CheckWeaponVisibility() {
        if (Input.GetKeyDown(KeyCode.R)) {
            isWeaponVisible = !isWeaponVisible;
            spriteRenderer.enabled = isWeaponVisible;

            // Reproducir el sonido de recarga
            PlayReloadSound();
        }
    }

    private void PlayShootSound() {
        if (audioSource != null && shootSound != null) {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void PlayReloadSound() {
        if (audioSource != null && reloadSound != null) {
            audioSource.PlayOneShot(reloadSound);
        }
    }
}
