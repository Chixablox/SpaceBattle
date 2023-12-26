namespace SpaveBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using TechTalk.SpecFlow;
using System.Collections;

[Binding]
public class ExceptionHandleTests
{
    private ExceptionHandle _exceptionHandle = new();
    private readonly MoveCommand _cmd = new(new Mock<IMovable>().Object);
    private readonly SetPosEx _ex = new("");
    private readonly IndexOutOfRangeException _ex2 = new("");
    private readonly Mock<SpaceBattle.Lib.ICommand> _handler1 = new();
    private readonly Mock<SpaceBattle.Lib.ICommand> _handler2 = new();
    private readonly Mock<SpaceBattle.Lib.ICommand> _handler3 = new();
    private readonly Mock<SpaceBattle.Lib.ICommand> _handler4 = new();

    public ExceptionHandleTests(){

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Exception.GetExceptionTree",
        (object[] args) =>
        {
            return new Hashtable(){
                {"MoveCommand", new Hashtable(){
                    {"SetPosEx", _handler1},
                    {"*", _handler2}
                }
                },
                {"*", new Hashtable(){
                    {"IndexOutOfRangeException", _handler3},
                    {"*", _handler4}
                }
                }
            };

        }
        ).Execute();
    }

    [Given(@"во время выполнения команды возникло исключение")]
    public void ДопустимВоВремяВыполненияКомандыВозниклоИсключение()
    {
        _handler1.Setup(cmd=>cmd.Execute()).Verifiable();
        _handler2.Setup(cmd=>cmd.Execute()).Verifiable();
        _handler3.Setup(cmd=>cmd.Execute()).Verifiable();
        _handler4.Setup(cmd=>cmd.Execute()).Verifiable();
    }

    [When("исключение обрабатывается")]
    public void КогдаИсключениеОбрабатывается()
    {
        var handler = _exceptionHandle.ExceptionHandler(_cmd, _ex);
        handler.Execute();
    }

    [Then(@"срабатывает первый обработчик")]
    public void ТоCрабатываетПервыйОбработчик()
    {
        Mock.Verify(_handler1);
    }

    [Then(@"срабатывает второй обработчик")]
    public void ТоCрабатываетВторойОбработчик()
    {
        Mock.Verify(_handler2);
    }

    [Then(@"срабатывает третий обработчик")]
    public void ТоCрабатываетТретийОбработчик()
    {
        Mock.Verify(_handler3);
    }

    [Then(@"срабатывает четвёртый обработчик")]
    public void ТоCрабатываетЧетвёртыйОбработчик()
    {
        Mock.Verify(_handler4);
    }
}

public class SetPosEx : Exception
{
    public SetPosEx(string message)
        : base(message) { }
}

