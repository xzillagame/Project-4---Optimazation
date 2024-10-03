
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "SO/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public int startingHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
}
