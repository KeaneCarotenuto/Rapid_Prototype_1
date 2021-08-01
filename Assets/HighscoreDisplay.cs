using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
    public TMP_Text m_Text;
    public Leaderboard m_Data;
    // Start is called before the first frame update
    void Start()
    {
        string s = "HIGHSCORE:\n";
        for (var i = 0; i < 4 && i < m_Data.m_Highscores.Count; i++)
        {
            s += (i + 1).ToString() + ": " + m_Data.m_Highscores[i].ToString() + "\n";
        }
        m_Text.SetText(s);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
