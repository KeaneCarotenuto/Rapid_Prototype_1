using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public UnityEvent OnTimerElapse;
    public float m_TimerDuration;
    public float m_TimeSurvived = 0;
    public int m_RoomsExplored = 0;

    public int m_Score = 0;

    public TMP_Text m_Text;
    public Leaderboard m_leaderboard;
    float m_Timer;

    bool keepCounting = true;

    public void AddPoints(int _points)
    {
        m_Score += _points;
    }

    public void SaveScore()
    {
        keepCounting = false;
        m_leaderboard.AddScore(m_Score);
    }
    // Start is called before the first frame update
    void Start()
    {
        keepCounting = true;
        m_Timer = m_TimerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (keepCounting)
        {
            m_TimeSurvived += Time.deltaTime;
            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                m_Timer = m_TimerDuration;
                OnTimerElapse.Invoke();
            }
            m_Text.SetText("Score: " + m_Score.ToString());
        }
        

    }
}
