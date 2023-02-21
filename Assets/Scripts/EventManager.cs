using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class EventManager : MonoBehaviour
    {
        public delegate void FloatDelegate(float time);
        public static FloatDelegate GoodPelletEaten;
        public static FloatDelegate BadPelletEaten;

        public delegate void VoidDelegate();
        public static VoidDelegate OnGoodPelletDestroy;
        public static VoidDelegate OnBadPelletDestroy;
        public static VoidDelegate GameOver;

        public delegate void StringDelegate(string str);
        public static StringDelegate OnPelletEaten;
    }
}