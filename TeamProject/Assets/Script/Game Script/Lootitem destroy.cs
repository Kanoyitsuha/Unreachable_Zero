using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootitemdestroy : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] public float Itemlife = 8f;

    public float moveSpeed = 0.5f;  // Speed at which the item will move
    public float moveDuration = 2f;  // Duration of the random movement

    private Vector2 movementDirection;
    private float moveTimeRemaining;
    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        Itemlife = 5f;
        Destroy(gameObject,Itemlife);

        moveSpeed = 0.1f;
        // Get the main camera
        mainCamera = Camera.main;
        movementDirection = new Vector2(Random.Range(-9.88f, -10f), Random.Range(-5f, 5f));


        // Calculate object size
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

    }
    private void Update()
    {

     transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
     Debug.Log(movementDirection);
     moveTimeRemaining -= Time.deltaTime; 
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
        


    }
}
