using UnityEditor;
using UnityEngine;
using GameModel = Model.Game;
using TileModel = Model.Tile;

/// <summary>
/// Custom inspector that allows for inspection of the Game Model in play mode
/// </summary>
[CustomEditor(typeof(ModelAPI))]
public class GameModelInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Game Model");

        ModelAPI modelAPI = (ModelAPI)target;
        if (modelAPI.gameModel == null)
        {
            EditorGUILayout.LabelField("None");
            return;
        }

        GameModel.Save gameSave = modelAPI.gameModel.GetSave();
        EditorGUILayout.LabelField("Draw Pile");
        foreach (TileModel tile in gameSave.drawPile)
        {
            EditorGUILayout.LabelField(tile.GetNotation());
        }
    }
}
