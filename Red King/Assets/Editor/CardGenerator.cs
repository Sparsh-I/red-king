using System.IO;
using System.Linq;
using Cards;
using UnityEditor;
using UnityEngine;

namespace Editor {
    public class CardGenerator : MonoBehaviour
    {
        [MenuItem("Tools/Generate Card Deck")]
        private static void GenerateDeck()
        {
            const string folderPath = "Assets/Cards";
            const string spritePath = "Assets/Sprites";
            
            if (!AssetDatabase.IsValidFolder(folderPath)) 
                AssetDatabase.CreateFolder("Assets", "Cards");
            
            foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit)))
            {
                var sheetPath = $"{spritePath}/{suit}.png";

                var slices = AssetDatabase.LoadAllAssetsAtPath(sheetPath)
                    .OfType<Sprite>()
                    .OrderBy(s => SliceIndex(s.name))
                    .ToList();
                
                if (slices.Count == 0)
                {
                    Debug.LogError($"No sprites found at {sheetPath}. Check the file name and that the Sprite Mode is Multiple.");
                    continue;
                }

                if (slices.Count != 13) 
                    Debug.LogWarning($"{sheetPath} has {slices.Count} slices, expected 13.");

                for (var rank = 0; rank < 13; rank++)
                {
                    var cardName = $"{suit}_{rank}";
                    var assetPath = $"{folderPath}/{cardName}.asset";

                    var card = AssetDatabase.LoadAssetAtPath<CardData>(assetPath);
                    var isNew = card == null;
                    if (isNew) card = ScriptableObject.CreateInstance<CardData>();

                    card.suit = suit;
                    card.rank = rank + 1;

                    if (rank < slices.Count) card.cardSprite = slices[rank];
                    else Debug.LogWarning($"No slice for {cardName} in {sheetPath}");
                    
                    if (isNew) AssetDatabase.CreateAsset(card, assetPath);
                    else EditorUtility.SetDirty(card);
                }
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("52-card deck generated with sprites!");
        }

        private static int SliceIndex(string spriteName)
        {
            var i = spriteName.LastIndexOf('_');
            return i >= 0 && int.TryParse(spriteName.Substring(i+1), out var n) ? n : int.MaxValue; 
        }
    }
}