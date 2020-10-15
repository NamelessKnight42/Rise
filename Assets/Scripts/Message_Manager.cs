using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static Message_Manager instance { get; private set; }

    private static bool isRelive = false;
    private static GameObject reliveTo = null;
    private static bool haveReliveTo = false;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIsRelive(bool a)
    {
        isRelive = a;
    }

    public bool GetIsRelive()
    {
        return isRelive;
    }
    public void SetReliveTo(GameObject gameobject)
    {
        Debug.Log("load"+gameobject.name);
        reliveTo = gameobject;
        
    }
    public GameObject GetReliveTo()
    {
        return reliveTo;
    }
    public void SetHaveReliveTo(bool a)
    {
        haveReliveTo = a;
    }
    public bool GetHaveReliveTo()
    {
        return haveReliveTo;
    }

}
