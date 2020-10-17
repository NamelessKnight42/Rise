using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creat_Body : MonoBehaviour
{
    public GameObject[] deadBody;
    private float bodyTime = 1.5f;
    private float bodyTimeLen = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyTime < bodyTimeLen)
        {
            bodyTime += Time.deltaTime;
        }
        else
        {
            Init_Body();
            bodyTime = 0;
        }
    }

    private void Init_Body()
    {
        Vector2 bodyPi;
        bodyPi.y = this.transform.position.y + 15;
        int seed = (int)System.DateTime.Now.Ticks;
        UnityEngine.Random.InitState(seed);
        GameObject body = deadBody[UnityEngine.Random.Range(0, 4)];
        bodyPi.x = UnityEngine.Random.Range(-9f, 9f);
        Instantiate(body, bodyPi, Quaternion.identity);

    }
}
