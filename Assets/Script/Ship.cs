using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensures the GameObject has a Rigidbody2D
public class Ship : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 20f; 
    [SerializeField] private float deceleration = 15f; // Increased for better feel

    [Header("References")]
    [SerializeField] private GameObject interactPopup;
    [SerializeField] private Transform playerPlaceholder;

    private Rigidbody2D rb;
    private bool isPlayerOnShip = false;
    private bool isPlayerNearShip = false;
    private Player player = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlayerOnShip)
        {
            interactPopup.SetActive(false);
            player.transform.position = playerPlaceholder.position;
        }
        
        PlayerInteract();
    }

    void FixedUpdate()
    {
        if (isPlayerOnShip)
        {
            MoveShip();
        }
        else
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    private void MoveShip()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 targetDirection = new Vector2(inputX, inputY).normalized;
        Vector2 targetVelocity = targetDirection * maxSpeed;

        if (targetDirection.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    void PlayerInteract()
    {
        if (Input.GetKeyDown(KeyCode.E) && (isPlayerNearShip || isPlayerOnShip))
        {
            if (isPlayerOnShip == false)
            {
                isPlayerOnShip = true; 
                player.isPlayerWalking = false;
                player.col.enabled = false;
                player.transform.SetParent(gameObject.transform);
            }
            else
            {
                player.isPlayerWalking = true;
                isPlayerOnShip = false;
                player.col.enabled = true;
                player.transform.SetParent(null);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactPopup.SetActive(true);
            isPlayerNearShip = true;
            player = col.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactPopup.SetActive(false);
            isPlayerNearShip = false;
            if (!isPlayerOnShip) player = null;
        }
    }
}
