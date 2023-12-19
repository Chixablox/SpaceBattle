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

        var cmds = new List<ICommand>();

        stringCmds.ToList().ForEach(sCmd => cmds.Add(IoC.Resolve<ICommand>(sCmd)));

        var arrayCmds = cmds.ToArray();

        return arrayCmds;
    }
}

