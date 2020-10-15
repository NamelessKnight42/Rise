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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Message_Manager.instance.GetIsRelive())
        {
            if(Input.GetMouseButtonDown(0))
            {
                int i = 1;
                while (i < 100) i++;
                par = Message_Manager.instance.GetReliveTo();

            }
            if(par!=null)
                this.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow=par.transform;
        }
       
    }
}
