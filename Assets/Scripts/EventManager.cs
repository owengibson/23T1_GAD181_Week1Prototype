using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class EventManager : MonoBehaviour
    {
        public delegate void TimerDelegate(float time);
        public static TimerDelegate GoodPelletEaten;
        public static TimerDelegate BadPelletEaten;

        public delegate void VoidDelegate();
        public static VoidDelegate OnPelletDestroy;
    }
}