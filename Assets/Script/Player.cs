using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float swimmingSlowMultiplier = 0.4f;
    public bool isPlayerWalking = true;
    public bool isPlayerSwimming = false;
    private bool isPlayerMoving = false;

    [Header("References")]
    public Animator anim;
    public Transform transform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public BoxCollider2D col;
    
    Vector3 targetDirection;
    void Update()
    {
        Animate();
    }

    void FixedUpdate()
    {
        if(isPlayerWalking == true)
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
        anim.SetBool("isSwimming", isPlayerSwimming);
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

        float swimmingSlowDebuff = (isPlayerSwimming == true) ? swimmingSlowMultiplier : 1f; 
        if(isPlayerMoving) rb.linearVelocity = targetDirection * moveSpeed * swimmingSlowDebuff;
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Water"))
        {
            isPlayerSwimming = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Water"))
        {
            isPlayerSwimming = false;
        }
    }
}
