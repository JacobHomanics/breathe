using UnityEngine;

public class CursorIconManager : MonoBehaviour
{
    public Texture2D tex;

    void Start()
    {
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }
}