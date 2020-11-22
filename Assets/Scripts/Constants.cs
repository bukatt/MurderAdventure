using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class Layers
    {
        public static readonly int player2 = LayerMask.NameToLayer("Player2");
        public static readonly int player1 = LayerMask.NameToLayer("Player1");
        public static readonly int shield = LayerMask.NameToLayer("Shield");
        public static readonly int bullet1 = LayerMask.NameToLayer("Bullet1");
        public static readonly int bullet2 = LayerMask.NameToLayer("Bullet2");
        public static readonly int map = LayerMask.NameToLayer("Map");

    }

    public static class Tags
    {
        public static readonly string player = "Player";
        public static readonly string inspectorUI = "InspectorUI";
        public static readonly string clueUI = "ClueUI";
        public static readonly string notClueUI = "NotClueUI";
        public static readonly string virtualCamera = "VirtualCamera";
        public static readonly string nameEnterField = "NameEnterField";
        public static readonly string nameEnterButton = "NameEnterButton";
        public static readonly string uiManager = "UIManager";
        public static readonly string lobbyUIManager = "LobbyUIManager";
        public static readonly string gameUIManager = "GameUIManager";
        public static readonly string itemContainer = "ItemContainer";
        public static readonly string chatUIManager = "ChatUIManager";
        public static readonly string chatManager = "ChatManager";
    }

    public static class GameStates
    {
        public static readonly string pregameCountdown = "Pre Game Countdown";
        public static readonly string inGame = "In Game";
        public static readonly string betweenRounds = "Between Rounds";
        public static readonly string gameOver = "Game Over";
        public static readonly string gameStarting = "Game Starting";
    }

    public static class Colors
    {
        [ColorUsage(true, true)]
        public static readonly Color team1Color = new Color(0f, 133f, 191f, 255f) / 40;

        [ColorUsage(true, true)]
        public static readonly Color team2Color = new Color(191f, 67f, 0f, 255f) / 40;

        public static readonly Color uiColor = new Color(28f, 191f, 0f, 255f);
    }

    public static class Roles
    {
        public static readonly string murderer = "Murderer";
        public static readonly string innocent = "Innocent";
    }

    public static class WeaponObjects
    {
        public static readonly string pistol = "Pistol";
        public static readonly WeaponObject pistolObject = Resources.Load("Pistol") as WeaponObject;

        public static readonly string knife = "Knife";
        public static readonly WeaponObject knifeObject = Resources.Load("Knife") as WeaponObject;

        public static readonly Dictionary<string, WeaponObject> uiWeaponsDict = new Dictionary<string, WeaponObject>()
        {
            {pistol, pistolObject },
            {knife, knifeObject }
        };
    }

    public static class Items
    {
        public static readonly ItemObject[] itemObjects = Resources.LoadAll<ItemObject>("Items");

        public static readonly Dictionary<string, ItemObject> itemDict = InitializeDict();

        private static  Dictionary<string, ItemObject> InitializeDict() {
            Dictionary<string, ItemObject> tempDict = new Dictionary<string, ItemObject>();
            foreach(ItemObject io in itemObjects)
            {
                tempDict.Add(io.name, io);
            }
            return tempDict;
        }

    }

        
}
