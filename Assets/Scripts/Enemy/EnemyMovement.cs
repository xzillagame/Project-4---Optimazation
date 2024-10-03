using UnityEngine;
using UnityEngine.AI;


//Edited to remove FindObjectOfType and GetComponent calls in Update function


public class EnemyMovement : MonoBehaviour
{

    private Transform playerTransform;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;

    private NavMeshAgent enemyNavAgent;


    private void OnEnable()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        enemyNavAgent = GetComponent<NavMeshAgent>();
    }


    void Update ()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            enemyNavAgent.SetDestination (playerTransform.position);
        }
        else
        {
            enemyNavAgent.enabled = false;
        }
    }
}
