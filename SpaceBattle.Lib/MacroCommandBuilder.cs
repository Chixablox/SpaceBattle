using Hwdtech;

namespace SpaceBattle.Lib;

public class MacroCommandBuilder : ICommand
{
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.MacroCommand.Builder",
        (object[] args) =>
        {   
            var dependency = (string)args[0];
            var stringCmds = IoC.Resolve<string[]>(dependency);

            var cmds = new ICommand[stringCmds.Length];

            var i = 0;

            stringCmds.ToList().ForEach(sCmd =>
            {
                cmds[i] = IoC.Resolve<ICommand>(sCmd);
                i++;
            });

            var macroCommand = new MacroCommand(cmds);
            return macroCommand;
        }
        ).Execute();
    }
}

