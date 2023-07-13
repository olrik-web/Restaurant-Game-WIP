using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab; // Reference to the customer prefab
    public Transform[] spawnPoints; // Array of spawn points for customers
    public float spawnInterval = 5f; // Time interval between customer spawns

    private void Start()
    {
        // Start the coroutine to spawn customers
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Instantiate a new customer at a random spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject customer = Instantiate(customerPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            // customer.GetComponent<CustomerMovement>().MoveToRandomSeat();
            
        }
    }
}
