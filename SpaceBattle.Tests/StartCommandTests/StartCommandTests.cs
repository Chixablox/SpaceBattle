namespace SpaveBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class ActionCommand : SpaceBattle.Lib.ICommand
{
    private readonly Action _action;
    public ActionCommand(Action action) => _action = action;
    public void Execute()
    {
        _action();
    }
}

public class StartMoveCommandTests
{

}
