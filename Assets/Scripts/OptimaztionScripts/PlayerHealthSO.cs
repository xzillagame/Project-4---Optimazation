using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerHealthSO", menuName = "SO/PlayerHealth")]
public class PlayerHealthSO : ScriptableObject
{

    public event UnityAction<int> OnPlayerTakeDamage;

    public int startingHealth = 100;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set 
        {
            currentHealth = value;
            OnPlayerTakeDamage?.Invoke(currentHealth);
        }
    }

    private int currentHealth;

}
