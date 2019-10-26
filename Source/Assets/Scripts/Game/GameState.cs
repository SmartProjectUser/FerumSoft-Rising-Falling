using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Project
{
  [Serializable]
  public class GameState
  {
    public static GameState inst;

    public GameState() {

    }

    public static void SaveState()
    {
      PrepareForSave();

      BinaryFormatter binaryFormatter = new BinaryFormatter();

      string filePath = Application.persistentDataPath + "/" + "save.bin";

      FileStream fileStream = new FileStream(filePath, FileMode.Create);

      binaryFormatter.Serialize(fileStream, inst);

      fileStream.Close();
    }

    public static void LoadState()
    {
      string filePath = Application.persistentDataPath + "/" + "save.bin";

      if (File.Exists(filePath))
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(filePath, FileMode.Open);

        inst = binaryFormatter.Deserialize(fileStream) as GameState;

        fileStream.Close();
      }
      else
      {
        inst = new GameState();
      }
    }

    public static void ResetState()
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();

      string filePath = Application.persistentDataPath + "/" + "save.bin";

      FileStream fileStream = new FileStream(filePath, FileMode.Create);

      binaryFormatter.Serialize(fileStream, new GameState());

      fileStream.Close();
    }

    public static void SaveTestState()
    {
      //ResetState();
      SaveState();
    }

    private static void PrepareForSave()
    {

    }
  }
}