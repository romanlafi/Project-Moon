using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum ControllerType
{
    PlayStation,
    Xbox,
    Keyboard
}

public class ButtonLayout : MonoBehaviour
{
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;

    private ControllerType currentControllerType;

    private void OnEnable ()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.layout == "DualShockGamepadHID")
            {
                currentControllerType = ControllerType.PlayStation;
            }
            else if (Gamepad.current.layout == "XboxGamepadHID")
            {
                currentControllerType = ControllerType.Xbox;
            }
        }
        else
        {
            currentControllerType = ControllerType.Keyboard;
        }

        UpdateButtonImage();
    }

    private void UpdateButtonImage ()
    {
        switch (currentControllerType)
        {
            case ControllerType.PlayStation:
                targetButton.sprite = buttonSprites[0];
                break;
            case ControllerType.Xbox:
                targetButton.sprite = buttonSprites[1];
                break;
            case ControllerType.Keyboard:
                targetButton.sprite = buttonSprites[2];
                break;
        }
    }
}
