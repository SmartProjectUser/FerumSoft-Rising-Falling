using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace Rectum
{
  public class RectImage : RectComponent
  {
    private Image image;

    public string color;
    public float alpha;

    public Sprite sprite
    {
      get => image.sprite;
      set => image.sprite = value;
    }

    public Image.Type type
    {
      get => image.type;
      set => image.type = value;
    }

    public Image.FillMethod fillMethod
    {
      get => image.fillMethod;
      set => image.fillMethod = value;
    }

    public float fillAmount
    {
      get => image.fillAmount;
      set => image.fillAmount = value;
    }

    public RectImage()
    {
      onInit.AddListener(OnInit);
    }

    private void OnInit()
    {
      image = gameObject.AddComponent<Image>();
    }

    public void SetColor(string color, float alpha)
    {
      image.color = RectUtils.HexToColor(color, alpha);

      this.color = color;
      this.alpha = alpha;
    }
  }
}