using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ScoreSystem : MonoBehaviour
{
   [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private TMP_Text scoreText;
    private int score = 0;


    public void scored()
    {
        score += 1;
        scoreText.SetText(score.ToString());
    }   

    public void startGame()
    {
        GameObject ball = Instantiate(ballPrefab);
    }
}
