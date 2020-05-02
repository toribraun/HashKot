using System;
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
        direction = 1;
    }

    private void Update()
    {
        CheckDirection();
        Move(direction, speed);
    }

    private void CheckDirection()
    {
        var colliderNear = Physics2D.OverlapPoint(transform.position + transform.up * 0.5F + transform.right * (4.5F * direction));

        if (colliderNear && !colliderNear.GetComponent<Unit>())
        {
            direction *= -1;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<HashKot>();

        if (player)
        {
            if (Math.Abs(player.transform.position.y - transform.position.y) > 10)
            {
                player.Jump();
                Die();
            }
            else
                player.GetDamage(transform.position, 3);
        }
    }
}
