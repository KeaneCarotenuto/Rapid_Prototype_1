using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource m_AudioStandard, m_AudioSFX;

    public AudioClip m_Idle, m_Running, m_Charge, m_Death, m_Damage;

    public bool m_isrunning = false;

    public void SetRunning(bool _isrunning)
    {
        if (_isrunning && !m_isrunning)
        {
            m_AudioStandard.clip = m_Running;
            m_isrunning = true;
        }
        else if (m_isrunning)
        {
            m_AudioStandard.clip = m_Idle;
            m_isrunning = false;
        }
        else
        {
            return;
        }
        //m_AudioStandard.Play();
    }

    public void PlaySoundEffect(string _effect)
    {
        switch (_effect)
        {
            case "CHARGE":
                m_AudioSFX.clip = m_Charge;
                break;
            case "DEATH":
                m_AudioSFX.clip = m_Death;
                break;
            case "DAMAGE":
                m_AudioSFX.clip = m_Damage;
                break;
            default:
                break;
        }
        //m_AudioSFX.Play();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
