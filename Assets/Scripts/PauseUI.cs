using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private bool Paused = false;
    public void Button()
    {
        Time.timeScale = Paused ? 1 : 0;
        Paused = !Paused;
    
        UIBIheviour.singleton.optionUI.gameObject.SetActive(Paused);
    }
}
