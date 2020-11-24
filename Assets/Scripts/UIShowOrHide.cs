using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShowOrHide : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Button btnExit;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //显示或者隐藏关闭按钮
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerEventData, results);
            if (results[0].gameObject.name == "showArea" || results[0].gameObject.name == "btnExit")
            {
                btnExit.gameObject.SetActive(true);
            }
            else
            {
                btnExit.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("进入UI");
        //print(eventData.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("离开UI");
        //print(eventData.ToString());
    }
}
