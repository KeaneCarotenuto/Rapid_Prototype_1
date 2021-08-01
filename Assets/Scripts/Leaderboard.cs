using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Leaderboard", menuName = "~/Documents/Repos/Rapid_Prototype_1/Assets/Scripts/Leaderboard.cs/Leaderboard", order = 0)]
public class Leaderboard : ScriptableObject
{
    public List<int> m_Highscores;

    public void AddScore(int _score)
    {
        m_Highscores.Add(_score);
        m_Highscores.Sort();
        m_Highscores.Reverse();
    }
}
