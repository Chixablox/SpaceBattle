using System.Collections;
using Hwdtech;

namespace SpaceBattle.Lib;

public class ExceptionHandle
{
    public ICommand ExceptionHandler(ICommand cmd, Exception exp)
    {
        var handleTree = IoC.Resolve<IDictionary>("Game.Exception.GetExceptionTree");
        var handle = 1;
        return handle;
    }
}

