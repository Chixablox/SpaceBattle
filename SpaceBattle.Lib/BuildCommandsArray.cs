using Hwdtech;

namespace SpaceBattle.Lib;

public class BuildCommandsArray
{
    private readonly string _stringDependency;

    public BuildCommandsArray(string stringDependency)
    {
         _stringDependency = stringDependency;
    }

    public ICommand[] DependencyHandling()
    {
        var stringCmds = IoC.Resolve<string[]>(_stringDependency);

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

