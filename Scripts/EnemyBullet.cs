using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed; // ความเร็วกระสุน
    private Rigidbody2D rb;

    public int Damage = 20; // HP เริ่มต้น

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ให้กระสุนเคลื่อนที่ลงในแกน Y (สมมติว่าศัตรูยิงลง)
        rb.velocity = -transform.up * speed; // ยิงลงในทิศทางตรงข้ามแกน Y
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        // ตรวจสอบว่าชนกับ Player หรือไม่
        if (target.gameObject.CompareTag("Player"))
        {
            // ตรวจสอบว่าผู้เล่นมี component Player หรือไม่
            Player player = target.GetComponent<Player>();
            if (player != null)
            {
                // ลด HP ของผู้เล่น
                player.TakeDamage(Damage);
            }

            // ทำลายกระสุนเมื่อชน
            Destroy(gameObject);
        }

        // ทำลายกระสุนหากชนวัตถุอื่นๆ (ถ้าจำเป็น)
        /*
                 if (target.gameObject.CompareTag("Obstacle"))
                {
                    Destroy(gameObject);
                }
         */
    }
}
