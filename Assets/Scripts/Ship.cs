using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    float moveSpeed = 5;
    bool moveRight;
    bool moveUp;
    bool moveLeft;
    bool moveDown;
    bool speedUp;
    bool shootIce;

    bool isAlive = true;

    Gun[] guns;
    bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns) gun.isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        shoot = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0);
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns) gun.Shoot();
        }
        shootIce = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse1);
        if (shootIce)
        {
            shootIce = false;
            foreach (Gun gun in guns) gun.ShootIce();
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp) moveAmount *= 1.5f;
        Vector2 move = Vector2.zero;
        if (moveRight) move.x += moveAmount;
        if (moveUp) move.y += moveAmount;
        if (moveLeft) move.x -= moveAmount;
        if (moveDown) move.y -= moveAmount;
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        pos += move;
        if (pos.x <= -0.4f) pos.x = -0.4f;
        if (pos.x >= 14) pos.x = 14;
        if (pos.y <= -4.7f) pos.y = -4.7f;
        if (pos.y >= 3.8f) pos.y = 3.8f;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
        Destructible destructible = collision.GetComponent<Destructible>();
        if (destructible != null)
        {
            Destroy(gameObject);
            Destroy(destructible.gameObject);
        }
        if (bullet != null && bullet.isEnemy || destructible != null)
        {
            isAlive = false;
            SceneManager.LoadScene("Outro");
        }
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }
}
