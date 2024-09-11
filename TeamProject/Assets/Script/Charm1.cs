using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charm1 : MonoBehaviour
{

     private float expandSpeed;
     private float MaxScale;
    // Start is called before the first frame update
    void Start()
    {
        expandSpeed = 5f;
        MaxScale = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        if (transform.localScale.x >= MaxScale)
            Destroy(gameObject);
    }

   


}
