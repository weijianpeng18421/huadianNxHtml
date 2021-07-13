using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.UI;
using System.Runtime.InteropServices;//调用外部库，需要引用命名空间
using System;

public class NetworkManager : MonoBehaviour
{
    public Text DayPower;//区域日发电量:
    public Text MonthPower;//区域月发电量:
    public Text YearPower;//区域年发电量:
    public Text AllPower;//区域总负荷:

    string format = "#0.00";

    // string api = "https://way.jd.com/he/freeweather?city=yinchuan&appkey=8fe3ef8df98a3348b3b46351acd5b674";
    string api = "http://10.141.88.205:8080/huadian/main";
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

        // Debug.Log("UnityWebRequest  : " + uwr);

        if (uwr.isNetworkError)
        {
            // Messagebox.MessageBox(IntPtr.Zero, uwr.error, "网络请求错误", 0);
        }
        else
        {
            string result = uwr.downloadHandler.text;

            //result = result.Replace("[", "");

            JsonData data = JsonMapper.ToObject(result);

            Debug.Log("data  : " + data);

            //数据展示
            for (int i = 0; i < data.Count; i++)
            {
                if ((string)data[i]["paraName"] == "CALC.NXXNY_RFDL_WS")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    DayPower.text = temp.ToString(format) + " 万kWh";
                }

                if ((string)data[i]["paraName"] == "CALC.NXXNY_YFDL_WS")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    MonthPower.text = temp.ToString(format) + " 万kWh";
                }

                if ((string)data[i]["paraName"] == "CALC.NXXNY_NFDL_WS")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    YearPower.text = temp.ToString(format) + " 万kWh";
                }

                if ((string)data[i]["paraName"] == "CALC.NXXNY:SSZFH")
                {
                    float temp = float.Parse(data[i]["value"].ToString());
                    AllPower.text = temp.ToString(format) + " 万kW";
                }

                // if ((string)data[i]["paraName"] == "WDGF.WDGF:NBQ12CE40003")
                // {
                //     float temp = float.Parse(data[i]["value"].ToString());
                //     DayPower.text = (temp * 0.0005f).ToString(format) + " kWh";
                //     MonthPower.text = temp.ToString(format) + " kWh";
                // }
            }
        }
    }

    void RepeateRequest()
    {
        Debug.Log("-----------开始调用");
        StartCoroutine(GetRequest());
    }
    //调用windows弹窗
    public class Messagebox
    {
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, String message, String title, int type);
    }
}
