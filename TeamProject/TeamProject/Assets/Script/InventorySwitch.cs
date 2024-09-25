using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySwitch : MonoBehaviour
{
    [SerializeField] private GameObject Fan;
    [SerializeField] private GameObject Laser;
    [SerializeField] private GameObject Trace;

    [SerializeField] private Image FanImage;  // UI Image for Fan
    [SerializeField] private Image LaserImage;  // UI Image for Laser
    [SerializeField] private Image TraceImage;  // UI Image for Trace


    public static int itemdropswitch = 0;

    // Start is called before the first frame update
    void Start()
    {

        FanImage.enabled = false;
        LaserImage.enabled = false;
        TraceImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (itemdropswitch)
        {
            case 1:
                FanImage.enabled=true;
                LaserImage.enabled = false;
                TraceImage.enabled = false;
                break;

            case 2:
                FanImage.enabled = false;
                LaserImage.enabled = true;
                TraceImage.enabled = false;
                break;

            case 3:
                FanImage.enabled = false;
                LaserImage.enabled = false;
                TraceImage.enabled = true;
                break;
        }
    }

   
}
