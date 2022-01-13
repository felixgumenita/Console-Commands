using System;

public class ConsoleCommandBase
{
    private string _id;
    private string _description;
    private string _form;

    public string ID { get { return _id; } }
    public string Description { get { return _description; } }
    public string Form { get { return _form; } }

    public ConsoleCommandBase(string id, string descriptin, string form)
    {
        id = _id;
        descriptin = _description;
        form = _form;
    }
}

public class ConsoleCommand : ConsoleCommandBase
{
    private Action command;
    
    public ConsoleCommand(string id, string description, string form, Action command) : base(id, description, form)
    {
        this.command = command;
    }

    public void Command()
    {
        command.Invoke();
    }
}

public class ConsoleCommand<T1> : ConsoleCommandBase
{
    private Action<T1> command;
    public ConsoleCommand(string id, string description, string form, Action<T1> command) : base(id, description, form)
    {
        this.command = command;
    }

    public void Command(T1 value)
    {
        command.Invoke(value);
    }
}


