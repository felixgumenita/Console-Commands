Create Command
=============  
+ Create ConsoleCommand variable and name it. For example;
    + ConsoleCommand TEST; (for string type)
    + ConsoleCommand<int> LOAD_SCENE; (for int type)
+ On "Start()" function assign (id, description and type) of command. For example
    + TEST = new ConsoleCommand("test", "Testing the command.", "test", ()=> { Debug.Log("Test"); });
    + LOAD_SCENE<int> = new ConsoleCommand("load_scene", "Testing load scene command.", "load_scene <int>", (x)=> { Debug.Log($"Load Scene: {x}"); });
+ Assing the command to commandList. For example;
    commandList = new List<object>
        {
            LOAD_SCENE,
            TEST
        };


 ConsoleCommandController.cs
=============
| Private  | Description |
| ----------------- | ---------------- |
| KeyCode _key  | Toggle key for Enable or Disable Console GUI. |
| string input  | String that written on TextField. |
| bool consoleEnable  | Check is GUI is Enable or Disable.|
| List<object> commandList  | List of all Commands. |

 ConsoleCommandBase
=============
| string Variables  | Description |
| ----------------- | ---------------- |
| _id  | ID of command. |
| _description  | Description of command. |
| _form  | Form of command. |

 ConsoleCommand and ConsoleCommand (T1)
=============
| Action  | Description |
| ----------------- | ---------------- |
| command  | Event that Invoking when input and ID is match. |
