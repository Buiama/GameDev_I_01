using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    bool canBeDestroyed;
    public int PointsValue = 100;

    bool isFrozen;
    float frozenTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.instance.AddEnemy();
        isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 9 && !canBeDestroyed)
        {
            canBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns) gun.isActive = true;
        }
        if (isFrozen)
        {
            frozenTimer -= Time.deltaTime;
            if (frozenTimer <= 0)
            {
                isFrozen = false;
                MoveLeft moveLeft = GetComponent<MoveLeft>();
                if (moveLeft != null)
                {
                    moveLeft.UnfreezeMove();
                }

                // If the enemy has guns, resume bullet speed
                Gun[] guns = GetComponentsInChildren<Gun>();
                foreach (Gun gun in guns)
                {
                    gun.ResumeBullets();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed) return;
        Bullet bullet = collision.GetComponent<Bullet>();
        if(bullet != null && !bullet.isEnemy)
        {
            if (bullet.isIceProjectile)
            {
                FreezeEnemy();
            }
            else
            {
                UnfreezeEnemy();
                LevelController.instance.EditScoreValue(PointsValue);
                Destroy(gameObject);
            }
            Destroy(bullet.gameObject);
        }
    }

    public void FreezeEnemy()
    {
        isFrozen = true;
        frozenTimer = 2f;
        MoveLeft moveLeft = GetComponent<MoveLeft>();
        if (moveLeft != null) moveLeft.FreezeMove();
        Gun[] guns = GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns) gun.SlowDownBullets();
    }

    public void UnfreezeEnemy()
    {
        isFrozen = false;
        frozenTimer = 2f;
        MoveLeft moveLeft = GetComponent<MoveLeft>();
        if (moveLeft != null) moveLeft.UnfreezeMove();
        Gun[] guns = GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns) gun.ResumeBullets();
    }

    private void OnDestroy()
    {
        LevelController.instance.RemoveEnemy();
    }
}
