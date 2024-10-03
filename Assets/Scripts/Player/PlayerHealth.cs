using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Updated to use a PlayerHealthSO as a middle man

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    [SerializeField] private PlayerHealthSO playerhealthSO;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;

    int dieAnimationHash = Animator.StringToHash("Die");


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        playerhealthSO.CurrentHealth = playerhealthSO.startingHealth;
    }


    public void TakeDamage (int amount)
    {

        playerhealthSO.CurrentHealth -= amount;

        playerAudio.Play ();

        if(playerhealthSO.CurrentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger (dieAnimationHash);

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
