
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class TransparentWindow : MonoBehaviour
{
  
    public Rect screenPosition;

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hwnd, int _nIndex);

    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, int dwFlags);

    const int SWP_SHOWWINDOW = 0x0040;
    const int GWL_EXSTYLE = -20;
    const int GWL_STYLE = -16;
    const int WS_CAPTION = 0x00C00000;
    const int WS_BORDER = 0x00800000;
    const int WS_EX_LAYERED = 0x80000;
    public const int LWA_ALPHA = 0x2;
    public const int LWA_COLORKEY = 0x1;

    private IntPtr handle;
    private hook.GlobalHook hook;
    void Start()
    {
        handle = GetForegroundWindow();
        SetWindowLong(handle, GWL_EXSTYLE, WS_EX_LAYERED);
        SetWindowLong(handle, GWL_STYLE, GetWindowLong(handle, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);
        SetWindowPos(handle, -1, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);

        //把黑色透明化，不工作
           SetLayeredWindowAttributes(handle, 0, 100, LWA_COLORKEY);

       // 把整个窗口透明化，工作
       // SetLayeredWindowAttributes(handle, 0, 100, LWA_ALPHA);

           //初始化钩子对象
           if (hook == null)
           {
               hook = new hook.GlobalHook();
             
           }
    }

    void LateUpdate()
    {

    }

    private void OnDestroy()
    {
        hook.Stop();
    }
}