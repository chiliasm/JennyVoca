using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class Log : BaseMonoBehaviour
    {
        public static void Msg(string msg, string header = "")
        {
            Debug.Log(msg);
        }

        public static void MsgWarning(string msg, string header = "")
        {
            Debug.LogWarning(msg);
        }

        public static void MsgError(string msg, string header = "")
        {
            Debug.LogError(msg);
        }
    }
}
