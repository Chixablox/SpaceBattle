using Hwdtech;

namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly IOrder _order;

    public StartCommand (IOrder order)
    {
        _order = order;
    }

    public void Execute()
    {

    }
}
