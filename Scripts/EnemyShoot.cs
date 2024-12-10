using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootingPoint; // จุดยิงกระสุน
    public GameObject bulletPrefab; // กระสุน
    public float shootInterval = 2f; // ระยะเวลายิงกระสุน (หน่วยวินาที)

    void Start()
    {
        // เริ่ม Coroutine ยิงกระสุน
        StartCoroutine(ShootBullet());
    }

    // Coroutine สำหรับยิงกระสุนทุกๆ 2 วินาที
    IEnumerator ShootBullet()
    {
        while (true)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation); // สร้างกระสุน
            Debug.Log("Enemy Shoot");
            yield return new WaitForSeconds(shootInterval); // รอ 2 วินาทีก่อนยิงครั้งถัดไป
        }
    }
}
