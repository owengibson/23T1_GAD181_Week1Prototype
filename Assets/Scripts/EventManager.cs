using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenSecondsToDie
{
    public class EventManager : MonoBehaviour
    {
        
        public static Action<Player> HeartEaten;
        public static Action<Player> PoisonEaten;
        public static Action<Player> SkullEaten;

        public static Action OnHeartDestroy;
        public static Action OnPoisonDestroy;
        public static Action GameOver;
        public static Action OnTwoPlayersConnected;
        
        public static Action<string> OnPelletEaten;

        public static Action<string, int> OnLeaderboardSubmit;
    }
}