using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsoleCommandController : MonoBehaviour
{

    #region Enable Console Toggle Key
    [SerializeField] private KeyCode _key = KeyCode.A;
    #endregion

    #region Private
    private string input;
    private bool consoleEnable;
    private List<object> commandList;
    #endregion

    #region Commands
    ConsoleCommand<int> LOAD_SCENE;
    ConsoleCommand TEST;
    ConsoleCommand<int> SETSCORE;
    ConsoleCommand<int> SETCLICKPOINT;
    #endregion

    void Start()
    {
        //Commands
        // ConsoleCommand<type>(int, bool, list ex. leave empty for strings)
        // (<id for input> , <description for the command>, <form of id (load_scene (int))
        LOAD_SCENE = new ConsoleCommand<int>("load_scene", "Load scene from build index.", "load_scene <scene-index", (x) =>
        {
            Debug.Log($"Scene Load : {x}");
        });
        TEST = new ConsoleCommand("test", "Load scene from build index.", "load_scene <scene-index", () =>
        {
            Debug.Log($"Test");
        });
        SETSCORE = new ConsoleCommand<int>("set_score", "To set current score.", "set_score <int-score>", (x) =>
        {
            ClickController click = FindObjectOfType<ClickController>();
            click.Score = x;
        });
        SETCLICKPOINT = new ConsoleCommand<int>("set_clickPoint", "To set clickPoint", "set_clickPoint <int-clickPoint>", (x) =>
        {
            ClickController click = FindObjectOfType<ClickController>();
            click.ClickPoint = x;
        });

        //Command List
        commandList = new List<object>
        {
            LOAD_SCENE,
            TEST,
            SETSCORE,
            SETCLICKPOINT
        };
    }

    private void Update()
    {
        //Activate Console GUI
        if (Input.GetKeyDown(_key)) consoleEnable = !consoleEnable;
    }
    private void OnGUI()
    {
        //If false GUI is disable
        if (!consoleEnable) return;

        //GUI start height
        float y = 0f;

        
        Event e = Event.current;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.SetNextControlName("input");

        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 25f), input);

        //Check return key
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl() == "input")
        {
            HandleInput();
            input = "";
        }
    }
    private void HandleInput()
    {
        //Splite text for check type of Command
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            //Get CosoleCommand as Base
            ConsoleCommandBase commandBase = commandList[i] as ConsoleCommandBase;

            if (input.Contains(commandBase.ID))
            {
                //Check Command Type
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
