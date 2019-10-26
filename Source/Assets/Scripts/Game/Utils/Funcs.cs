using UnityEngine;

using System;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
  public class Funcs
  {
    public static string GetPriceFormatted(ulong price)
    {
      if (price > 999999999999)
      {
        return price.ToString("0,,,,.####T", CultureInfo.InvariantCulture);
      }
      else
      if (price > 999999999)
      {
        return price.ToString("0,,,.###B", CultureInfo.InvariantCulture);
      }
      else
      if (price > 999999)
      {
        return price.ToString("0,,.##M", CultureInfo.InvariantCulture);
      }
      else
      if (price > 999)
      {
        return price.ToString("0,.#K", CultureInfo.InvariantCulture);
      }
      else
      {
        return price.ToString(CultureInfo.InvariantCulture);
      }
    }

    public static IEnumerator Delay(float time, System.Action callback)
    {
      yield return new WaitForSeconds(time);

      callback();

      yield break;
    }

    public static void Log(object format, params object[] paramList)
    {
      StackTrace stackTrace = new StackTrace(true);

      StackFrame stackFrame = stackTrace.GetFrame(2);

      string log = string.Format("[{3}.{4}] == ",
                    stackFrame.GetFileName(),
                    stackFrame.GetFileLineNumber(),      // always reports 0
                    stackFrame.GetFileColumnNumber(),    // always reports 0
                    stackFrame.GetMethod().ReflectedType.Name,
                    stackFrame.GetMethod().Name);

      UnityEngine.Object newContext = null;

      if (paramList.Length > 0 && paramList[paramList.Length - 1] is UnityEngine.Object)
      {
        newContext = paramList[paramList.Length - 1] as UnityEngine.Object;
      }

      // UnityEngine.Object context = StackFrame.GetMethod().ReflectedType;

      if (format is string)
      {
        log += format as string;
        UnityEngine.Debug.Log(string.Format(log, paramList), newContext);
      }
      else
      {
        log += format.ToString();
        UnityEngine.Debug.Log(log, newContext);
      }
    }

    /*StartCoroutine(Funcs.WaitFor((input)=>{
      var unitObject = input as UnitObject;

      if(unitObject.objectComponent != null){
        return true;
      }

      return false;
    }, object, (returnResult)=>{

    }));*/

    public delegate T Wait<T>(T args);
    public static IEnumerator WaitFor<T>(Wait<T> exp, T args, System.Action<T> callback)
    {
      while (true)
      {
        T obj = exp(args);

        if (obj != null)
        {
          callback(obj);

          yield break;
        }

        yield return new WaitForSeconds(0.1f);
      }
    }

    public static GameObject GetRootMesh(GameObject targetObject)
    {
      Transform findTransform = targetObject.transform.parent;

      while (findTransform != null)
      {
        Transform parentTrans = findTransform.transform.parent;

        if (parentTrans != null)
        {
          findTransform = parentTrans;
        }
        else
        {
          break;
        }
      }

      return findTransform.gameObject;
    }

    public static GameObject GetSubMesh(GameObject targetObject, string name)
    {
      GameObject findObject;

      for (int i = 0; i < targetObject.transform.childCount; i++)
      {
        GameObject currentObject = targetObject.transform.GetChild(i).gameObject;

        findObject = currentObject;

        if (currentObject.name != name)
        {
          RecursFind(currentObject, name, ref findObject);
        }

        if (findObject.name == name)
        {
          return findObject;
        }
      }

      return null;
    }

    private static void RecursFind(GameObject targetObject, string name, ref GameObject returnObject)
    {
      for (int a = 0; a < targetObject.transform.childCount; a++)
      {
        GameObject currentObject = targetObject.transform.GetChild(a).gameObject;

        if (currentObject.name == name)
        {
          returnObject = currentObject;

          break;
        }

        RecursFind(currentObject, name, ref returnObject);
      }
    }

    public static Color HexToCol(string hex, float alpha = 1.0f)
    {
      if (hex != null)
      {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        byte a = 255; //assume fully visible unless specified in hex

        if (hex.Length == 8)
        {
          a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color(r, g, b, alpha);
      }

      return new Color();
    }

    public static string StripTags(string input)
    {
      string output;

      output = Regex.Replace(input, @"</?.+?>", string.Empty);
      output = Regex.Replace(output, @"<(.|\n)*?>", string.Empty);

      return output;
    }

    public static void Throw(string message)
    {
      throw new Exception(message);
    }

    public static int GenUniqueId(int size = 4)
    {
      int tm = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

      string sTm = tm.ToString();

      return Convert.ToInt32(sTm.Substring(sTm.Length - (sTm.Length - size), size));
    }

    public static string GetMd5(string input)
    {
      MD5 md5 = MD5.Create();

      byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));

      StringBuilder sBuilder = new StringBuilder();

      for (int i = 0; i < data.Length; i++)
      {
        sBuilder.Append(data[i].ToString("x2"));
      }

      return sBuilder.ToString();
    }

    public static Hashtable ObjectToHash(object obj)
    {
      Hashtable hash = new Hashtable();

      PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);

      foreach (PropertyDescriptor prop in props)
      {
        object val = prop.GetValue(obj);

        hash.Add(prop.Name, val);
      }

      return hash;
    }

    public static string EncodeString(string str)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }

    public static string DecodeString(string str)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(str));
    }
  }
}