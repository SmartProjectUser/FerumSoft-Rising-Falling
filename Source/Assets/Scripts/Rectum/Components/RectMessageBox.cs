using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;
using System.Collections;

namespace Rectum
{
  public class RectMessageBox : RectComponent
  {
    private static GameObject messageBoxObject;

    public float timer = -6f;

    public static float topMargin = 40f;

    public static float buttonMargin = 10f;

    public static float showTime = 2f;

    public static float windowAlpha = 0.5f;
    public static string windowColor = "#000000";

    public static string buttonColor = "#111111";

    public static string buttonFont = "Arial.ttf";
    public static string messageFont = "Arial.ttf";

    public static int boxWidth = 360;
    public static int boxHeight = 110;

    public static int messageWidth = 260;
    public static int messageHeight = 110;

    public static int buttonWidth = 80;
    public static int buttonHeight = 20;

    private RectObjectEvent callback = new RectObjectEvent();
    private object target;

    public RectMessageBox()
    {
      onInit.AddListener(() =>
      {
        gameObject.name = "MessageBox";

        // Image image = gameObject.AddComponent<Image> ();
        // image.color = Funcs.hex_to_col (wnd_color, wnd_alpha);

        size = new Vector2(boxWidth, boxHeight);
        positionOnCanvas = new Vector2(Screen.width / 2 - boxWidth / 2, 80);
      });
    }

    void Update()
    {
      if (timer > -1)
      {
        timer -= Time.deltaTime;
      }

      if (timer < 0 && timer > -6)
      {
        Destroy(gameObject);
      }
    }

    protected static GameObject Init()
    {
      GameObject canvas = RectCanvas.GetCanvasObject();

      GameObject messageBoxObject = new GameObject("Message Box");
      messageBoxObject.transform.SetParent(canvas.transform);
      messageBoxObject.AddComponent<RectMessageBox>();

      return messageBoxObject;
    }

    public static bool IsShowing()
    {
      return messageBoxObject != null ? true : false;
    }

    public static void Hint(string text)
    {
      if (messageBoxObject == null)
      {
        GameObject messageBoxObject = RectMessageBox.Init();

        AddMessageText(messageBoxObject.transform, text);

        messageBoxObject.GetComponent<RectMessageBox>().timer = showTime;

        RectMessageBox.messageBoxObject = messageBoxObject;
      }
    }

    public static void Show(string text)
    {
      if (messageBoxObject == null)
      {
        GameObject messageBoxObject = RectMessageBox.Init();

        AddMessageText(messageBoxObject.transform, text);

        AddButtonOk(messageBoxObject.transform);

        RectMessageBox.messageBoxObject = messageBoxObject;
      }
    }

    public static void Show(string text, RectObjectEvent callback, object target = null)
    {
      if (messageBoxObject == null)
      {
        GameObject messageBoxObject = RectMessageBox.Init();

        AddMessageText(messageBoxObject.transform, text);

        AddButtonYes(messageBoxObject.transform);
        AddButtonNo(messageBoxObject.transform);

        RectMessageBox messageBox = messageBoxObject.GetComponent<RectMessageBox>();
        messageBox.target = target;
        messageBox.callback = callback;

        RectMessageBox.messageBoxObject = messageBoxObject;
      }
    }

    public void Close()
    {
      Destroy(gameObject);

      messageBoxObject = null;
    }

    private static void AddMessageText(Transform parent, string text)
    {
      GameObject messageTextObject = new GameObject("Text");
      messageTextObject.transform.SetParent(parent);

      RectTransform rectTransform = messageTextObject.AddComponent<RectTransform>();
      rectTransform.SetWidth(messageWidth);
      rectTransform.SetHeight(messageHeight);
      rectTransform.SetLeftTopPosition(new Vector2(0, 0));

      Text textComponent = messageTextObject.AddComponent<Text>();
      textComponent.font = Resources.GetBuiltinResource(typeof(Font), messageFont) as Font;
      textComponent.text = text;
      textComponent.alignment = TextAnchor.MiddleCenter;
    }

    private static void AddButtonText(GameObject buttonObject, string text)
    {
      GameObject buttonTextObject = new GameObject("Button Text");
      buttonTextObject.transform.SetParent(buttonObject.transform);

      RectTransform rectTransform = buttonTextObject.AddComponent<RectTransform>();
      rectTransform.SetWidth(buttonWidth);
      rectTransform.SetHeight(buttonHeight);
      rectTransform.SetLeftTopPosition(new Vector2(0, 0));

      Text textComponent = buttonTextObject.AddComponent<Text>();
      textComponent.font = Resources.GetBuiltinResource(typeof(Font), buttonFont) as Font;
      textComponent.alignment = TextAnchor.MiddleCenter;
      textComponent.text = text;
    }

    private static void AddButtonOk(Transform parent)
    {
      GameObject buttonObject = new GameObject("Button");
      buttonObject.transform.SetParent(parent);

      RectTransform rectTransform = buttonObject.AddComponent<RectTransform>();
      rectTransform.SetWidth(buttonWidth);
      rectTransform.SetHeight(buttonHeight);
      rectTransform.SetLeftTopPosition(new Vector2(boxWidth - (buttonWidth + buttonMargin), (buttonHeight + buttonMargin) - boxHeight));

      Image image = buttonObject.AddComponent<Image>();
      image.color = RectUtils.HexToColor(buttonColor, windowAlpha);

      Button button = buttonObject.AddComponent<Button>();
      button.targetGraphic = image;
      button.onClick.AddListener(parent.gameObject.GetComponent<RectMessageBox>().ButtonOkClick);

      AddButtonText(buttonObject, "Ok");
    }

    public static void AddButtonYes(Transform parent)
    {

    }

    public static void AddButtonNo(Transform parent)
    {

    }

    public void ButtonOkClick()
    {
      this.Close();
    }

    public void ButtonYesClick()
    {
      callback.Invoke(target);
    }

    public void ButtonNoClick()
    {
      this.Close();
    }
  }
}