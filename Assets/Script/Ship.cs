using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 20f; 
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private GameObject interactPopup;
    [SerializeField] private Transform playerPlaceholder;
    private BoxCollider2D collider2D;
    private bool isPlayerOnShip = false;
    private bool isPlayerNearShip = false;

    void Start()
    {
        collider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(isPlayerOnShip)
        {
            ShipMovement();
            interactPopup.SetActive(false);
        }
        PlayerInteract();
    }

    private Player player = null;
    void PlayerInteract()
    {
        if(Input.GetKeyDown(KeyCode.E) && isPlayerNearShip && !isPlayerOnShip)
        {
            // Player Enter Ship
            isPlayerOnShip = true;
            player.playerMovement = false;
        }else if(Input.GetKeyDown(KeyCode.E) && isPlayerNearShip && isPlayerOnShip)
        {
            // Player Exit Ship
            isPlayerOnShip = false;
            player.playerMovement = true;
            player = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            interactPopup.SetActive(true);
            isPlayerNearShip = true;
            player = col.GetComponent<Player>();
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            interactPopup.SetActive(false);
            isPlayerNearShip = false;
        }
    }

    private Vector3 currentVelocity;
    void ShipMovement()
    {
        float inputX = 0;
        float inputY = 0;

        if(Input.GetKey(KeyCode.W)) inputY++;
        if(Input.GetKey(KeyCode.S)) inputY--;
        if(Input.GetKey(KeyCode.D)) inputX++;
        if(Input.GetKey(KeyCode.A)) inputX--;

        Vector3 targetDirection = new Vector3(inputX, inputY, 0).normalized;

        if(targetDirection.sqrMagnitude > 0)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetDirection * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        transform.position += currentVelocity * Time.deltaTime;
        player.transform.position = playerPlaceholder.position;
    }
}
