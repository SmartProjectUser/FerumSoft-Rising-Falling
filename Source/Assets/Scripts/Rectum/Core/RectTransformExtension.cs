using UnityEngine;

using System.Collections;

namespace Rectum
{
  public static class RеctTransformExtension
  {
    public static Vector2 GetScreenCenter()
    {
      GameObject canvasObject = RectCanvas.GetCanvasObject();

      RectTransform canvasTransformComponent = canvasObject.GetComponent<RectTransform>();

      return new Vector2(canvasTransformComponent.pivot.x - Screen.width / 2, canvasTransformComponent.pivot.y + Screen.height / 2);
    }

    public static Vector2 PosOnScreen(Vector2 pos)
    {
      Vector2 v2 = GetScreenCenter();

      return new Vector2(v2.x + pos.x, v2.y - pos.y);
    }

    /*public static void SetPivot(this RectTransform trans, Vector2 newPos)
    {
      trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
    }*/

    public static void SetCenterPivot(this RectTransform trans)
    {
      trans.pivot = new Vector2(0.5f, 0.5f);
    }

    public static void SetLeftTopPivot(this RectTransform trans)
    {
      trans.pivot = new Vector2(0, 1);

      trans.anchorMin = new Vector2(0, 1);
      trans.anchorMax = new Vector2(0, 1);
    }

    /*public static void SetLeftBottomPivot(this RectTransform trans){
      trans.pivot = new Vector2(0,1);

      trans.anchorMin = new Vector2(0,0);
      trans.anchorMax = new Vector2(0,0);
    }*/

    public static void SetAnchors(this RectTransform trans, Vector2 minPos, Vector2 maxPos)
    {
      trans.anchorMin = minPos;
      trans.anchorMax = maxPos;
    }

    public static void SetCenterPosition(this RectTransform trans, Vector2 newPos)
    {
      SetCenterPivot(trans);

      trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
    {
      SetLeftTopPivot(trans);

      trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
    {
      trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }

    public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos) {
      trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos) {
      trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }

    public static void SetSize(this RectTransform trans, Vector2 newSize)
    {
      Vector2 oldSize = trans.rect.size;
      Vector2 deltaSize = newSize - oldSize;

      trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
      trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }

    public static void SetDefaultScale(this RectTransform trans)
    {
      trans.localScale = new Vector3(1, 1, 1);
    }

    public static Vector2 GetSize(this RectTransform trans)
    {
      return trans.rect.size;
    }

    public static float GetWidth(this RectTransform trans)
    {
      return trans.rect.width;
    }

    public static float GetHeight(this RectTransform trans)
    {
      return trans.rect.height;
    }

    public static void SetWidth(this RectTransform trans, float newSize)
    {
      SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }

    public static void SetHeight(this RectTransform trans, float newSize)
    {
      SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
  }
}