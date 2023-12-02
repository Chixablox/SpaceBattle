using Hwdtech;

namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly IOrder order;

    public StartCommand (IOrder order)
    {
        this.order = order;
    }

    public void Execute()
    {
        
    }
}
