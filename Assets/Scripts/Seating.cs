using UnityEngine;

public class Seating : MonoBehaviour
{
    public Transform[] seats; // Array of seat transforms
    public bool[] isSeatOccupied; // Array indicating if a seat is occupied or not

    private void Start()
    {
        InitializeSeats();
    }

    private void InitializeSeats()
    {
        int seatCount = seats.Length;
        isSeatOccupied = new bool[seatCount];

        // Initialize all seats as unoccupied
        for (int i = 0; i < seatCount; i++)
        {
            isSeatOccupied[i] = false;
        }
    }

    public bool IsSeatAvailable()
    {
        // Check if there is an available seat
        for (int i = 0; i < seats.Length; i++)
        {
            if (!isSeatOccupied[i])
            {
                return true;
            }
        }
        return false;
    }

    public Transform GetRandomAvailableSeat()
    {
        // Get a random available seat
        for (int i = 0; i < seats.Length; i++)
        {
            int randomIndex = Random.Range(0, seats.Length);
            if (!isSeatOccupied[randomIndex])
            {
                return seats[randomIndex];
            }
        }
        return null;
    }

    public void OccupySeat(Transform seat)
    {
        // Occupy the specified seat
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i] == seat)
            {
                isSeatOccupied[i] = true;
                break;
            }
        }
    }

    public void VacateSeat(Transform seat)
    {
        // Vacate the specified seat
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i] == seat)
            {
                isSeatOccupied[i] = false;
                break;
            }
        }
    }
}
