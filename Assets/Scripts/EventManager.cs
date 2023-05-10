using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSTD
{
    public class EventManager : MonoBehaviour
    {
        public delegate void FloatDelegate(float time);
        public static FloatDelegate HeartEaten;
        public static FloatDelegate BadPelletEaten;

        public delegate void VoidDelegate();
        public static VoidDelegate OnHeartDestroy;
        public static VoidDelegate OnPoisonDestroy;
        public static VoidDelegate GameOver;

        public delegate void StringDelegate(string str);
        public static StringDelegate OnPelletEaten;

        public delegate void LeaderboardDelegate(string name, int score);
        public static LeaderboardDelegate OnLeaderboardSubmit;
    }
}