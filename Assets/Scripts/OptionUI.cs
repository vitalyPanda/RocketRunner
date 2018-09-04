using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text sensText;
    public Slider sensitive;
    public Toggle toogle;
    public void Init()
    {
        sensText.text = GameBiheviour.singleton.Rocket.Divider.ToString("#.##");
        sensitive.value = GameBiheviour.singleton.Rocket.Divider;
        toogle.isOn = GameBiheviour.singleton.Rocket.Revers;
    }

    public void ChangeToggle(bool toggle)
    {
        GameBiheviour.singleton.Rocket.Revers = toogle;
    }
    public void ChangeSens(float sens)
    {
        GameBiheviour.singleton.Rocket.Divider = sens;
        sensText.text = sens.ToString("0.##");
    }
}
