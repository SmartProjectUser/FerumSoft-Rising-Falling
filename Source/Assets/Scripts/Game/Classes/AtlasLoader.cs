using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Project
{
  public class AtlasLoader
  {
    public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

    public AtlasLoader() { }

    public AtlasLoader(string atlasPath)
    {
      LoadAtlas(atlasPath);
    }

    public void LoadAtlas(string atlasPath)
    {
      Sprite[] allSprites = Resources.LoadAll<Sprite>(atlasPath);

      if (allSprites == null || allSprites.Length <= 0)
      {
        Debug.LogError("Atlas with name `" + atlasPath + "` not found.");

        return;
      }

      for (int i = 0; i < allSprites.Length; i++)
      {
        spriteDic.Add(allSprites[i].name, allSprites[i]);
      }
    }

    public Sprite GetSprite(string spriteName)
    {
      Sprite tempSprite = null;

      if (spriteName != null && !spriteDic.TryGetValue(spriteName, out tempSprite))
      {
        Debug.LogError("Sprite with name `" + spriteName + "` not found.");

        return null;
      }

      return tempSprite;
    }

    public int GetSpritesCount()
    {
      return spriteDic.Count;
    }
  }
}