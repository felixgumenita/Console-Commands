using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsoleCommandController : MonoBehaviour
{
    [SerializeField] KeyCode _key = KeyCode.A;
    [SerializeField] string input;
    bool consoleEnable;
    public List<object> commandList;

    ConsoleCommand<int> LOAD_SCENE;
    ConsoleCommand TEST;

    void Start()
    {
        LOAD_SCENE = new ConsoleCommand<int>("load_scene", "Load scene from build index.", "load_scene <scene-index", (x) =>
        {
            Debug.Log($"Scen Load : {x}");
        });
        TEST = new ConsoleCommand("test", "Load scene from build index.", "load_scene <scene-index", () =>
        {
            Debug.Log($"Test");
        });

        commandList = new List<object>
        {
            LOAD_SCENE,
            TEST
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(_key)) consoleEnable = !consoleEnable;
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
