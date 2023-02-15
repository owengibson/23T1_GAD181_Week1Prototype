using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class EventManager : MonoBehaviour
    {
        public delegate void TimerDelegate(float time);
        public static TimerDelegate GoodPelletEaten;
        public static TimerDelegate BadPelletEaten;

        public delegate void VoidDelegate();
        public static VoidDelegate OnGoodPelletDestroy;
        public static VoidDelegate OnBadPelletDestroy;
        public static VoidDelegate GameOver;
    }
}