using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public Vector2 velocity;
    public float speed = 3;
    public bool isEnemy;
    public bool isIceProjectile;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return;
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
