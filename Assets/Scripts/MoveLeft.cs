using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed = 2;
    float originalMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreezeMove()
    {
        moveSpeed = 0;
    }

    public void UnfreezeMove()
    {
        moveSpeed = originalMoveSpeed;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return;
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.fixedDeltaTime;
        transform.position = pos;
        if (pos.x < -11) Destroy(gameObject);
    }
}
