using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    public new Camera camera;
    public Transform spawner;  // Lugar de disparo
    public GameObject bulletPrefab;  // Prefab de la bala

    public AudioSource audioSource; 
    public AudioClip shootSound;   // Sonido del disparo
    public AudioClip reloadSound;  // Sonido de recarga

    private bool isWeaponVisible = true;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        if (isWeaponVisible && Input.GetMouseButtonDown(0)) {
            FireBullet();
        }
    }

    private void FireBullet() {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawner.position;
        bullet.transform.rotation = transform.rotation;  // La bala tiene la misma rotación que el arma

        PlayShootSound();
    }

    private void CheckWeaponVisibility() {
        if (Input.GetKeyDown(KeyCode.R)) {
            isWeaponVisible = !isWeaponVisible;
            spriteRenderer.enabled = isWeaponVisible;
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
