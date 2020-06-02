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
}
