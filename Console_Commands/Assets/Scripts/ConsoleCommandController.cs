using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System

public class ConsoleCommandController : MonoBehaviour
{
    string input;
    bool consoleEnable;
    public List<object> commandList;

    void Start()
    {

    }
    private void OnGUI()
    {
        if (!consoleEnable) return;

        float y = 0f;
        Event e = Event.current;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.SetNextControlName("input");

        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 25f), input);
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl() == "input")
        {
            HandleInput();
            input = "";
        }
    }

    void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            ConsoleCommandBase commandBase = commandList[i] as ConsoleCommandBase;

            if (input.Contains(commandBase.ID))
            {
                if (commandList[i] as ConsoleCommand != null)
                {
                    (commandList[i] as ConsoleCommand).Command();
                }
                else if (commandList[i] as ConsoleCommand<int> != null)
                {
                    (commandList[i] as ConsoleCommand<int>).Command(int.Parse(properties[1]));
                }
            }
        }
    }
}
