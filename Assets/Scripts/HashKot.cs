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
    private int totalCollectedPoint = 0;

    private bool isGroundNear;
    private bool doubleJumped = false;
    private bool turnedRight = true;

    private Rigidbody2D rigitbody;
    private Animator animator;
    private Collider2D collider;
    private Vector2 standingPoint;
    private Vector2 boxSize = new Vector2 { x = 7.39F, y = 0.1F };

    private HashKotState State
    {
        get { return (HashKotState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
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
        else SetFallState();
    }

    private void Update()
    {
        if (isGroundNear)
            Idle();
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckAndJump();
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
            SetFallState();
        Move(Input.GetAxis("Horizontal"), speed);
    }

    public void SetFallState()
    {
        if (turnedRight)
        {
            if (rigitbody.velocity.y >= jumpforce / 2)
                State = HashKotState.JumpRight;
            else if (rigitbody.velocity.y < -0.5)
                State = HashKotState.FallRight;
            else if (Math.Abs((int)State) < 3)
                State = HashKotState.FallRight;
        }
        else
        {
            if (rigitbody.velocity.y >= jumpforce / 2)
                State = HashKotState.JumpLeft;
            else if (rigitbody.velocity.y < -0.5)
                State = HashKotState.FallLeft;
            else if (Math.Abs((int)State) < 3)
                State = HashKotState.FallLeft;
        }
    }
    public void Jump()
    {
        if (turnedRight)
            State = HashKotState.JumpRight;
        else State = HashKotState.JumpLeft;
        rigitbody.velocity = Vector3.zero;
        rigitbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    private void CheckAndJump()
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

    private void GetStandingPoint()
    {
        standingPoint.x = collider.bounds.center.x;
        standingPoint.y = transform.position.y - 7.35F;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(standingPoint, boxSize, 0);
        isGroundNear = colliders.Length > 1;
    }

    public void UpdatePoints(int points)
    {
        pointsSum += points;
        pointsSumText.text = pointsSum.ToString();
        if (points > 0)
        {
            totalCollectedPoint += points;
            if (totalCollectedPoint > 99)
                EndGame();
        }
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

    public void EndGame()
    {
        if (pointsSum >= 40)
        {
            GameStates.CurrentPointSum = pointsSum;
            SceneManager.LoadScene("MenuWin");
        }
        else
            SceneManager.LoadScene("MenuLose");
    }
}