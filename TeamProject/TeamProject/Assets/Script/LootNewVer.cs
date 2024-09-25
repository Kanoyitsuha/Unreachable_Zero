using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootNewVer : MonoBehaviour
{
    private Rigidbody2D rb;
    private int Dropped = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * Dropped, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
