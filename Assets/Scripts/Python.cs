using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Python : Unit
{
    [SerializeField]
    private float speed = 10F;

    private SpriteRenderer sprite;
    private int direction;
    
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        direction = 2;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var colliderNear = Physics2D.OverlapPoint(transform.position + transform.up * 0.5F + transform.right * direction);
        if (colliderNear && !colliderNear.GetComponent<Unit>() || Math.Abs(transform.position.x) > 100)
        {
            direction *= -1;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right * direction, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<HashKot>();

        if (player)
        {
            if (Math.Abs(player.transform.position.y - transform.position.y) > 10)
            {
                player.Jump();
                Destroy(gameObject);
            }
            else
                player.GetDamage(transform.position, 3);
        }
    }
}
