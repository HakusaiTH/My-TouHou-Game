using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveX;
    private Rigidbody2D rb;
    float Speed = 10;

    public Text score_ui;
    public Text life_ui;
    public Text maxHP_ui;

    public static int score = 0;
    public static int life = 5;

    public AudioClip coint;
    public AudioClip die_sound;

    private AudioSource audioSource;

    public int maxHP = 100; // HP เริ่มต้น
    private int currentHP;

    // Static HashSet สำหรับเก็บชื่อวัตถุที่ถูก Destroy แล้ว
    private static HashSet<string> destroyedObjects = new HashSet<string>();

    private float previousMousePositionX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // อัพเดตค่า score_ui และ life_ui
        score_ui.text = "Score: " + score.ToString();
        life_ui.text = "Life: " + life.ToString();
        maxHP_ui.text = "HP:  " + maxHP.ToString();

        // ตรวจสอบวัตถุในฉากที่เคยถูก Destroy และทำลายซ้ำ
        DestroyObjectsByTag("Coint");
        DestroyObjectsByTag("Life_item");
        DestroyObjectsByTag("Box_item");

        previousMousePositionX = Mouse.current.position.ReadValue().x;

        currentHP = maxHP; // กำหนดค่า HP เริ่มต้น
    }

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

    private void Die()
    {
        life -= 1;
        life_ui.text = "Life: " + life.ToString();

        // saudioSource.PlayOneShot(die_sound);
        
        if (life <= 0)
        {
            SceneManager.LoadScene("Menu");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }

    // ฟังก์ชันสำหรับทำลายวัตถุตามแท็ก
    private void DestroyObjectsByTag(string tag)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
        {
            if (destroyedObjects.Contains(obj.name))
            {
                Destroy(obj);
            }
        }
    }

    void Update()
    {
        // รีเซ็ตความเร็วในแนวนอนและแนวตั้ง
        float moveX = 0;
        float moveY = 0;

        // ตรวจสอบการเคลื่อนที่ในแนวนอนและแนวตั้งด้วยคีย์บอร์ด
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
            {
                moveY = 1; // ขึ้น
            }
            if (Keyboard.current.sKey.isPressed)
            {
                moveY = -1; // ลง
            }
            if (Keyboard.current.aKey.isPressed)
            {
                moveX = -1; // ซ้าย
            }
            if (Keyboard.current.dKey.isPressed)
            {
                moveX = 1; // ขวา
            }
        }

        rb.velocity = new Vector2(moveX * Speed, moveY * Speed);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // 
        }
    }

    private void Coint()
    {
        score += 20;
        score_ui.text = "Score: " + score.ToString();
    }

    private void HP_item()
    {
        maxHP += 30;
        maxHP_ui.text = "Hp: " + maxHP.ToString();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Coint") || target.gameObject.CompareTag("MaxHP_item") || target.gameObject.CompareTag("Box_item"))
        {
            destroyedObjects.Add(target.gameObject.name);
            audioSource.PlayOneShot(coint);
            Destroy(target.gameObject);
        }

        if (target.gameObject.CompareTag("Coint"))
        {
            Coint();
        }

        if (target.gameObject.CompareTag("MaxHP_item"))
        {
            HP_item();
        }

        if (target.gameObject.CompareTag("Box_item"))
        {
            int randomValue = Random.Range(1, 3);

            if (randomValue == 1)
            {
                HP_item();
            }
            else if (randomValue == 2)
            {
                Coint();
            }
        }
    }
}