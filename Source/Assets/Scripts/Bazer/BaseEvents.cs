using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Bazer
{
  public class CollisionEvent : UnityEvent<Collision> { }
  public class Collision2DEvent : UnityEvent<Collision2D> { }
  public class TriggerEvent : UnityEvent<Collider> { }
  public class Trigger2DEvent : UnityEvent<Collider2D> { }
  public class BoolEvent : UnityEvent<bool> { }
  public class DragEndEvent: UnityEvent<PointerEventData> { }
}