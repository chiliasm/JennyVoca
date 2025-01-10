using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class BaseMonoSingleton<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
    {
        static T _instance;
        static object _lock = new();
        static bool _isQuit = false;

        public static T Instance
        {
            get
            {
                if (_isQuit)
                    return null;
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindAnyObjectByType(typeof(T));
                        if (_instance == null)
                        {
                            var o = new GameObject();
                            _instance = o.AddComponent<T>();
                        }

                        _instance.OnCreateSingleton();
                        _instance.gameObject.name = "(singleton) " + typeof(T).ToString();
                        _instance.transform.parent = null;
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                    return _instance;
                }
            }
        }        
    }
}
