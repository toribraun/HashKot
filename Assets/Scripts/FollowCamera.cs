using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] 
    private float speed = 7F;
    [SerializeField] 
    private Transform player;

    void Awake()
    {
        if (!player)
            player = FindObjectOfType<HashKot>().transform;

    }

    void Update ()
    {
        var position = player.position;
        position.z = -10F;
        
        if (player.position.y < 5)
            position.y = 0;
        position.y *= 0.9F;
            
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
