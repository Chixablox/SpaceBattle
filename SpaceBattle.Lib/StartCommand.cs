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
        var injCmd = (ICommand)IoC.Resolve<IInjectable>("Game.Command.Inject." + _order.Command, _order.Target);
        IoC.Resolve<object>("Game.Object.SetProperty", _order.Target, "Game.Command.Inject" + _order.Command, injCmd);
        IoC.Resolve<IQueue>("Game.Queue").Add(injCmd);
    }
}

