using System.IO;
using Cards;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CardGenerator : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            GenerateDeck();
        }

        private static void GenerateDeck()
        {
            const string folderPath = "Assets/Cards";
            
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit)))
            {
                for (var rank = 0; rank < 13; rank++)
                {
                    var card = ScriptableObject.CreateInstance<CardData>();
                    card.suit = suit;
                    card.rank = rank;

                    var cardName = $"{suit}_{rank}";
                    
                    var sprite = Resources.Load<Sprite>($"Sprites/{cardName}");
                    if (sprite != null) card.cardSprite = sprite;
                    else Debug.LogWarning($"Missing sprite for {cardName}");
                    
                    AssetDatabase.CreateAsset(card, folderPath + $"/{cardName}.asset");
                }
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("52-card deck generated with sprites!");
        }
    }
}
