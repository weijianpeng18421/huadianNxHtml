using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WindowMod : MonoBehaviour
{
    //public Button btnSmallScreen;
    //public enum appStyle
    //{
    //    FullScreen,
    //    WindowedFullScreen,
    //    Windowed,
    //    WindowedWithoutBorder
    //}
    //public enum zDepth
    //{
    //    Normal,
    //    Top,
    //    TopMost
    //}
    private const uint SWP_SHOWWINDOW = 64u;
    private const int GWL_STYLE = -16;
    private const int WS_BORDER = 1;
    private const int GWL_EXSTYLE = -20;
    private const int WS_CAPTION = 12582912;
    private const int WS_POPUP = 8388608;
    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;
    //public WindowMod.appStyle AppWindowStyle = WindowMod.appStyle.WindowedFullScreen;
    //public WindowMod.zDepth ScreenDepth;
    private int windowLeft = 0;
    private int windowTop = 0;
    private int windowWidth = 450;
    private int windowHeight = 130;
    private Rect screenPosition;
    private IntPtr HWND_TOP = new IntPtr(0);
    private IntPtr HWND_TOPMOST = new IntPtr(-1);
    private IntPtr HWND_NORMAL = new IntPtr(-2);
    private int Xscreen;
    private int Yscreen;
    private int i;
    private float timer = 0;
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hPos, int x, int y, int cx, int cy, uint nflags);
    [DllImport("User32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("User32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("User32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int dwNewLong);
    [DllImport("User32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wP, IntPtr IP);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hChild);
    [DllImport("User32.dll")]
    public static extern IntPtr GetSystemMetrics(int nIndex);

    private void Awake()
    {
        Screen.SetResolution(600, 350, false);
        //btnSmallScreen.onClick.AddListener(OnFullScreenClick);
        this.Xscreen = (int)WindowMod.GetSystemMetrics(0);
        this.Yscreen = (int)WindowMod.GetSystemMetrics(1);
        // Screen.SetResolution(this.windowWidth, this.windowWidth, false);
        // this.screenPosition = new Rect((this.Xscreen - (float)this.windowWidth), (this.Yscreen - (float)this.windowHeight), (float)this.windowWidth, (float)this.windowHeight);
        this.screenPosition = new Rect(0, this.Yscreen-(float)this.windowHeight, this.Xscreen, (float)this.windowHeight);

    }
    private void FixedUpdate()
    {

    }
    private void Start()
    {
        Invoke("SwitchFullScreen", 0.1f);
    }
    private void SwitchFullScreen()
    {
        WindowMod.SetWindowLong(WindowMod.GetForegroundWindow(), -16, 369164288);
        WindowMod.SetWindowPos(WindowMod.GetForegroundWindow(), this.HWND_TOPMOST, (int)this.screenPosition.x, (int)this.screenPosition.y, (int)this.screenPosition.width, (int)this.screenPosition.height, 64u);

    }
}


