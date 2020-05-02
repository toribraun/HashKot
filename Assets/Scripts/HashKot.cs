using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HashKot : Unit
{
    [SerializeField]
    private float speed = 15F;
    [SerializeField]
    private float jumpforce = 75F;
    [SerializeField]
    public int pointsSum;
    [SerializeField] 
    private Text pointsSumText; 

    private bool isGroundNear;

    private Rigidbody2D rigitbody;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigitbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        pointsSum = 0;
        pointsSumText.text = "0";
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Move(Input.GetAxis("Horizontal"), speed);
        if (isGroundNear && Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        
    }

    public void Jump()
    {
        rigitbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        isGroundNear = Physics2D.OverlapCircle(transform.position - transform.up * 17, 9);
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