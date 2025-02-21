using UnityEngine;

[System.Serializable]
public class Combo
{
    public enum InputResolutionType { Down, Up, Active }

    [System.Serializable]
    public class KeyCodeWithResolutionType
    {
        public KeyCode keyCode;
        public InputResolutionType resolutionType;
    }

    [System.Serializable]
    public class MouseButtonWithResolutionType
    {
        public int mouseButton;
        public InputResolutionType resolutionType;
    }

    public KeyCodeWithResolutionType[] keyCodes;
    public MouseButtonWithResolutionType[] mouseButtons;

    public bool IsResolved
    {
        get
        {
            bool isValid = true;

            for (int i = 0; i < keyCodes.Length; i++)
            {
                bool resolution = false;
                if (keyCodes[i].resolutionType == InputResolutionType.Down)
                    resolution = Input.GetKeyDown(keyCodes[i].keyCode);
                if (keyCodes[i].resolutionType == InputResolutionType.Up)
                    resolution = Input.GetKeyUp(keyCodes[i].keyCode);
                if (keyCodes[i].resolutionType == InputResolutionType.Active)
                    resolution = Input.GetKey(keyCodes[i].keyCode);

                isValid &= resolution;
            }

            for (int i = 0; i < mouseButtons.Length; i++)
            {
                bool resolution = false;
                if (mouseButtons[i].resolutionType == InputResolutionType.Down)
                    resolution = Input.GetMouseButtonDown(mouseButtons[i].mouseButton);
                if (mouseButtons[i].resolutionType == InputResolutionType.Up)
                    resolution = Input.GetMouseButtonUp(mouseButtons[i].mouseButton);
                if (mouseButtons[i].resolutionType == InputResolutionType.Active)
                    resolution = Input.GetMouseButton(mouseButtons[i].mouseButton);

                isValid &= resolution;
            }

            return isValid;
        }
    }

    public static int GetResolveCount(Combo[] combos)
    {
        int count = 0;

        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i].IsResolved)
                count++;
        }

        return count;
    }
}
