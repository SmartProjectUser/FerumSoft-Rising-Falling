using UnityEngine;

using System;
using System.Collections;

namespace Project
{
  public class Sprites
  {
    private static Sprite[] sprites;
    private static string[] spritesArray;

    public static Sprite LoadSprite(string file)
    {
      return Resources.Load<Sprite>(Const.SPRITES_PATH + file);
    }

    public static Sprite GetSprite(string name)
    {
      Sprite sprite = null;

      if (spritesArray is null)
      {
        LoadSprites();
      }

      try
      {
        sprite = sprites[Array.IndexOf(spritesArray, name)];
      }
      catch (IndexOutOfRangeException e)
      {
        Funcs.Throw("Sprite not found" + " " + name);
      }

      return sprite;
    }

    private static void LoadSprites()
    {
      sprites = Resources.LoadAll<Sprite>(Const.SPRITES_PATH);

      spritesArray = new string[sprites.Length];

      for (int i = 0; i < spritesArray.Length; i++)
      {
        spritesArray[i] = sprites[i].name;
      }
    }
  }
}