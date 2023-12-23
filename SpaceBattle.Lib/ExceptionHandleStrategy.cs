using System.Collections;
using Hwdtech;

namespace SpaceBattle.Lib;

public class ExceptionHandle
{
    public ICommand? ExceptionHandler(ICommand cmd, Exception exc)
    {
        var handleTree = IoC.Resolve<IDictionary>("Game.Exception.GetExceptionTree");
        var cmdTree = (IDictionary?)handleTree[cmd.GetType().ToString()];
        var handle = (ICommand?)cmdTree[exc.GetType().ToString()];
        return handle;
    }
}

