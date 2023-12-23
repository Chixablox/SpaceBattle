using System.Collections;
using Hwdtech;

namespace SpaceBattle.Lib;

public class ExceptionHandle
{
    public static ICommand? ExceptionHandler(ICommand cmd, Exception exc)
    {
        var handleTree = IoC.Resolve<IDictionary<string, IDictionary>>("Game.Exception.GetExceptionTree");
        var cmdTree = (IDictionary<string, ICommand>?)handleTree.GetValueOrDefault(cmd.GetType().ToString());
        var handle = cmdTree.GetValueOrDefault(exc.GetType().ToString());
        return handle;
    }
}

