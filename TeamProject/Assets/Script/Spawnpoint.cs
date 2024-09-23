using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform room1SpawnPoint;  // Assign this to your Room 1 Spawn Point in the inspector
    private GameObject player;

    void Start()
    {
        // Find the player object (assuming it is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && room1SpawnPoint != null)
        {
            // Move the player to the Room 1 spawn point at the start of the scene
            player.transform.position = room1SpawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Player or Room 1 spawn point is not assigned/found!");
        }
    }
}
