using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ให้กระสุนเคลื่อนที่ขึ้นในแกน Y
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            // ตรวจสอบว่าศัตรูมี component Enemy หรือไม่
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                // ลด HP ของศัตรู
                enemy.TakeDamage(20);
            }

            // ทำลายกระสุนเมื่อชน
            Destroy(gameObject);
        }
    }
}
