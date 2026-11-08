using UnityEngine;
using UnityEditor;
using Cards;

[CustomEditor(typeof(DeckController))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DeckController deck = (DeckController)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Populate Deck")) deck.ShuffleDeck();
    }
}
