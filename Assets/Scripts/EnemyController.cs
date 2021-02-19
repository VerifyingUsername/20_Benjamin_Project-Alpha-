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

    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        EnemyRb = GetComponent<Rigidbody>();
        EnemyAnimator = GetComponent<Animator>();
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

        if(isDead == true)
        {
            SceneManager.LoadScene("WinScene");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            EnemyAnimator.SetTrigger("isDead");
            isDead = true;
        }
    }
}
