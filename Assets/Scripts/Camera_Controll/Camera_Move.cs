using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    /**
     * 用于控制镜头的转移，通过判断当前相机的父物体tag是否为"isControlled"来切换相机
     * *
     */

    private GameObject par;
    
    // Start is called before the first frame update
    void Start()
    {
        par = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(par.tag=="isControlled")
        {
            this.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority=5;
        }
        else
        {
            this.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 2;
        }
    }
}
