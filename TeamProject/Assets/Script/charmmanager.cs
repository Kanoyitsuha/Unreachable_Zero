using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charmmanager : MonoBehaviour
{
    [SerializeField] private Image charm;  // UI Image for charm
    [SerializeField] private Image charm1;                                       // Add more skill images as needed
    public static int charmCount = 2;
    void Start()
    {
        charm.enabled = true;
        charm1.enabled = true;

    }
    
    void Update()
        {
        
         switch (charmCount)
         {
            case 0:
             //charm.enabled = true;
                //charm1.enabled = true;
                charm.enabled = false;
                charm1.enabled = false;
                break;

            case 1:
                charm.enabled = true;
                charm1.enabled = false;

                break;

            case 2:
                //charm.enabled = false;
                 //charm1.enabled = false;
                 charm.enabled = true;
                 charm1.enabled = true;

                break;


    }
}
}
