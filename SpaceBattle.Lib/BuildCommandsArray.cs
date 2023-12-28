using Hwdtech;

namespace SpaceBattle.Lib;

public class BuildCommandsArray
{
    public static ICommand[] DependencyHandling(string stringDependency)
    {
        var stringCmds = IoC.Resolve<string[]>(stringDependency);

        var cmds = new ICommand[stringCmds.Length];

        var i = 0;

        stringCmds.ToList().ForEach(sCmd =>
        {
            cmds[i] = IoC.Resolve<ICommand>(sCmd);
            i++;
        });

        return cmds;
    }
}

