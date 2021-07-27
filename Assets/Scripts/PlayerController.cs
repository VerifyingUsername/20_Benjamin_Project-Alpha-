using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Movement speed
    public float moveSpeed;
    public float rotateSpeed;

    //Player health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;

    //Bullets
    //public int maxBullet = 10;
    public static int currentBullet = 10;
    private bool NoBullets = false;
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    //Ammo
    private bool CanReload = false;
    public GameObject AmmoPrefab;
    public GameObject AmmoSpawn;

    //Shop
    private bool inShop = false;

    //Gold
    public static int GoldAmount = 5;
    

    //Treasure Count
    //public int maxTreasure = 4;
    public float currentTreasure = 0;
    public TreasureScript treasureBar;

    //Declaration
    public Rigidbody rb;
    public Animator animator;
    public GameObject bulletText;
    public GameObject HealthText;
    //public GameObject AmmoClipText;
    public GameObject GoldText;
    //public static GameObject GoldTextUI;

    

    public GameObject TreasureText;
    public ParticleSystem muzzleFlash;

    //Audio
    public AudioClip[] AudioClipArr;
    private AudioSource audiosource;
   
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        bulletText.GetComponent<Text>().text = "Bullets: " + currentBullet;
        HealthText.GetComponent<Text>().text = "" + maxHealth;
        //AmmoClipText.GetComponent<Text>().text = currentAmmo + "/2";
        GoldText.GetComponent<Text>().text = "X " + GoldAmount;
        TreasureText.GetComponent<Text>().text = currentTreasure + "/4";
        

        audiosource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Movement Controls
        if (Input.GetKey(KeyCode.W))
        {          
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            animator.SetBool("isRun", true);
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * -rotateSpeed, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {    
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
            animator.SetBool("isRun", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * rotateSpeed, 0));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            audiosource.PlayOneShot(AudioClipArr[4], 0.2f);

            GoldAmount += 5;            
            GoldText.GetComponent<Text>().text = "X " + GoldAmount;
        }

        // Lose
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        // Lose if player falls out of world
        if (transform.position.y < -1)
        {           
            SceneManager.LoadScene("LoseScene");
        }

        // WinScene
        if (currentTreasure >= 4)
        {
            SceneManager.LoadScene("WinScene");
        }
        
        if (NoBullets == false)
        {
            //shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("ShootTrigger");
                muzzleFlash.Play();
                Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
                audiosource.PlayOneShot(AudioClipArr[0], 0.2f);
                currentBullet -= 1;
                bulletText.GetComponent<Text>().text = "Bullets: " + currentBullet;
                
                if (currentBullet == 0)
                {
                    NoBullets = true;
                }
            }
        }

        //Reloading
        if (CanReload == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetTrigger("ReloadTrigger");
                NoBullets = false;
                CanReload = false;

                audiosource.PlayOneShot(AudioClipArr[1], 0.2f);
                currentBullet = 10;
                bulletText.GetComponent<Text>().text = "Bullets: " + currentBullet;
            }
        }
        

        // Shop
        if (inShop == true)
        {
            if (GoldAmount >= 5)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    Instantiate(AmmoPrefab, AmmoSpawn.transform.position, transform.rotation);
                    audiosource.PlayOneShot(AudioClipArr[7], 0.2f);
                    GoldAmount -= 5;
                    GoldText.GetComponent<Text>().text = "X " + GoldAmount;
                }
            }
        }
        
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            inShop = true;
            Debug.Log("InShop = true");
        }
        else if (collision.gameObject.tag != "Shop")
        {
            inShop = false;
            Debug.Log("InShop = false");
        }

        if (collision.gameObject.tag == "Ammo")
        {
            CanReload = true;
            //Debug.Log("Can reload = true");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Spider")
        {
            audiosource.PlayOneShot(AudioClipArr[2], 0.2f);

            currentHealth -= 20;
            HealthText.GetComponent<Text>().text = "" + currentHealth;
            healthBar.SetHealth(currentHealth);
        }

        if (collision.gameObject.tag == "GoldBar")
        {
            audiosource.PlayOneShot(AudioClipArr[4], 0.2f);

            GoldAmount += 2;
            Destroy(collision.gameObject);
            GoldText.GetComponent<Text>().text = "X " + GoldAmount;
        }

        if (collision.gameObject.tag == "TreasureChest")
        {
            audiosource.PlayOneShot(AudioClipArr[5], 0.2f);

            currentTreasure += 1f;
            Destroy(collision.gameObject);
            TreasureText.GetComponent<Text>().text = currentTreasure + "/4";         
        }

        if (collision.gameObject.tag == "Health")
        {
            audiosource.PlayOneShot(AudioClipArr[6], 0.2f);

            Destroy(collision.gameObject);
            currentHealth = 100;
            HealthText.GetComponent<Text>().text = "" + currentHealth;
            healthBar.SetHealth(currentHealth);
        }
    }
}
