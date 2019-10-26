using UnityEngine;

using System;
using System.Collections;
using System.Xml;

namespace Project
{
  public class Langs
  {
    private static Hashtable strings;

    private static void ParseLang()
    {
      /*XmlTextReader reader = new XmlTextReader(Const.LANGUAGES_PATH + Const.LANGUAGE + ".xml");

      while (reader.Read()){
        if(reader.Name == Const.LANGUAGE && reader.NodeType == XmlNodeType.Element){
          while(reader.Read()){
            if(reader.MoveToAttribute("name")){
              string key = reader.Value;
              string val = string.Empty;

              while(reader.Read () && reader.NodeType == XmlNodeType.Text){
                val = reader.Value;
              }

              strings.Add(key, val);
            }
          }
        }

      }*/

      string langXml = Resources.Load(Const.LANGUAGES_PATH + Const.LANGUAGE).ToString();

      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(langXml);

      XmlElement xRoot = xDoc.DocumentElement;

      foreach (XmlNode xnode in xRoot.ChildNodes)
      {
        if (xnode != null)
        {
          if (xnode.Attributes != null)
          {
            XmlNode attr = xnode.Attributes.GetNamedItem("name");

            if (attr != null)
            {
              string key = attr.Value;
              string val = xnode.InnerText;

              strings.Add(key, val);
            }
          }
        }
      }
    }

    public static string GetString(string key)
    {
      if (strings is null)
      {
        strings = new Hashtable();

        ParseLang();
      }

      string val = string.Empty;

      foreach (DictionaryEntry entry in strings)
      {
        if (entry.Key.ToString() == key)
        {
          val = entry.Value.ToString();

          break;
        }
      }

      if (val == string.Empty)
      {
        Funcs.Throw("String not found");
      }

      return val;
    }
  }
}