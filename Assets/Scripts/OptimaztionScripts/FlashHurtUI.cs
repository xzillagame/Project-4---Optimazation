using UnityEngine;
using UnityEngine.UI;

public class FlashHurtUI : MonoBehaviour
{

    [SerializeField] private PlayerHealthSO playerHealthSO;
    [SerializeField] private Image damageImage;
    [SerializeField] private float flashSpeed = 5f;
    [SerializeField] Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private bool damaged = false;

    private void OnEnable()
    {
        playerHealthSO.OnPlayerTakeDamage += TriggerDamaged;
    }

    private void OnDisable()
    {
        playerHealthSO.OnPlayerTakeDamage -= TriggerDamaged;
    }

    private void TriggerDamaged(int currentHealth) => damaged = true;

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


}
