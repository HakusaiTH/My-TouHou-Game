using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100; // HP เริ่มต้น
    private int currentHP;

    public bool isNext = false; // HP เริ่มต้น

    public float moveSpeed = 2f; // ความเร็วการเคลื่อนที่
    public float moveDistance = 3f; // ระยะทางที่เคลื่อนที่ไปกลับ

    private Vector3 startPosition; // ตำแหน่งเริ่มต้น
    private bool movingRight = true; // ทิศทางการเคลื่อนที่ (true = ขวา, false = ซ้าย)

    public int scoreReward = 10; // คะแนนที่เพิ่มให้ Player เมื่อศัตรูถูกทำลาย

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP; // กำหนดค่า HP เริ่มต้น
        startPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // ฟังก์ชันลด HP
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ลด HP
        Debug.Log("Enemy HP: " + currentHP);

        // หาก HP เหลือ 0 หรือน้อยกว่า ให้ทำลายศัตรู
        if (currentHP <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันจัดการเมื่อศัตรูตาย
    private void Die()
    {
        if (isNext)
        {
            SceneManager.LoadScene("Level2");
        }

        Debug.Log("Enemy Destroyed");

        // เพิ่มคะแนนให้ Player
        Player.score += scoreReward;

        // อัพเดตคะแนนใน UI
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            playerScript.score_ui.text = "Score: " + Player.score.ToString();
        }

        // ทำลายศัตรู
        Destroy(gameObject);
    }

    // ฟังก์ชันสำหรับการเคลื่อนที่
    private void Move()
    {
        if (movingRight)
        {
            // เคลื่อนที่ไปทางขวา
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // หากถึงระยะที่กำหนด ให้เปลี่ยนทิศทาง
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            // เคลื่อนที่ไปทางซ้าย
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // หากถึงระยะที่กำหนด ให้เปลี่ยนทิศทาง
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }
}
