using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    public Text scoreText;

    public void SetUI(string score)
    {
        if (!gameObject.activeInHierarchy) gameObject.SetActive(true);
        scoreText.text = "Score : " + score;
    }
}
