using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;

namespace Rectum
{
  public class RectCanvas : RectComponent, IPointerClickHandler
  {
    public static GameObject canvasObject;

    private static string canvasObjectName = "CanvasObject";
    private static string eventSystemName = "EventSystem";

    protected UnityEvent onLeftMouseClick = new UnityEvent();
    protected UnityEvent onRightMouseClick = new UnityEvent();

    public RectCanvas()
    {
      onInit.AddListener(OnInit);
    }

    private void OnInit()
    {
      AddTransparentImage();
    }

    public static GameObject GetCanvasObject()
    {
      if (canvasObject == null)
      {
        canvasObject = GameObject.Find(canvasObjectName);
      }

      return canvasObject;
    }

    public static Vector2 GetCanvasSize()
    {
      return new Vector2(Screen.width, Screen.height);
    }

    public static Vector2 GetCenterPosition(Vector2 size)
    {
      return new Vector2(
				(Screen.width - size.x * RectUI.scaleFactor) / 2,
				(Screen.height - size.y * RectUI.scaleFactor) / 2
			);
    }

    private void AddTransparentImage()
    {
      Image image = gameObject.AddComponent<Image>();
      image.color = new Color(255, 255, 255, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      // Нажата левая кнопка мыши на канву
      if (eventData.button == PointerEventData.InputButton.Left)
      {
        onLeftMouseClick.Invoke();
      }

      // Нажата правая кнопка мыши на канву
      if (eventData.button == PointerEventData.InputButton.Right)
      {
        onRightMouseClick.Invoke();
      }
    }

    protected static void InitCanvasWith(Type type)
    {
      AddCanvasObject(type);
      AddEventSystem();
    }

    protected static void AddCanvasObject(Type type)
    {
      canvasObject = GameObject.Find(canvasObjectName);

      if (canvasObject == null)
      {
        canvasObject = new GameObject(canvasObjectName);
        canvasObject.AddComponent(type);

        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = false;
        canvas.sortingOrder = 0;

        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        canvasScaler.scaleFactor = 1;
        canvasScaler.referencePixelsPerUnit = 100;

        GraphicRaycaster graphicRaycaster = canvasObject.AddComponent<GraphicRaycaster>();
        graphicRaycaster.ignoreReversedGraphics = true;
        graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
      }
    }

    protected static void AddEventSystem()
    {
      if (GameObject.Find(eventSystemName) == null)
      {
        GameObject eventSystemObject = new GameObject(eventSystemName);

        EventSystem eventSystem = eventSystemObject.AddComponent<EventSystem>();
        eventSystem.firstSelectedGameObject = null;
        eventSystem.sendNavigationEvents = true;
        eventSystem.pixelDragThreshold = 5;

        StandaloneInputModule standaloneInputModule = eventSystemObject.AddComponent<StandaloneInputModule>();
        standaloneInputModule.horizontalAxis = "Horizontal";
        standaloneInputModule.verticalAxis = "Vertical";
        standaloneInputModule.submitButton = "Submit";
        standaloneInputModule.cancelButton = "Cancel";
        standaloneInputModule.inputActionsPerSecond = 10;
        standaloneInputModule.forceModuleActive = false;
      }
    }
  }
}