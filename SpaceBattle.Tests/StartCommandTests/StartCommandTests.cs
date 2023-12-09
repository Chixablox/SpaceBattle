namespace SpaveBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using TechTalk.SpecFlow;

public class ActionCommand : SpaceBattle.Lib.ICommand
{
    private readonly Action _action;
    public ActionCommand(Action action) => _action = action;
    public void Execute()
    {
        _action();
    }
}

[Binding]
public class StartMoveCommandTests
{
    private readonly Mock<IOrder> _order = new Mock<IOrder>();
    private StartCommand _startMove;
    private Queue<SpaceBattle.Lib.ICommand> _queue = new Queue<SpaceBattle.Lib.ICommand>();
    private Mock<IQueue> _qMock = new Mock<IQueue>();

    [When("приказ обрабатывается")]
    public void КогдаПриказОбрабатывается()
    {
        _startMove.Execute();
    }

    [Given(@"отдан приказ на движение космического корабля, начальная позиция корабля \((.*), (.*)\) и мнгоновенная скорсоть корабля \((.*), (.*)\)")]
    public void ДопустимОтданПриказНаДвижениеКосмическогоКорабляНачальнаяПозицияКорабляИМнгоновеннаяСкорсотьКорабля(int x, int y, int dx, int dy)
    {
        var order = new Mock<IOrder>();
        var queue = new Mock<IQueue>();
        var initialValues = new Dictionary<string, object>
        {
            { "Position", new Vector(new int[] { x, y }) },
            { "Velocity", new Vector(new int[] { dx, dy }) }
        };
        var uObject = new Mock<IUObject>();
        var dictionaryForUObject = new Dictionary<string, object>();
        var cmd = "Move";
        
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Object.SetProperty",
        (object[] args) =>
        {
            var order = (IUObject)args[0];
            var key = (string)args[1];
            var value = args[2];

            order.SetProperty(key, value);
            return new object();
        }
        ).Execute();

        var moveCommand = new Mock<SpaceBattle.Lib.ICommand>();
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.Move",
        (object[] args) =>
        {
            var order = (IUObject)args[0];
            var pos = (Vector)order.GetProperty("Position");
            var vel = (Vector)order.GetProperty("Velocity");
            var movable = new Mock<IMovable>();
            movable.SetupGet(m => m.Position).Returns(pos);
            movable.SetupGet(m => m.Velocity).Returns(vel);
            var move = new MoveCommand(movable.Object);
            return move;
        }
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Queue",
        (object[] args) =>
        {
            return queue.Object;
        }
        ).Execute();

        order.SetupGet(order => order.Target).Returns(uObject.Object);
        order.SetupGet(order => order.Command).Returns(cmd);
        order.SetupGet(order => order.InitialValues).Returns(initialValues);

        uObject.Setup(uObject => uObject.SetProperty(It.IsAny<string>(), It.IsAny<object>())).Callback<string, object>(dictionaryForUObject.Add);

        queue.Setup(queue => queue.Add(It.IsAny<SpaceBattle.Lib.ICommand>())).Callback(_queue.Enqueue);

        _startMove = new StartCommand(order.Object);
    }

    [Then(@"команада, отданная игровому объекту, успешно добалвяется в очередь")]
    public void ТоКоманадаОтданнаяИгровомуОбъектуУспешноДобалвяетсяВОчередь()
    {
        Assert.NotEmpty(_queue);
    }

    [Then(@"возникает ошибка Exception")]
    public void ТоВозникаетОшибкаException()
    {
        Assert.Throws<Exception>(() => _startMove.Execute());
    }
}
