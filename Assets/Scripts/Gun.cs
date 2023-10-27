using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Vector2 direction;

    public Bullet bullet;
    public bool autoShoot;

    public float shootInterval = 0.5f;
    public float shootDelay = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    public bool isActive;
    public Bullet iceBullet;
    float originalBulletSpeed;
    bool bulletsSlowed;

    // Start is called before the first frame update
    void Start()
    {
        originalBulletSpeed = bullet.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        direction = (transform.localRotation * Vector2.right).normalized;
        if(autoShoot)
        {
            if (delayTimer >= shootDelay)
            {
                if (shootTimer >= shootInterval)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else shootTimer += Time.deltaTime;
            }
            else delayTimer += Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
        if (bulletsSlowed) goBullet.SetSpeed(originalBulletSpeed / 3);
        else goBullet.SetSpeed(originalBulletSpeed);
    }

    public void ShootIce()
    {
        GameObject go = Instantiate(iceBullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }

    public void SlowDownBullets()
    {
        bulletsSlowed = true;
    }

    public void ResumeBullets()
    {
        bulletsSlowed = false;
    }
}
