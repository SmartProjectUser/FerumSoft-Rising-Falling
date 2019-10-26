using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Rectum;

namespace Project
{
	public class GameWindow : RectWindow
	{
		public RectText scoresText;
		public RectText lifesText;
		public RectButton startButton;

		public static AtlasLoader gameWindowAtlas;

  	public GameWindow()
    {
	    onCreate.AddListener(OnCreate);
	    onUpdate.AddListener(OnUpdate);
  	}

    private void OnCreate()
    {
			windowAlpha = 0f;

			gameWindowAtlas = new AtlasLoader(Const.ATLAS_PATH + "Main_atlas");

			Vector2 canvasSize = RectCanvas.GetCanvasSize();

	    size = new Vector2(canvasSize.x / RectUI.scaleFactor, canvasSize.y / RectUI.scaleFactor);

	    positionOnCanvas = new Vector2(0, 0);

		  AddScoresText();
		  AddLifesText();
			AddStartButton();
    }

    private void OnUpdate()
    {
	    scoresText.text = "Scores: " + GameController.scoresCount.ToString();
	    lifesText.text = "Lifes: " + GameController.lifesCount.ToString();
    }

		private void AddScoresText()
    {
			int fontSize = (int)(28 * RectUI.scaleFactor);

	    scoresText = AddChild<RectText>("ScoresText");
	    scoresText.position = new Vector2(0, 0);
	    scoresText.size = new Vector2(180, 56);
	    scoresText.text = "";
	    scoresText.font = Font.CreateDynamicFontFromOSFont("Arial", fontSize);
	    scoresText.fontSize = fontSize;
			scoresText.color = Funcs.HexToCol("#FFFFFF");
			scoresText.textAlignment = TextAnchor.MiddleCenter;
    }

		private void AddLifesText()
    {
			int fontSize = (int)(28 * RectUI.scaleFactor);

	    lifesText = AddChild<RectText>("LifesText");
	    lifesText.position = new Vector2(Screen.width / RectUI.scaleFactor - 200, 0);
	    lifesText.size = new Vector2(180, 56);
	    lifesText.text = "";
	    lifesText.font = Font.CreateDynamicFontFromOSFont("Arial", fontSize);
	    lifesText.fontSize = fontSize;
			lifesText.color = Funcs.HexToCol("#FFFFFF");
			lifesText.textAlignment = TextAnchor.MiddleCenter;
    }

		private void AddStartButton()
    {
      startButton = AddChild<RectButton>("ShopButton");
      startButton.size = new Vector2(287, 140);
      startButton.position = new Vector2(Screen.width / RectUI.scaleFactor / 2 - 140, Screen.height / RectUI.scaleFactor / 2);
			startButton.sprite = gameWindowAtlas.GetSprite("start_button");
      startButton.onButtonClick.AddListener(() =>
      {
				GameController.StartGame();
      });
    }
  }
}