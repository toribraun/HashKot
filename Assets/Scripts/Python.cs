using System;
using UnityEngine;
using Random = System.Random;

public class Python : Unit
{
    [SerializeField]
    private float speed = 10F;

    private SpriteRenderer sprite;
    private int direction;
    private Random random = new Random();
    private AudioSource audio;
    private int stolenPoints;
    
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        direction = -1;
        stolenPoints = 0;
    }

    private void Update()
    {
        CheckDirection();
        Move(direction, speed);
    }

    private void CheckDirection()
    {
        var colliderNear = Physics2D.OverlapPoint(
            transform.position + transform.right * (4.5F * direction));

        if (colliderNear && !colliderNear.GetComponent<Unit>())
        {
            direction *= -1;
            sprite.flipX = direction > 0.0;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<HashKot>();

        if (player)
        {
            if (Math.Abs(player.transform.position.y - transform.position.y) > Math.Abs(player.transform.position.x - transform.position.x))
            {
                player.Jump();
                player.GetComponent<AudioSource>().Play();
                if (stolenPoints > 0)
                    player.pythonsToRevenge--;
                player.UpdatePoints(stolenPoints / 2);
                Die();
            }
            else
            {
                if (stolenPoints == 0)
                    player.pythonsToRevenge++;
                player.GetDamage(transform.position, GetDamageLevelRandomly());
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private int GetDamageLevelRandomly()
    {
        var maxRange = 10;
        var minRange = 3;
        var damage = random.Next(minRange, maxRange);
        stolenPoints += damage;
        return damage;
    }
}