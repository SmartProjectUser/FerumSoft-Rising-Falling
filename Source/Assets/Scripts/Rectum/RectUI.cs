using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Rectum
{
  public class RectUI : RectCanvas
  {
    public static List<RectWindow> windowsList = new List<RectWindow>();

    protected static UnityEvent onFirstWindowOpen = new UnityEvent();
    protected static UnityEvent onLastWindowClose = new UnityEvent();

    public static float scaleFactor = 1;
    private const float ethalonScreenWidth = 720;

    public RectUI() {
      scaleFactor = Screen.width / ethalonScreenWidth;
    }

    public static T OpenWindow<T>() where T: RectWindow
    {
      if (IsWindowOpen<T>())
      {
        return null;
      }

      if (windowsList.Count == 0)
      {
        onFirstWindowOpen.Invoke();
      }

      GameObject windowObject = new GameObject(typeof(T).ToString());

      windowObject.transform.SetParent(canvasObject.transform);

      var windowComponent = windowObject.AddComponent(typeof(T));

      windowsList.Add(windowComponent as RectWindow);

      return (T)windowComponent;
    }

    public static T GetWindow<T>() where T: RectWindow
    {
      if (IsWindowOpen<T>())
      {
        foreach (RectWindow currRectWindow in windowsList)
        {
          if (currRectWindow is T)
          {
            return currRectWindow as T;
          }
        }
      }

      return null;
    }

    public static void CloseWindow<T>() where T: RectWindow
    {
      if (windowsList.Count == 1)
      {
        onLastWindowClose.Invoke();
      }

      foreach (RectWindow currRectWindow in windowsList)
      {
        if (currRectWindow is T)
        {
          windowsList.Remove(currRectWindow);

          Destroy(currRectWindow.gameObject);

          break;
        }
      }
    }

    public static bool IsWindowOpen<T>() where T: RectWindow
    {
      foreach (RectWindow currRectWindow in windowsList)
      {
        if (currRectWindow is T)
        {
          return true;
        }
      }

      return false;
    }

    public static bool IsAnyWindowOpen()
    {
      return windowsList.Count > 0;
    }

    public static void CloseAllWindows()
    {
      while (windowsList.Count > 0)
      {
        var currRectWindow = windowsList[0];

        windowsList.Remove(currRectWindow);

        Destroy(currRectWindow.gameObject);
      }
    }
  }
}