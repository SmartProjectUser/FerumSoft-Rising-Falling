using UnityEngine;

using System;
using System.Collections;

namespace Project
{
  public static class Extends {
    public static int ToInt(this object obj) => Convert.ToInt32(obj);
  }
}