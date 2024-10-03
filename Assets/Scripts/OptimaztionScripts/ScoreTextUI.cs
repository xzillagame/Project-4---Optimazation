using UnityEngine;
using UnityEngine.UI;

//Listern for when the Score is changed and adjusted text value

public class ScoreTextUI : MonoBehaviour
{
    [SerializeField] Text scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
