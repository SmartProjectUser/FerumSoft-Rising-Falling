using UnityEngine;

namespace Project
{
  public class Game
  {
    public static void Start()
    {
      GameState.LoadState();

      GameUI.Init();

      GameController.Init();
    }
  }
}