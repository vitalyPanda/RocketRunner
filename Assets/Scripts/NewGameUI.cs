using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameUI : MonoBehaviour
{

    public Text MaxScore;
    public void Play()
    {
        gameObject.SetActive(false);
       
        GameBiheviour.singleton.Restart();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SetUI(string score)
    {
        gameObject.SetActive(true);
        MaxScore.text = "Best score : " + score;
    }

}
