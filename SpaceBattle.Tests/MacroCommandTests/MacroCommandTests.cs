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
    private readonly Mock<IUObject> _uObject = new();
    private Mock<SpaceBattle.Lib.ICommand> checkFuelCommand = new();
    private Mock<SpaceBattle.Lib.ICommand> burnFuelCommand = new();
    private Mock<SpaceBattle.Lib.ICommand> moveCommand = new();
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

        checkFuelCommand.Setup(cmd => cmd.Execute()).Callback(()=> {}).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.CheckFuel",
        (object[] args) =>
        {
            var target = (IUObject)args[0];
            target.SetProperty("Game.Command.CheckFuel", checkFuelCommand.Object);
            return  checkFuelCommand.Object;
        }
        ).Execute();

        burnFuelCommand.Setup(cmd => cmd.Execute()).Callback(()=> {}).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.BurnFuel",
        (object[] args) =>
        {
            var target = (IUObject)args[0];
            target.SetProperty("Game.Command.BurnFuel", burnFuelCommand.Object);
            return burnFuelCommand.Object;
        }
        ).Execute();
        
        moveCommand.Setup(cmd => cmd.Execute()).Callback(()=> {}).Verifiable();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.Move",
        (object[] args) =>
        {
            var target = (IUObject)args[0];
            target.SetProperty("Game.Command.Move", moveCommand.Object);
            return moveCommand.Object;
        }
        ).Execute();

        new MacroCommandBuilder().Execute();
    }

    [Given(@"зависимость с названием (.*) и некий игровой объект")]
    public void ДопустимЗависимостьСНазваниемИНекийИгровойОбъект(string stringDependency)
    {
        var dictionaryForUObject = new Dictionary<string, object>();

        _uObject.Setup(uObject => uObject.SetProperty(It.IsAny<string>(), It.IsAny<object>())).Callback<string, object>(dictionaryForUObject.Add);
        _uObject.Setup(uObject => uObject.GetProperty(It.IsAny<string>())).Returns((string key) => dictionaryForUObject[key]);

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
        _macroCommand = (MacroCommand)IoC.Resolve<Lib.ICommand>("Game.MacroCommand.Builder", _stringDependency, _uObject.Object);
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

