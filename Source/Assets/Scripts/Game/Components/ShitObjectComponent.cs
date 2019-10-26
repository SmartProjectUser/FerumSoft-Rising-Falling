using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Bazer;

namespace Project
{
  public class ShitObjectComponent : BaseComponent
  {
    private CoroutineObject destroyRoutine;

    public new void Start()
    {
      base.Start();

      destroyRoutine = new CoroutineObject(this, OnStartDestroy);
      destroyRoutine.Start();
    }

    private IEnumerator OnStartDestroy()
    {
      int counter = 5;

      while (counter > 0) {
        yield return new WaitForSeconds(0.1f);

        counter--;
      }

      DestroyObject(gameObject);
    }
  }
}