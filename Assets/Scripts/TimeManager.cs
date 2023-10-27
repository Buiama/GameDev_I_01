using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTime()
    {
        StartCoroutine(StopTimeRoutine());
    }

    private IEnumerator StopTimeRoutine()
    {
        Gun[] allGuns = FindObjectsOfType<Gun>();
        Bullet[] allBullets = FindObjectsOfType<Bullet>();
        MoveLeft[] allMovingObjects = FindObjectsOfType<MoveLeft>();

        foreach (Gun gun in allGuns)
        {
            gun.isActive = false;
        }
        foreach (Bullet bullet in allBullets)
        {
            if (bullet.isEnemy) bullet.enabled = false;
        }

        foreach (MoveLeft movingObject in allMovingObjects)
        {
            movingObject.enabled = false;
        }

        yield return new WaitForSeconds(5);
        allGuns = FindObjectsOfType<Gun>();
        allBullets = FindObjectsOfType<Bullet>();
        allMovingObjects = FindObjectsOfType<MoveLeft>();

        foreach (Gun gun in allGuns)
        {
            gun.isActive = true;
        }
        foreach (Bullet bullet in allBullets)
        {
            if(bullet.isEnemy) bullet.enabled = true;
        }

        foreach (MoveLeft movingObject in allMovingObjects)
        {
            movingObject.enabled = true;
        }
    }
}
