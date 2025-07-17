using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;

/// <summary>
/// Create tile attributes assets through Unity Editor individually or in bulk from a JSON file
/// </summary>
public class TileAttributesEditor : EditorWindow
{
    [MenuItem("Assets/Create/Tile Attribute")]

    /// <summary>
    /// Create tile attributes asset in the project view from the "Create" menu
    /// </summary>
    public static void CreateTileAttributesAndSave()
    {
        TileAttributes asset = ScriptableObject.CreateInstance<TileAttributes>();
        SaveTileAttributes(asset, "Assets/NewTileAttributes.asset");

        // Highlight newly created asset in project window
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    static void SaveTileAttributes(TileAttributes tileAttributes, string filePath)
    {
        AssetDatabase.CreateAsset(tileAttributes, filePath);
        AssetDatabase.SaveAssets();
    }

    [Serializable]
    public class TileAttributesListJSON
    {
        public TileAttributesJSON[] attributes;
    }

    [Serializable]
    public class TileAttributesJSON
    {
        public string type;
        public int number;
        public int count;

        public string asset;

        public int order;
    }

    public TileAttributesListJSON tileAttributesList;

    [MenuItem("Window/Tile Attributes Editor")]
    static void Init()
    {
        GetWindow(typeof(TileAttributesEditor));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Load Attribute List from JSON", GUILayout.ExpandWidth(false)))
        {
            string path = EditorUtility.OpenFilePanel("Tile Attributes list JSON", "", "json");
            if (path.Length != 0)
            {
                var jsonString = File.ReadAllText(path);
                TileAttributesListJSON attrFile = JsonUtility.FromJson<TileAttributesListJSON>(jsonString);

                Debug.Log("Creating attribute assets...");
                SaveTileAttributes(attrFile.attributes);
            }
        }
    }

    void SaveTileAttributes(TileAttributesJSON[] attributesList)
    {
        foreach (TileAttributesJSON attributesJSON in attributesList)
        {
            TileAttributes attributes = ScriptableObject.CreateInstance<TileAttributes>();
            attributes.type = attributesJSON.type;
            attributes.number = attributesJSON.number;
            attributes.count = attributesJSON.count;
            attributes.order = attributesJSON.order;
            attributes.asset = attributesJSON.asset;

            string filePath = "Assets/Resources/TileAttributes/Tile" + attributes.type + attributes.number + ".asset";
            Debug.Log(filePath);
            SaveTileAttributes(attributes, filePath);
        }
    }

}


