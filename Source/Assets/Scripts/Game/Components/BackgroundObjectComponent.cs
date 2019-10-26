using UnityEngine;

using Bazer;

namespace Project
{
  [RequireComponent(typeof(BoxCollider2D))]
  public class BackgroundObjectComponent : BaseComponent
  {
    public new void Start()
    {
      base.Start();
    }
  }
}