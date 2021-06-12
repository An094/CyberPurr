using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text MyScore;
    private int scoreNum;

    #region singleton
    public static Score Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        scoreNum = 0;
        MyScore.text = scoreNum.ToString();
    }

    public void IncreaseScore()
    {
        scoreNum += 1;
        MyScore.text = scoreNum.ToString();
    }
}
