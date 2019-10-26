using UnityEngine;

using Bazer;

namespace Project
{
  [RequireComponent(typeof(BoxCollider2D))]
  [RequireComponent(typeof(Rigidbody2D))]
  public class ObstacleObjectComponent : BaseComponent
  {
    public new void Start()
    {
      base.Start();

      var rigidBody = gameObject.GetComponent<Rigidbody2D>();

      rigidBody.mass = 0;
      rigidBody.gravityScale = 0;
      rigidBody.bodyType = RigidbodyType2D.Dynamic;
      rigidBody.constraints = RigidbodyConstraints2D.FreezePosition |
                              RigidbodyConstraints2D.FreezeRotation;
    }
  }
}