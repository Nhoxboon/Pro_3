using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isInStartMenu = false; 

    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public Vector2 moveDirection;
    
    private void Update()
    {
        if (isInStartMenu)
        {
            moveDirection = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }
}
