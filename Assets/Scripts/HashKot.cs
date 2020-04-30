using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HashKot : Unit
{
    [SerializeField]
    private float speed = 15F;
    [SerializeField]
    private float jumpforce = 75F;
    [SerializeField]
    private int pointsSum;

    [SerializeField] 
    public Text pointsSumText; 

    private bool isGroundNear;

    private Rigidbody2D rigitbody;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        pointsSumText.text = "0";
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        
    }

    private void Run()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            transform.position + transform.right * Input.GetAxis("Horizontal"), 
            speed * Time.deltaTime);
    }

    private void Jump()
    {
        rigitbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGroundNear = colliders.Length > 1;
    }
    
    public void GetPoints(int points)
    {
        pointsSum += points;
        pointsSumText.text = pointsSum.ToString();
    }
}