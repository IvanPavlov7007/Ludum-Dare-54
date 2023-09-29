using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;

public class SettingsMenu : MenuBase
{
    public Toggle thisToggle, thatToggle;
    public Slider thoseSlider;

    protected override void Start()
    {
        base.Start();
        thisToggle.isOn = IntersceneData._this;
        thatToggle.isOn = IntersceneData._that;
        thoseSlider.value = IntersceneData._those;
    }

    public void ToggleThis(bool val)
    {
        IntersceneData._this = val;
    }

    public void ToggleThat(bool val)
    {
        IntersceneData._that = val;
    }

    public void ChangeThose(float val)
    {
        IntersceneData._those = val;
    }

    public void Back()
    {
        uiStateMachine.Previous();
    }
}
