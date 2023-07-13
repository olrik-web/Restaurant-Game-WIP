using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the customer moves towards the seat
    private Seating seating; // Reference to the Seating script
    private Transform targetSeat; // Transform of the target seat where the customer should move
    private bool isMoving = false; // Indicates if the customer is currently moving
    private Vector3 targetPosition; // Position of the target seat

    private void Start()
    {
        seating = FindObjectOfType<Seating>(); // Find the Seating script in the scene
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the customer has reached the target position
            if (transform.position == targetPosition)
            {
                // Customer has reached the seat, perform seat-related actions
                OnReachedSeat();
            }
        }
    }

    private void OnReachedSeat()
    {
        // Perform any actions you want when the customer reaches the seat
        // For example, change customer state to "seated" or disable movement
        seating.OccupySeat(targetSeat); // Mark the seat as occupied in the Seating script
    }

    public void MoveToRandomSeat()
    {
        if (!isMoving && seating.IsSeatAvailable())
        {
            Transform randomSeat = seating.GetRandomAvailableSeat(); // Get a random available seat from the Seating script

            if (randomSeat != null)
            {
                targetSeat = randomSeat; // Set the target seat transform
                targetPosition = randomSeat.position; // Set the target position to the seat's position
                isMoving = true; // Start the movement towards the seat
            }
        }
    }
}
