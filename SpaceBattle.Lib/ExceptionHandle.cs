using System.Collections;
using Hwdtech;

namespace SpaceBattle.Lib;

public class ExceptionHandle
{
    public ICommand ExceptionHandler(ICommand cmd, Exception ex)
    {
        var handleTree = IoC.Resolve<Hashtable>("Game.Exception.GetExceptionTree");
        var cmdTree = (Hashtable?)handleTree.GetValueOrDefaultValue(cmd.GetType());
        var handle = (ICommand?)cmdTree.GetValueOrDefaultValue(ex.GetType());
        return handle;
    }
}

