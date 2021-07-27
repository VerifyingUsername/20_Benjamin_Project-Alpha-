using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent Mob;

    public GameObject Player;

    public float MobDistanceRun = 4.0f;

    public Rigidbody EnemyRb;
    public Animator EnemyAnimator;
    public GameObject GoldBarSpawn;
    public GameObject GoldBarPrefab;

    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        EnemyRb = GetComponent<Rigidbody>();
        EnemyAnimator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");

        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < MobDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            Mob.SetDestination(newPos);

            EnemyAnimator.SetBool("SpiderRun", true);
        }

        else if (distance > MobDistanceRun)
        {
            EnemyAnimator.SetBool("SpiderRun", false);
        }       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && isDead == false)
        {         
            isDead = true;
            EnemyAnimator.SetTrigger("isDead");
           
            Destroy(gameObject, 1.0f);          
            Destroy(collision.gameObject);
            Instantiate(GoldBarPrefab, GoldBarSpawn.transform.position, transform.rotation);
        }

        if (collision.gameObject.tag == "Player")
        {
            EnemyAnimator.SetTrigger("isAttack");
        }
    }

}
