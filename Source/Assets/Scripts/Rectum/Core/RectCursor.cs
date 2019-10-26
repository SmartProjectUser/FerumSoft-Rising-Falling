using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace Rectum
{
  public class RectCursor
  {
    public static RectImage guiCursor;

    private static RectTransform rectTransform;

    private static int cursorSize = 32;

    private static string cursorObjName = "Cursor";

    static RectCursor()
    {
      CreateGuiCursor();
    }

    private static void CreateGuiCursor()
    {
      GameObject canvas = RectCanvas.GetCanvasObject();

      float xPos = Screen.width / 2 - (cursorSize / 2);
      float yPos = Screen.height / 2 - (cursorSize / 2);

      RectComponent canvasComponent = canvas.GetComponent<RectComponent>();

      guiCursor = canvasComponent.AddChild<RectImage>(cursorObjName);

      RectImage guiCursorComponent = guiCursor.GetComponent<RectImage>();
      guiCursorComponent.size = new Vector2(cursorSize, cursorSize);
      guiCursorComponent.position = new Vector2(xPos, yPos);

      guiCursor.gameObject.SetActive(false);
    }

    public static void SetCurs(string path, string file)
    {
      Cursor.SetCursor(RectUtils.LoadSprite(path, file).texture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public static void ShowCurs()
    {
      Cursor.visible = true;
    }

    public static void HideCurs()
    {
      Cursor.visible = false;
    }

    public static void ShowGuiCurs(Sprite sprite)
    {
      RectImage guiCursorComp = guiCursor.GetComponent<RectImage>();

      if (guiCursorComp.sprite == null)
      {
        guiCursorComp.sprite = sprite;
      }

      guiCursor.gameObject.SetActive(true);
    }

    public static void HideGuiCurs()
    {
      guiCursor.gameObject.SetActive(false);
    }
  }
}