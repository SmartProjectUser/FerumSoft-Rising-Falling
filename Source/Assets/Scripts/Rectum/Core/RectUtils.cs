using UnityEngine;

using System;

namespace Rectum
{
  public class RectUtils
  {
    public static Color HexToColor(string hex, float alpha = 1.0f)
    {
      if (hex != null)
      {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        byte a = 255; //assume fully visible unless specified in hex

        if (hex.Length == 8)
        {
          a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color(r, g, b, alpha);
      }

      return new Color();
    }

    public static Sprite LoadSprite(string path, string file)
    {
      return Resources.Load<Sprite>(path + file);
    }

    public static Vector2 ProjectPositionToScreen(Vector3 objectPosition)
    {
      Vector2 resultPosition = Camera.main.WorldToScreenPoint(objectPosition);

      resultPosition.y = Screen.height - resultPosition.y;

      return resultPosition / RectUI.scaleFactor;
    }
  }
}
