using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object(); 
    private static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting) 
            {
                return null;
            }
            lock (_lock) 
            {
                if (null == _instance)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof(T)).Length > 1) 
                    {
                        return _instance;  
                    }

                    if (null == _instance)   
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T).ToString();

                        DontDestroyOnLoad(singleton); 
                    }
                }
                return _instance;
            }
        }
    }

    public void OnDestroy() 
    {
        applicationIsQuitting = true;
    }
}
