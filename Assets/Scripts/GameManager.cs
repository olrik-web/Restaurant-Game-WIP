using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentDay { get; private set; }
    public float TimeRemaining { get; private set; }
    public int Money { get; private set; }
    public int[] IngredientStock { get; private set; }
    public int PowerUpCount { get; private set; }
    private List<GameObject> seatsList = new List<GameObject>();


    private void Awake()
    {
        // Singleton pattern to ensure only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Perform any necessary game initialization tasks
        InitializeGame();
        InitializeSeating();
    }

    private void InitializeGame()
    {
        // Set initial values for gameplay variables
        CurrentDay = 1;
        TimeRemaining = 120f; // Example time limit of 2 minutes
        // Set initial values for resources
        Money = 100;
        IngredientStock = new int[3]; // 3 types of ingredients
        PowerUpCount = 0;
    }

    public void InitializeSeating()
    {
        // Get all seat objects in the scene
        GameObject[] seats = GameObject.FindGameObjectsWithTag("Seat");
        foreach (GameObject seat in seats)
        {
            // Add the seat to the list of seats
            seatsList.Add(seat);
        }
    }


    // Example methods to manage resources
    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public bool DeductMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            return true; // Successful deduction
        }
        else
        {
            return false; // Insufficient funds
        }
    }

    public void AddIngredient(int ingredientIndex, int amount)
    {
        IngredientStock[ingredientIndex] += amount;
    }

    public bool DeductIngredient(int ingredientIndex, int amount)
    {
        if (IngredientStock[ingredientIndex] >= amount)
        {
            IngredientStock[ingredientIndex] -= amount;
            return true; // Successful deduction
        }
        else
        {
            return false; // Insufficient ingredients
        }
    }

    public void AddPowerUp(int amount)
    {
        PowerUpCount += amount;
    }

    public void UsePowerUp()
    {
        if (PowerUpCount > 0)
        {
            // Apply power-up effect
            PowerUpCount--;
        }
    }

    public void DecreaseTime(float amount)
    {
        TimeRemaining -= amount;
        if (TimeRemaining <= 0f)
        {
            // Game over logic when time runs out
            GameOver();
        }
    }

    public void GameOver()
    {
        // Game over logic
        Debug.Log("Game Over!");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public GameObject GetAvailableSeat()
    {
        Debug.Log("GetAvailableSeats");
        Debug.Log(seatsList.Count);
        foreach (GameObject seat in seatsList)
        {
            Seat seatScript = seat.GetComponent<Seat>();
            if (!seatScript.IsOccupied)
            {
                return seat;
            }
        }
        return null;
    }

    public void OccupySeat(Vector3 position)
    {
        foreach (GameObject seat in seatsList)
        {
            if (seat.transform.position == position)
            {
                seat.GetComponent<Seat>().IsOccupied = true;
                break;
            }
        }
    }
}
