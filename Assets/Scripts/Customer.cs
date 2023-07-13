using UnityEngine;
using Pathfinding;

public class Customer : MonoBehaviour
{
    public float patience = 10f; // How long the customer is willing to wait
    public float patienceDecreaseRate = 1f; // Rate at which patience decreases per second

    private bool isSeated = false; // Indicates if the customer has been seated
    private bool isServed = false; // Indicates if the customer has been served
    private float currentPatience; // Current patience level
    

    private void Start()
    {
        currentPatience = patience; // Set the initial patience level
    }

    private void Update()
    {
        if (!isSeated)
        {
            // Find an unoccupied seat and move to it
            GameObject seat = GameManager.Instance.GetAvailableSeat();
            if (seat != null)
            {
                // Move to the seat
                gameObject.GetComponent<Pathfinding.AIDestinationSetter>().target = seat.transform;

                // Occupy the seat
                isSeated = true;
                GameManager.Instance.OccupySeat(seat.transform.position);
            }
        }
        if (!isServed && isSeated)
        {
            // Decrease patience over time
            currentPatience -= patienceDecreaseRate * Time.deltaTime;

            if (currentPatience <= 0f)
            {
                // Customer runs out of patience, handle game over or penalty logic
                GameManager.Instance.GameOver();
            }
        }
    }

    public void ServeCustomer()
    {
        if (!isServed)
        {
            // Handle customer served logic
            isServed = true;

            // Increase player score or other relevant actions
            GameManager.Instance.AddMoney(10);

            // Optionally, trigger customer satisfaction animation or other visual/audio effects
        }
    }
}
