using UnityEngine;

public class PlayerMovement : NhoxBehaviour
{
    public bool isInStartMenu = true; 

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

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody2D();
    }

    protected void LoadRigidbody2D()
    {
        if(rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2D", gameObject);
    }
}
