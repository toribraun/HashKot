using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HashKot : Unit
{
    [SerializeField]
    private float speed = 40F;
    [SerializeField]
    private float jumpforce = 75F;
    [SerializeField]
    public int pointsSum;
    [SerializeField]
    private Text pointsSumText;

    private bool isGroundNear;
    private bool doubleJumped = false;
    private bool turnedRight = true;

    private Rigidbody2D rigitbody;
    private Animator animator;
    private Vector2 standingPoint;
    
    private HashKotState State
    {
        get { return (HashKotState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pointsSum = 0;
        pointsSumText.text = "0";
    }

    private void FixedUpdate()
    {
        GetStandingPoint();
        CheckGround();
        if (isGroundNear)
        {
            doubleJumped = false;
        }
        else SetJumpState();
    }

    private void Update()
    {
        if (isGroundNear)
            Idle();
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGroundNear)
            {
                Jump();
            }
            else if (!doubleJumped)
            {
                doubleJumped = true;
                Jump();
            }
        }
    }

    public void Idle()
    {
        if (turnedRight)
            State = HashKotState.IdleRight;
        else State = HashKotState.IdleLeft;
    }

    public void Run()
    {
        if (Input.GetAxis("Horizontal") >= 0)
        {
            turnedRight = true;
            State = HashKotState.RunRight;
        }
        else
        {
            turnedRight = false;
            State = HashKotState.RunLeft;
        }
        if (!isGroundNear)
            SetJumpState();
        Move(Input.GetAxis("Horizontal"), speed);
    }

    public void SetJumpState()
    {
        if (turnedRight)
            State = HashKotState.JumpRight;
        else State = HashKotState.JumpLeft;
    }

    public void Jump()
    {
        rigitbody.velocity = Vector3.zero;
        rigitbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    private void GetStandingPoint()
    {
        standingPoint.x = transform.position.x;
        standingPoint.y = transform.position.y - 7.35F;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(standingPoint, 0.5F);
        isGroundNear = colliders.Length > 1;
    }
    
    public void UpdatePoints(int points)
    {
        pointsSum += points;
        pointsSumText.text = pointsSum.ToString();
    }
    
    public void GetDamage(Vector3 pythonPosition, int damage)
    {
        if (damage <= pointsSum)
        {
            var axisDirection = pythonPosition.x < transform.position.x ? 1 : -1;
            rigitbody.velocity = Vector3.zero;
            rigitbody.AddForce((transform.up + axisDirection * transform.right) * 40, ForceMode2D.Impulse);
            UpdatePoints(-damage);
        }
        else
            Die();
    }

    public override void Die()
    {
        SceneManager.LoadScene("MenuLose");
    }
}