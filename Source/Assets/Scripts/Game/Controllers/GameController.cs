using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

using Bazer;

namespace Project
{
  public class GameController: BaseController
  {
    public static GameController inst;

    public static List<BaseObject> obstacleList = new List<BaseObject>();

    public static bool isPlaying = false;
    public static GameObject musicObject;

    public static string deadObstacleName = "Obstacle_5";

    private static int obstacleTypesCount = 6;

    public static float leftBorder = -4f;
    public static float rightBorder = 4f;

    public static float moveSpeed = 6f;
    public static float fallingSpeed = 2f;
    public static float respawnTime = 1f;

    public bool moveLeft = false;
    public bool moveRight = false;

    public static int scoresCount;
    public static int lifesCount = 10;

    public static string startingPlaceName = "Falling_start";
    public static string backgroundObjectName = "Background";

    public string[] respawnPlaceNames = {"Respawn_0", "Respawn_1", "Respawn_2", "Respawn_3", "Respawn_4"};

    public FallingObject fallingObject;
    public BackgroundObject backgroundObject;
    public BaseObject fallingObjectPlace;
    public BaseObject[] respawnObjectPlaces = new BaseObject[5];

    private CoroutineObject moveRoutine;

    public static void Init()
    {
      inst = new GameController();

      inst.AddToScene();
      inst.StartIntervalUpdate(1f);

      inst.gameComponent.onStart.AddListener(inst.OnStart);
      inst.gameComponent.onUpdate.AddListener(inst.OnUpdate);
      inst.gameComponent.onIntervalUpdate.AddListener(inst.OnIntervalUpdate);

      inst.AssignBackgroundObject();
      inst.AssignFallingObjectPlace();
      inst.AssignRespawnPlaces();
    }

    public GameController() { }

    public void OnIntervalUpdate()
    {
      if (isPlaying)
      {
        GenerateObstacleObject();
      }
    }

    public void OnStart()
    {

    }

    public static void StartGame()
    {
      var gameWindow = GameUI.GetWindow<GameWindow>();
      gameWindow.startButton.active = false;

      isPlaying = true;

      scoresCount = 0;
      lifesCount = 10;
      fallingSpeed = 2f;

      GameController.inst.fallingObject = new FallingObject();
      GameController.inst.fallingObject.CreateFallingObject();

      musicObject = Audio.PlaySound(GameController.inst.backgroundObject.gameObject, Const.SOUNDS_PATH + "music", true);
    }

    public static void StopGame()
    {
      var gameWindow = GameUI.GetWindow<GameWindow>();
      gameWindow.startButton.active = true;

      RemoveAllObstacles();

      GameObject.Destroy(musicObject);
      GameController.inst.fallingObject.DestroyObject();

      Audio.PlaySound(GameController.inst.backgroundObject.gameObject, Const.SOUNDS_PATH + "game_over");

      isPlaying = false;
    }

    public void OnUpdate()
    {
      if (isPlaying)
      {
        if (fallingObject.gameObject.transform.position.x > GameController.leftBorder && moveLeft)
        {
          fallingObject.gameObject.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }

        if (fallingObject.gameObject.transform.position.x < GameController.rightBorder && moveRight)
        {
          fallingObject.gameObject.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }

        CheckObjectAlive();
      }
    }

    public void AssignBackgroundObject()
    {
      backgroundObject = new BackgroundObject();

      backgroundObject.AssignBackgroundObject();
    }

    public void AssignFallingObjectPlace()
    {
      BaseObjectParams objectParams = new BaseObjectParams
      {
        name = startingPlaceName,
      };

      fallingObjectPlace = new BaseObject();

      fallingObjectPlace.AssignObject(objectParams);
    }

    public void AssignRespawnPlaces()
    {
      for (int i = 0; i < GameController.inst.respawnPlaceNames.Length; i++)
      {
        BaseObjectParams objectParams = new BaseObjectParams
        {
          name = GameController.inst.respawnPlaceNames[i],
        };

        GameController.inst.respawnObjectPlaces[i] = new BaseObject();

        GameController.inst.respawnObjectPlaces[i].AssignObject(objectParams);
      }
    }

    public void GenerateObstacleObject()
    {
      var obstacleRespawnIndex = UnityEngine.Random.Range(0, respawnObjectPlaces.Length);
      var obstacleTypeIndex = UnityEngine.Random.Range(0, obstacleTypesCount);

      var obstacleObject = new ObstacleObject(obstacleRespawnIndex, obstacleTypeIndex);
      obstacleObject.CreateObstacleObject();

      obstacleList.Add(obstacleObject);
    }

    private void CheckObjectAlive()
    {
      for (int i = 0; i < obstacleList.Count; i++)
      {
        var obstacle = obstacleList[i];

        if (
          obstacle.gameObject.transform.position.y - fallingObjectPlace.gameObject.transform.position.y > 5 &&
          obstacle.gameObject.name != deadObstacleName
        )
        {
          obstacleList.Remove(obstacle);
          obstacle.DestroyObject();

          lifesCount--;

          if (lifesCount == 0)
          {
            StopGame();
          }

          break;
        }
      }
    }

    private void StopMoving()
    {
      moveLeft = false;
      moveRight = false;
    }

    private void RunMoveTimer()
    {
      moveRoutine = new CoroutineObject(gameComponent, OnMove);
      moveRoutine.Start();
    }

    private IEnumerator OnMove()
    {
      int counter = 3;

      while (counter > 0) {
        yield return new WaitForSeconds(0.1f);

        counter--;
      }

      StopMoving();
      moveRoutine.Stop();
    }

    public static void SwipeObject(DraggedDirection draggedDirrection)
    {
      GameController.inst.StopMoving();

      switch(draggedDirrection)
      {
        case DraggedDirection.Left:
          GameController.inst.moveLeft = true;
        break;

        case DraggedDirection.Right:
          GameController.inst.moveRight = true;
        break;

        default: break;
      }

      GameController.inst.RunMoveTimer();
    }

    public static void ContactObject(Collision2D collision)
    {
      var facedObstacle = obstacleList.Find(obstacleObject => obstacleObject.gameObject == collision.gameObject);

      if(facedObstacle != null)
      {
        if (collision.gameObject.name != deadObstacleName)
        {
          ShowShirParticles(facedObstacle.gameObject.transform.position);

          obstacleList.Remove(facedObstacle);
          facedObstacle.DestroyObject();

          UpdateScores();

          Audio.PlaySound(GameController.inst.fallingObject.gameObject, Const.SOUNDS_PATH + "collision");
        }
        else
        {
          StopGame();
        }
      }
    }

    public static void ShowShirParticles(Vector3 position)
    {
      BaseObjectParams objectParams = new BaseObjectParams
      {
        path = Const.PARTICLES_PATH,
        file = "Shit_particles",
        name = "Shit_particles",
        position = new Vector3(position.x, position.y, 0),
        scale = new Vector3(1f, 1f, 1f),
        component = typeof(ShitObjectComponent)
      };

      var particleObject = new BaseObject();
      particleObject.CreateObject(objectParams);
    }

    private static void UpdateScores()
    {
      scoresCount++;

      if (scoresCount % 5 == 0)
      {
        fallingSpeed++;
      }
    }

    private static void RemoveAllObstacles()
    {
      while (obstacleList.Count > 0)
      {
        var obstacle = obstacleList[0];

        obstacleList.Remove(obstacle);
        obstacle.DestroyObject();
      }
    }
  }
}