using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashKot : MonoBehaviour
{
    [SerializeField]
    private float speed = 3F;
    [SerializeField]
    private float jumpforce = 15F;

    private bool isGround;

    private Rigidbody2D rigitbody;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump"))
            Jump();
        
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void Jump()
    {
        rigitbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGround = colliders.Length > 1;
    }
}