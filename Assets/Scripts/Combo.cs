using UnityEngine;

[System.Serializable]
public class Combo
{
    public KeyCode[] keyCodes;
    public int[] mouseButtons;

    public static bool Resolve(Combo combo)
    {
        bool isValid = true;

        for (int i = 0; i < combo.keyCodes.Length; i++)
        {
            isValid &= Input.GetKey(combo.keyCodes[i]);
        }

        for (int i = 0; i < combo.mouseButtons.Length; i++)
        {
            isValid &= Input.GetMouseButton(combo.mouseButtons[i]);
        }

        return isValid;
    }

    public static int GetResolveCount(Combo[] combos)
    {
        int count = 0;

        for (int i = 0; i < combos.Length; i++)
        {
            if (Resolve(combos[i]))
                count++;
        }

        return count;
    }
}
