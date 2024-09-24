using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    [SerializeField] public Transform firePoint1;
    [SerializeField] public Transform firePoint2;
    [SerializeField] public Transform firePoint3;
    [SerializeField] public Transform firePoint4;
    [SerializeField] public Transform firePoint5;
    
    [Range(0.0001f, 1f)]
    [SerializeField] public float FireRate;

    [Range(0,300)]
    [SerializeField] public float weaponTimer;
    public float setTimer;



    private float fireTimer;


    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject LazerPrefab;
    [SerializeField] public GameObject TrackingPrefab;
    [SerializeField] public GameObject FanPrefab;
    [SerializeField] public GameObject CharmPrefab;


    private Animator anim;
    private playerMovement pMove;
    public int WeaponType;


    //public AudioClip shootingSound;
    //private AudioSource audioSource;


    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

            anim = GetComponent<Animator>();
        pMove = GetComponent<playerMovement>();
        WeaponType = 0;
        //audioSource = GetComponent<AudioSource>();
        //if (audioSource == null)
        //{
        //    audioSource = gameObject.AddComponent<AudioSource>();  // Adds an AudioSource if not already present
        //}
        //audioSource.clip = shootingSound;
        //audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsendingScene())
            gameObject.SetActive(false);

        if (WeaponType != 0)
            WeaponTimer();


        if (charmmanager.charmCount < 0)
            charmmanager.charmCount = 0;

        else if (charmmanager.charmCount > 2)
            charmmanager.charmCount = 2;

        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            anim.SetFloat("Attack", 1);
            fireTimer = FireRate;
            switch (WeaponType)
            {
                case 1:
                    LazerShoot();
                    
                    FireRate = 0.5f;
                    break;
                case 2:
                    TrackingShoot();
                    
                    FireRate = 0.2f;
                    break;
                case 3:
                    FanShoot();
                    
                    FireRate = 0.01f;
                    break;
                default:
                    BulletShoot();
                    
                    FireRate = 0.01f;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && charmmanager.charmCount > 0)
        {
            charmmanager.charmCount--;
            CharmShoot();
            

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetFloat("Attack", -1);
        }


        else
        {
            fireTimer -= Time.deltaTime;
        }
    }


    private bool IsendingScene()
    {
        return SceneManager.GetActiveScene().name == "Ending" || SceneManager.GetActiveScene().buildIndex == 6;
    }
    void BulletShoot()
    {

        Instantiate(bulletPrefab, firePoint3.position, Quaternion.identity);
        Music.instance.PlaySE("Attack");
    }

    void LazerShoot()
     {
        Instantiate(LazerPrefab,firePoint3.position, Quaternion.identity);
        Music.instance.PlaySE("Attack");
    }

    void TrackingShoot()
    {
        Instantiate(TrackingPrefab,firePoint3.position, Quaternion.identity);
        Music.instance.PlaySE("Attack");

    }
    void FanShoot()
    {
        Instantiate(FanPrefab,firePoint1.position, firePoint1.rotation);
        Instantiate(FanPrefab,firePoint2.position, firePoint2.rotation);
        Instantiate(FanPrefab,firePoint3.position, firePoint3.rotation);
        Instantiate(FanPrefab,firePoint4.position, firePoint4.rotation);
        Instantiate(FanPrefab,firePoint5.position, firePoint5.rotation);
        Music.instance.PlaySE("Attack");
    }

    void CharmShoot()
    {
        Instantiate(CharmPrefab,transform.position,Quaternion.identity);
        Music.instance.PlaySE("Attack");
    }

    public void WeaponTimer()
    {
        if (weaponTimer > 0)
        {
        weaponTimer -= Time.deltaTime;
        }
        else if (weaponTimer < 0) 
        {
            weaponTimer = 0;
            WeaponType = 0;
        }
    }

    //private void PlayShootingSound()
    //{

    //    if (audioSource != null && shootingSound != null ) 
    //    {
    //        audioSource.PlayOneShot(shootingSound);  // Plays the shooting sound once
          
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Shooting sound not assigned or AudioSource is missing!");
    //    }
    //}
    public void ResetWeaponTimer()
    {
        weaponTimer = setTimer;

    }
}