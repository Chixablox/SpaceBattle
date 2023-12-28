namespace SpaceBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class MacroCommandTests2
{
    private MacroCommand _macroCommand;
    private string _stringDependency;
    private Mock<SpaceBattle.Lib.ICommand> checkFuelCommand = new Mock<SpaceBattle.Lib.ICommand>();
    private Mock<SpaceBattle.Lib.ICommand> burnFuelCommand = new Mock<SpaceBattle.Lib.ICommand>();
    private Mock<SpaceBattle.Lib.ICommand> moveCommand = new Mock<SpaceBattle.Lib.ICommand>();
    public MacroCommandTests2(){

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.MacroCommand.Move",
        (object[] args) =>
        {
            return new string[] {"Game.Command.CheckFuel","Game.Command.BurnFuel","Game.Command.Move"};
        }
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.MacroCommand.Builder",
        (object[] args) =>
        {   
            var dependency = (string)args[0];
            var cmdArray = BuildCommandsArray.DependencyHandling(dependency);
            var macroCommand = new MacroCommand(cmdArray);
            return macroCommand;
        }
        ).Execute();
    }

    [Given(@"зависимость с названием (.*)")]
    public void ДопустимЗависимостьСНазванием(string stringDependency)
    {
        checkFuelCommand.Setup(cmd => cmd.Execute()).Callback(()=> {}).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.CheckFuel",
        (object[] args) =>
        {
            return  checkFuelCommand.Object;
        }
        ).Execute();

        burnFuelCommand.Setup(cmd => cmd.Execute()).Callback(()=> {}).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.BurnFuel",
        (object[] args) =>
        {
            return burnFuelCommand.Object;
        }
        ).Execute();
        
        moveCommand.Setup(cmd => cmd.Execute()).Callback(()=> {}).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.Move",
        (object[] args) =>
        {
            return moveCommand.Object;
        }
        ).Execute();

        _stringDependency = stringDependency;
    }

    [Given(@"известно, что одна из команд не выполнится")]
    public void ДопустимИзвестноЧтоОднаИзКомандНеВыполнится()
    {
        burnFuelCommand.Setup(cmd => cmd.Execute()).Throws<Exception>();
    }

    [When("макрокоманда составляется")]
    public void КогдаMакрокомандаCоставляется()
    {
        _macroCommand = (MacroCommand)IoC.Resolve<Lib.ICommand>("Game.MacroCommand.Builder", _stringDependency);
    }

    [Then(@"макрокоманда успешно выполняется")]
    public void ТоМакрокомандаУспешноВыполняется()
    {
        _macroCommand.Execute();
        Mock.Verify(checkFuelCommand, burnFuelCommand, moveCommand);
    }

    [Then(@"выкидывается исключение")]
    public void ТоВыкидываетсяИсключение()
    {
        Assert.Throws<Exception>(() => _macroCommand.Execute());
    }
}

