using System;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Commands
{
    public Commands()
    {
        Console.Instance.AddCommand("help", CmdHelp, "Show available commands");
        Console.Instance.AddCommand("clear", CmdClear, "Clear console");
        Console.Instance.AddCommand("scene", CmdScene, "Load scene");
        Console.Instance.AddCommand("timescale", CmdTimeScale, "Game time scale (default - 1.0)");
        Console.Instance.AddCommand("gravity", CmdGravity, "Game gravity (default - -9.81)");
        Console.Instance.AddCommand("fov", CmdFov, "Field of view of main camera (default - 60)");
        Console.Instance.AddCommand("windowmode", CmdWindowMode, "Window mode (0 - fullscreen, 1 - windowed, 2 - exclusive full screen, 3 - maximized window)");
        Console.Instance.AddCommand("resolution", CmdResolution, "Window resolution (width, heigth)");
        Console.Instance.AddCommand("fpslimit", CmdFpsLimit, "FPS limit (default - -1)");
        Console.Instance.AddCommand("quit", CmdQuit, "Quit game");
    }

    private void CmdHelp(string[] args)
    {
        foreach (var c in Console.Instance.m_Commands)
        {
            Console.Instance.Write("  {0,-15} {1}\n", Colors.Pink + c.Key, Colors.Yellow + Console.Instance.m_CommandDescriptions[c.Key]);
        }
    }

    private void CmdClear(string[] args)
    {
        Console.Instance.Clear();
    }

    private void CmdTimeScale(string[] args)
    {
        float[] num = Array.ConvertAll(args, s => float.Parse(s));

        if (num[0] < 0.0f)
        {
            Console.Instance.Write($"{Colors.Red}The value cannot be less than 0.0");
            return;
        }

        Console.Instance.Write($"{Colors.Green}Time scale changed to {num[0]} from {Time.timeScale}");
        Time.timeScale = num[0];
    }

    private void CmdGravity(string[] args)
    {
        float[] num = Array.ConvertAll(args, s => float.Parse(s));

        Console.Instance.Write($"{Colors.Green}Gravity changed to {num[0]} from {Physics.gravity.y}");
        Physics.gravity = new Vector3(0, num[0], 0);
    }

    private void CmdScene(string[] args)
    {
        int[] num = Array.ConvertAll(args, s => int.Parse(s));

        if (num[0] < -1)
        {
            Console.Instance.Write($"{Colors.Red}The value cannot be less than -1");
            return;
        }

        Console.Instance.Write($"{Colors.Green}Scene changed to {num[0]} from {SceneManager.GetActiveScene().buildIndex}");
        SceneManager.LoadScene(num[0]);
    }

    private void CmdFov(string[] args)
    {
        int[] num = Array.ConvertAll(args, s => int.Parse(s));

        if (num[0] < 1 || num[0] > 180)
        {
            Console.Instance.Write($"{Colors.Red}The value cannot be less than 1 or greater than 180");
            return;
        }

        Console.Instance.Write($"{Colors.Green}FOV changed to {num[0]} from {Camera.main.fieldOfView}");
        Camera.main.fieldOfView = num[0];
    }

    private void CmdWindowMode(string[] args)
    {
        int[] num = Array.ConvertAll(args, s => int.Parse(s));

        if (num[0] < 0 || num[0] > 3)
        {
            Console.Instance.Write($"{Colors.Red}The value cannot be less than 0 or greater than 3");
            return;
        }

        switch (num[0])
        {
            case 0: Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow); break;
            case 1: Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.Windowed); break;
            case 2: Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen); break;
            case 3: Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.MaximizedWindow); break;
        }
    }

    private void CmdResolution(string[] args)
    {
        int[] num = Array.ConvertAll(args, s => int.Parse(s));

        if (num[0] < 1 || num[0] > 9999 || num[1] < 1 || num[1] > 9999)
        {
            Console.Instance.Write($"{Colors.Red}The value cannot be less than 1 or greater than 9999");
            return;
        }

        Console.Instance.Write($"{Colors.Green}Window resolution changed to {num[0]}x{num[1]} from {Screen.currentResolution.width}x{Screen.currentResolution.height}");
        if (Screen.fullScreen)
        {
            Screen.SetResolution(num[0], num[1], true);
        }
        else
        {
            Screen.SetResolution(num[0], num[1], false);
        }
    }

    private void CmdFpsLimit(string[] args)
    {
        int[] num = Array.ConvertAll(args, s => int.Parse(s));

        if (num[0] < -1)
        {
            Console.Instance.Write($"{Colors.Red}The value cannot be less than -1");
            return;
        }

        Console.Instance.Write($"{Colors.Green}FPS limit changed to {num[0]} from {Application.targetFrameRate}");
        Application.targetFrameRate = num[0];
    }

    private void CmdQuit(string[] args)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
