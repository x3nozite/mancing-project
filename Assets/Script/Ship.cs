using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 20f; 
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private GameObject interactPopup;
    private BoxCollider2D collider2D;
    public bool isPlayerOnShip = false;

    void Start()
    {
        collider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(isPlayerOnShip) ShipMovement(); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            interactPopup.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            interactPopup.SetActive(false);
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

    }
}
