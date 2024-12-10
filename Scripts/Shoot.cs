using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform shootingPoint; // จุดยิง
    public GameObject bulletPrefab; // กระสุน

    public AudioClip shoot_sound;

    private AudioSource audioSource;

    void Update()
    {
        // ตรวจสอบการคลิกเมาส์ซ้าย
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // audioSource.PlayOneShot(shoot_sound);
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            Debug.Log("Shoot");
        }
    }
}
