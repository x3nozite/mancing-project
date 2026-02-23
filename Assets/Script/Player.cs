using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Transform transform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f;
    public bool playerMovement = true; 
    
    private bool isPlayerMoving = false;
    Vector3 targetDirection;
    void Update()
    {
        Animate();
    }

    void FixedUpdate()
    {
        if(playerMovement == true)
        {
            HandleMovement();
        }
    }

    void Animate()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f; 

        Vector3 mouseWorldPos = UnityEngine.Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector3 lookDirection = (mouseWorldPos - transform.position).normalized;

        anim.SetFloat("mouseX", lookDirection.x);
        anim.SetFloat("mouseY", lookDirection.y);
        anim.SetBool("isMoving", isPlayerMoving);
        anim.SetFloat("moveX", targetDirection.x);
        anim.SetFloat("moveY", targetDirection.y);

        if(mouseWorldPos.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }else
        {
            spriteRenderer.flipX = false;
        }
    }

    void HandleMovement()
    {
        float inputX = 0;
        float inputY = 0;

        if(Input.GetKey(KeyCode.W)) inputY++;
        if(Input.GetKey(KeyCode.S)) inputY--;
        if(Input.GetKey(KeyCode.D)) inputX++;
        if(Input.GetKey(KeyCode.A)) inputX--;

        if(inputX != 0f || inputY != 0f) isPlayerMoving = true;
        else isPlayerMoving = false;

        targetDirection = new Vector3(inputX, inputY, 0).normalized;
        if(isPlayerMoving) rb.linearVelocity = targetDirection * moveSpeed;
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
