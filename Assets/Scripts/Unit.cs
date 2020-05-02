using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void Move(float direction, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            transform.position + transform.right * direction, 
            speed * Time.deltaTime);
    }
}
