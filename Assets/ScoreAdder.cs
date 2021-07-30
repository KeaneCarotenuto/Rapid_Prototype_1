using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    public GameObject m_ScorePrefab;

    public bool m_AddScoreOnTimer;
    public int m_TimerScore;

    public float m_TimerDuration;

    float m_Timer;

    ScoreManager m_Manager;

    public Flamable m_BurnScore;
    bool m_Flammable;

    public void AddScore(int _score)
    {
        ScorePopup popup = GameObject.Instantiate(m_ScorePrefab, this.transform.position, Quaternion.Euler(35, 130, 0)).GetComponent<ScorePopup>();
        popup.m_Score = _score;
        m_Manager.AddScore(_score);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Manager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        m_Timer = m_TimerDuration;
        m_Flammable = (m_BurnScore != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Flammable)
        {
            m_AddScoreOnTimer = m_BurnScore.onFire;
        }
        if (m_AddScoreOnTimer)
        {


            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                m_Timer = m_TimerDuration;
                AddScore(m_TimerScore);
            }
        }
    }
}
