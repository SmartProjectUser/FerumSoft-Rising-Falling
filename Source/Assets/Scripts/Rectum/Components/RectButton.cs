using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

namespace Rectum
{
  public class RectButton : RectComponent, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
  {
    private Button buttonComponent;
    private Image imageComponent;

    public RectText rectText;
    public RectImage rectImage;

    public Sprite normalState;
    public Sprite hoverState;
    public Sprite pressedState;
    public Sprite disabledState;

    private string fontName = "Arial.ttf";

    public string buttonColor = "#111111";
    public float buttonAlpha = 0.5f;

    public UnityEvent onButtonClick = new UnityEvent();

    public UnityEvent onMousePress = new UnityEvent();
    public UnityEvent onMouseRelease = new UnityEvent();
    public UnityEvent onMouseEnter = new UnityEvent();
    public UnityEvent onMouseLeave = new UnityEvent();

    public Sprite sprite
    {
      get => rectImage.sprite;
      set {
        rectImage.size = size;
        rectImage.sprite = value;
      }
    }

    public Vector2 spriteSize
    {
      get => rectImage.size;
      set => rectImage.size = value;
    }

    public Vector2 spritePosition
    {
      get => rectImage.position;
      set => rectImage.position = value;
    }

    public string text
    {
      get => rectText.text;
      set
      {
        rectText.size = size;
        rectText.text = value;
      }
    }

    private RectAnchors spriteAnchor;
    public RectAnchors spriteAnchors
    {
      get => spriteAnchor;
      set => spriteAnchor = value;
    }

    public Font font
    {
      get => rectText.font;
      set => rectText.font = value;
    }

    public int fontSize
    {
      get => rectText.fontSize;
      set => rectText.fontSize = value;
    }

    public FontStyle fontStyle
    {
      get => rectText.fontStyle;
      set => rectText.fontStyle = value;
    }

    public TextAnchor textAlignment
    {
      get => rectText.textAlignment;
      set => rectText.textAlignment = value;
    }

    public RectButton()
    {
      onInit.AddListener(OnInit);
      onSizeChange.AddListener(OnSizeChange);
    }

    private void OnInit()
    {
      AddButtonComponent();
      AddImageComponent();
      AddSpriteComponent();
      AddTextComponent();
    }

    private void OnSizeChange()
    {
      if (rectImage != null)
      {
        RectAnchors anchor = RectAnchors.FillRect;

        switch (anchor)
        {
          case RectAnchors.FillRect:
            imageComponent.color = RectUtils.HexToColor("#FFFFFF", 0);

            rectImage.size = size;
            rectImage.position = new Vector2(0, 0);
            break;

          case RectAnchors.MiddleLeft:
            rectImage.size = size;
            rectImage.position = new Vector2(0, size.y / 2 - size.y / 2);
            break;
        }

        spriteAnchor = anchor;
      }
    }

    public void SetSprites(Sprite normalState,
                          Sprite pressedState = default(Sprite),
                          Sprite hoverState = default(Sprite),
                          Sprite disabledState = default(Sprite))
    {
      this.normalState = normalState;
      this.pressedState = pressedState;
      this.hoverState = hoverState;
      this.disabledState = disabledState;

      if (enabled)
      {
        sprite = normalState;
      }
      else
      {
        sprite = disabledState;
      }

      rectImage.size = size;
    }

    private void AddButtonComponent()
    {
      buttonComponent = gameObject.AddComponent<Button>();
      buttonComponent.targetGraphic = imageComponent;
      buttonComponent.onClick.AddListener(() =>
      {
        if (enabled)
        {
          onButtonClick.Invoke();
        }
      });
    }

    private void AddImageComponent()
    {
      imageComponent = gameObject.AddComponent<Image>();
      imageComponent.color = RectUtils.HexToColor(buttonColor, buttonAlpha);
    }

    private void RemoveImageComponent()
    {
      Destroy(imageComponent);
    }

    private void AddTextComponent()
    {
      int fontSize = (int)(24 * RectUI.scaleFactor);

      rectText = AddChild<RectText>("Text");
      rectText.size = size;
      rectText.position = new Vector2(0, 0);
      rectText.font = Font.CreateDynamicFontFromOSFont("Arial", fontSize);
      rectText.fontSize = fontSize;
      rectText.textAlignment = TextAnchor.MiddleCenter;
    }

    private void AddSpriteComponent()
    {
      rectImage = AddChild<RectImage>("Sprite");
      rectImage.size = new Vector2(0, 0);
      rectImage.position = new Vector2(0, 0);
    }

    public void OnPointerEnter(PointerEventData data)
    {
      if (enabled)
      {
        if (hoverState != null)
        {
          sprite = hoverState;
        }

        onMouseEnter.Invoke();
      }
    }

    public void OnPointerExit(PointerEventData data)
    {
      if (enabled)
      {
        if (hoverState != null)
        {
          sprite = normalState;
        }

        onMouseLeave.Invoke();
      }
    }

    public void OnPointerDown(PointerEventData data)
    {
      if (enabled)
      {
        if (pressedState != null)
        {
          sprite = pressedState;
        }

        onMousePress.Invoke();
      }
    }

    public void OnPointerUp(PointerEventData data)
    {
      if (enabled)
      {
        if (pressedState != null)
        {
          sprite = normalState;
        }

        onMouseRelease.Invoke();
      }
    }
  }
}