using System.Collections;
using UnityEngine;

// Added object pool reference


public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private EnemyStatsSO enemyStats;

    public int currentHealth;
    [HideInInspector] public EnemyManager enemyManagerPool;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = enemyStats.startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * enemyStats.sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = enemyStats.deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += enemyStats.scoreValue;
        
        StartCoroutine(AddToQueueAfterSinkRoutine());

    }


    private IEnumerator AddToQueueAfterSinkRoutine()
    {
        yield return new WaitForSeconds(2f);

        enemyManagerPool.ReAddToQueue(this.gameObject);
    }

}
