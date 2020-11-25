using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.UI;
using System.Runtime.InteropServices;//调用外部库，需要引用命名空间
using System;

public class NetworkManager : MonoBehaviour
{
    public Text RealLoad;//实施负荷
    public Text InternetPower;//上网电量
    public Text DayPower;//日发电量

    string format = "#0.00";

    //string api = "https://way.jd.com/he/freeweather?city=yinchuan&appkey=8fe3ef8df98a3348b3b46351acd5b674";
    string api = "http://192.168.1.7:8080/huadian/main";
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RepeateRequest", 0f, 5f);
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetRequest()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(api);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Messagebox.MessageBox(IntPtr.Zero, uwr.error, "网络请求错误", 0);
        }
        else
        {
            string result = uwr.downloadHandler.text;
            Debug.Log("result  : " + result);
            //result = result.Replace("[", "");

            JsonData data = JsonMapper.ToObject(result);


            //数据展示
            for (int i = 0; i < data.Count; i++)
            {
                if ((string)data[i]["paraName"] == "WDGF.WDGF:NBQ01CE40001")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    RealLoad.text = temp.ToString(format);

                }
                if ((string)data[i]["paraName"] == "WDGF.WDGF:NBQ01CE40002")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    InternetPower.text = temp.ToString(format);
                }
                if ((string)data[i]["paraName"] == "WDGF.WDGF:NBQ01CE40003")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    DayPower.text = temp.ToString(format);
                }
            }
        }
    }

    void RepeateRequest()
    {
        StartCoroutine(GetRequest());
    }
    //调用windows弹窗
    public class Messagebox
    {
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, String message, String title, int type);
    }
}
