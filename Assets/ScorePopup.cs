using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScorePopup : MonoBehaviour
{
    public int m_Score;

    

    
    public TMP_Text m_text;

    public float m_lifespan;

    float m_timer;
    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        m_text.text = "+" + m_Score.ToString();
        if (m_timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
