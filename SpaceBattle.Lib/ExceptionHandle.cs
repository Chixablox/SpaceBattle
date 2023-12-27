using System.Collections;
using Hwdtech;

namespace SpaceBattle.Lib;

public class ExceptionHandle
{
    public ICommand ExceptionHandler(ICommand cmd, Exception exc)
    {
        var handleTree = IoC.Resolve<Hashtable>("Game.Exception.GetExceptionTree");
        var cmdTree = (Hashtable?)handleTree.GetValueOrDefaultValue(cmd.GetType().ToString());
        var handle = (ICommand?)cmdTree.GetValueOrDefaultValue(exc.GetType().ToString());
        Console.WriteLine(cmd.GetType().ToString());
        return handle;
    }
}

