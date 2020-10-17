using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Cam_Follow : MonoBehaviour
{
    public Text high;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject objectToFollow;

    public float speed = 2.0f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            //Debug.Log("1");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync("start", LoadSceneMode.Single);
        }
        high.text=transform.position.y.ToString();//更新高度
        if (Message_Manager.instance.GetIsRelive())
        {
            if (Input.GetMouseButtonDown(0))
            {
                int i = 1;
                while (i < 100) i++;
                objectToFollow = Message_Manager.instance.GetReliveTo();

            }
        }

        float interpolation = speed * Time.deltaTime;
        if (objectToFollow != null)
        {
            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

            this.transform.position = position;
        }
    }
}
