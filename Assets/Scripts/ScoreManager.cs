using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int m_Score;
    public TMP_Text m_ScoreText;
    public void AddScore(int _score)
    {
        m_Score += _score;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreText.text = "Score: " + m_Score.ToString();
    }
}
