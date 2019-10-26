using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

using Rectum;

namespace Project
{
  public class GameUI : RectUI
  {
    public static void Init()
    {
      InitCanvasWith(typeof(GameUI));
    }

    public GameUI()
    {
      onFirstWindowOpen.AddListener(OnFirstWindowOpened);
      onLastWindowClose.AddListener(OnLastWindowClosed);

      onUpdate.AddListener(OnUpdateUI);

      onLeftMouseClick.AddListener(OnLeftMouseClick);

      onInit.AddListener(OnInit);
    }

    public void OnInit()
    {
      OpenWindow<GameWindow>();
    }

    public void OnFirstWindowOpened()
    {
    }

    public void OnLastWindowClosed()
    {
    }

    protected void OnUpdateUI()
    {
    }

    protected void OnLeftMouseClick()
    {
    }
  }
}