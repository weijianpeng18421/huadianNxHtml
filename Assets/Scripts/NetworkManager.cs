using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetRequest("https://way.jd.com/jisuapi/weather?city=安顺&cityid=111&citycode=101260301&appkey=8fe3ef8df98a3348b3b46351acd5b674"));
        StartCoroutine(GetRequest("http://192.168.1.7:8080/huadian/main"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("TTTTTT  : " + uwr.downloadHandler.text);
            JsonData jd = JsonMapper.ToObject(uwr.downloadHandler.text);
            Debug.Log((string)jd["code"]);
        }
    }
}
