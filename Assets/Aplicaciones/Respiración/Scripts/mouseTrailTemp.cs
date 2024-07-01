using UnityEngine;

public class mouseTrailTemp : MonoBehaviour
{
    public Transform TrailPos;

    private void Update()
    {
        Cursor.visible = false;
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TrailPos.position = cursorPos;
    }
}