using UnityEngine;

public class cursor : MonoBehaviour
{
    public Texture2D texturaMira ;
    public Vector2 hotspot = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(texturaMira, hotspot, CursorMode.Auto);
    }

    
}
