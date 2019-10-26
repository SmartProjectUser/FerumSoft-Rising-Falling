using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace Rectum
{
  public class RectText : RectComponent
  {
    private Text textComponent;

    public string text
    {
      get => textComponent.text;
      set => textComponent.text = value;
    }

    public Font font
    {
      get => textComponent.font;
      set => textComponent.font = value;
    }

    public Color color
    {
      get => textComponent.color;
      set => textComponent.color = value;
    }

    public int fontSize
    {
      get => textComponent.fontSize;
      set => textComponent.fontSize = value;
    }

    public float lineSpacing
    {
      get => textComponent.lineSpacing;
      set => textComponent.lineSpacing = value;
    }

    public TextAnchor textAlignment
    {
      get => textComponent.alignment;
      set => textComponent.alignment = value;
    }

    public FontStyle fontStyle
    {
      get => textComponent.fontStyle;
      set => textComponent.fontStyle = value;
    }

    public RectText()
    {
      onInit.AddListener(() =>
      {
        textComponent = gameObject.AddComponent<Text>();
      });
    }

    /*public void SetTextAlign(TextAlignment align){
      textComponent.alignment = align;
    }*/
  }
}