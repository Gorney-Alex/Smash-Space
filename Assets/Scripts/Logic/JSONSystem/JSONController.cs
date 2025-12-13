using UnityEngine;
using System.IO;

public static class JSONController
{
    public static T LoadJson<T>(string resourcePath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);

        if (textAsset == null)
        {
            Debug.LogError($"JSON not found in Resources at: {resourcePath}");
            return default;
        }

        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
