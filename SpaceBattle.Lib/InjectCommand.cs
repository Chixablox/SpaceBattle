namespace SpaceBattle.Lib;

public class InjectCommand : ICommand, IInjectable
{
    private ICommand _cmd;
    public InjectCommand(ICommand cmd)
    {
        _cmd = cmd;
    }

    public void Inject(ICommand command)
    {
        _cmd = command;
    }

    public void Execute()
    {
        _cmd.Execute();
    }
}

