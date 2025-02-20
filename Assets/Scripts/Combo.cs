using UnityEngine;

[System.Serializable]
public class Combo
{
    public KeyCode[] keyCodes;
    public int[] mouseButtons;

    public bool IsResolved
    {
        get
        {
            bool isValid = true;

            for (int i = 0; i < keyCodes.Length; i++)
            {
                isValid &= Input.GetKey(keyCodes[i]);
            }

            for (int i = 0; i < mouseButtons.Length; i++)
            {
                isValid &= Input.GetMouseButton(mouseButtons[i]);
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
