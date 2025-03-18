using System.IO;
using System.Xml;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Assets.Sources.Editor
{
    public class PostBuildProcessor
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.iOS)
            {
                string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
                XmlDocument plist = new XmlDocument();
                plist.Load(plistPath);

                XmlNode root = plist.DocumentElement;
                XmlNode dict = root.SelectSingleNode("dict");

                XmlElement keyElement = plist.CreateElement("key");
                keyElement.InnerText = "API_HOST";

                XmlElement valueElement = plist.CreateElement("string");
                valueElement.InnerText = "api.example.com";

                dict.AppendChild(keyElement);
                dict.AppendChild(valueElement);

                plist.Save(plistPath);

                Debug.Log("API_HOST created");
            }
        }
    }
}