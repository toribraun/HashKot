using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Python : MonoBehaviour
{
    [SerializeField]
    private float speed = 10F;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<HashKot>();

        if (player)
        {
            if (Math.Abs(player.transform.position.x - transform.position.x) > 1)
            {
                player.Jump();
                Destroy(gameObject);
            } 
            else
                player.GetDamage(3);
        }
            
    }
}
