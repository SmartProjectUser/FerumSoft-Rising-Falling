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
		public RectButton resetStateButton;

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
    }

    private void OnUpdate()
    {
	    scoresText.text = "Scores: " + GameController.scoresCount.ToString();
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
  }
}