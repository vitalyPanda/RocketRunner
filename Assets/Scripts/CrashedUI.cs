using UnityEngine;
using UnityEngine.UI;

public class CrashedUI : MonoBehaviour
{

    public Text Score;
  

    public void SetUI(string score)
    {
        gameObject.SetActive(true);
        Score.text = score;
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        GameBiheviour.singleton.Restart();
    }
    public void Exit()
    {
        Application.Quit();
    }
}