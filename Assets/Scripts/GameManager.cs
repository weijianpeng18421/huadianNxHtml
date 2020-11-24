using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button btnExitSystem;
    // Start is called before the first frame update
    void Start()
    {
        btnExitSystem.onClick.AddListener(OnExitSystem);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnExitSystem()
    {
        Application.Quit();
    }
}
