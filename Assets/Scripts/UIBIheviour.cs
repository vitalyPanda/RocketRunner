using UnityEngine;

public class UIBIheviour : MonoBehaviour
{
    public static UIBIheviour singleton;

    public CrashedUI crashedUI;
    public NewGameUI newGameUI;
    public ScoreUI scoreUI;
    public OptionUI optionUI;
    public PauseUI pauseUI;

    private void Awake()
    {
        singleton = this;
    }


}
