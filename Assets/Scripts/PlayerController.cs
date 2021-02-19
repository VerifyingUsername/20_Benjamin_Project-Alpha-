using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    //Player health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    public Rigidbody rb;
    public Animator animator;
    public GameObject bulletText;

    private int bulletCount = 10;
    private bool NoBullets = false;

    public AudioClip[] AudioClipArr;
    private AudioSource audiosource;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        bulletText.GetComponent<Text>().text = "Bullets: " + bulletCount;

        audiosource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
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

        //Reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("ReloadTrigger");
            NoBullets = false;
            
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            audiosource.PlayOneShot(AudioClipArr[1], 0.2f);
            bulletCount = 10;
            bulletText.GetComponent<Text>().text = "Bullets: " + bulletCount;
        }

        //Test damage
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentHealth -= 20;

            healthBar.SetHealth(currentHealth);
        }

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }


        if (NoBullets == false)
        {
            //shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("ShootTrigger");
                Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
                audiosource.PlayOneShot(AudioClipArr[0], 0.2f);
                bulletCount -= 1;
                bulletText.GetComponent<Text>().text = "Bullets: " + bulletCount;
                
                if (bulletCount == 0)
                {
                    NoBullets = true;
                }
            }
        }
        
        
    }
}
