using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image gameOver;
    public Image start;
    public Image quit;
    public Image A;
    public Image B;

    bool fade = false;

    public float fadeSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.color -= new Color(0,0,0, gameOver.color.a);
        start.color -= new Color(0, 0, 0, start.color.a);
        quit.color -= new Color(0, 0, 0, quit.color.a);
        A.color -= new Color(0, 0, 0, A.color.a);
        B.color -= new Color(0, 0, 0, B.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            if (gameOver.color.a <= 1) gameOver.color += new Color(0, 0, 0, fadeSpeed);
            if (start.color.a <= 1) start.color += new Color(0, 0, 0, fadeSpeed);
            if (quit.color.a <= 1) quit.color += new Color(0, 0, 0, fadeSpeed);
            if (A.color.a <= 1) B.color += new Color(0, 0, 0, fadeSpeed);
            if (B.color.a <= 1) A.color += new Color(0, 0, 0, fadeSpeed);
        }
    }

    public void StartFade()
    {
        fade = true;
    }
}
