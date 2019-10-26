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

    public static float moveSpeed = 6f;
    public static float fallingSpeed = 2f;
    public static float respawnTime = 1f;

    public bool moveLeft = false;
    public bool moveRight = false;

    public static int scoresCount;

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
      GenerateObstacleObject();
      CheckObjectAlive();
    }

    public void OnStart()
    {
      fallingObject = new FallingObject();
      fallingObject.CreateFallingObject();

      Audio.PlaySound(backgroundObject.gameObject, Const.SOUNDS_PATH + "8bit_sound", true);
    }

    public void OnUpdate()
    {
      if (fallingObject.gameObject.transform.position.x > -5.5f && moveLeft)
      {
        fallingObject.gameObject.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
      }

      if (fallingObject.gameObject.transform.position.x < 5.5f && moveRight)
      {
        fallingObject.gameObject.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
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

      var obstacleObject = new ObstacleObject(obstacleRespawnIndex);
      obstacleObject.CreateObstacleObject();

      obstacleList.Add(obstacleObject);
    }

    public void CheckObjectAlive()
    {
      for (int i = 0; i < obstacleList.Count; i++)
      {
        var obstacle = obstacleList[i];

        var distance = Vector3.Distance(
          fallingObjectPlace.gameObject.transform.position,
          obstacle.gameObject.transform.position
        );

        if (distance > 25f)
        {
          obstacleList.Remove(obstacle);
          obstacle.DestroyObject();

          break;
        }
      }
    }

    public void StopMoving()
    {
      moveLeft = false;
      moveRight = false;
    }

    public void RunMoveTimer()
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
        obstacleList.Remove(facedObstacle);
        facedObstacle.DestroyObject();

        UpdateScores();

        Audio.PlaySound(GameController.inst.fallingObject.gameObject, Const.SOUNDS_PATH + "explosion");
      }
    }

    public static void UpdateScores()
    {
      scoresCount++;

      if (scoresCount % 5 == 0)
      {
        fallingSpeed++;
      }
    }
  }
}