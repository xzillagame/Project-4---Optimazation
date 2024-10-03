using UnityEngine;
using UnityEngine.Events;

//Updated to have a static property and UnityAction for when the score is changed

public class ScoreManager : MonoBehaviour
{
    private static int _score;
    public static int score
    {
        get { return _score; }
        set 
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public static event UnityAction<int> OnScoreChanged;

    void Awake ()
    {
        score = 0;
    }
}
