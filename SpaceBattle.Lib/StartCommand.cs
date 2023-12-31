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
        _order.InitialValues.ToList().ForEach(value => IoC.Resolve<object>("Game.Object.SetProperty", _order.Target, value.Key, value.Value));
        var cmd = IoC.Resolve<ICommand>("Game.Command." + _order.Command, _order.Target);
        var inj = IoC.Resolve<ICommand>("Game.Command.Inject", cmd);
        IoC.Resolve<object>("Game.Object.SetProperty", _order.Target, "Game.Command." + _order.Command, inj);
        IoC.Resolve<IQueue>("Game.Queue").Add(inj);
    }
}

