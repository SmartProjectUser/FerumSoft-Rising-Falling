using UnityEngine;

namespace Project
{
  public class DragHelper
  {
    public static DraggedDirection GetDragDirection(Vector3 dragVector)
    {
      float positiveX = Mathf.Abs(dragVector.x);
      float positiveY = Mathf.Abs(dragVector.y);

      DraggedDirection draggedDir;

      if (positiveX > positiveY)
      {
        draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
      }
      else
      {
        draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
      }

      return draggedDir;
    }
  }
}