namespace SpaveBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class StartMoveCommandTests
{
    private readonly Mock<IOrder> _order = new();
    private StartCommand _startMove;
    private readonly Queue<SpaceBattle.Lib.ICommand> _queueReal = new();
    private readonly Mock<IQueue> _queue = new();

    private readonly Mock<IUObject> _uObject = new();

    public StartMoveCommandTests(){

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Object.SetProperty",
        (object[] args) =>
        {
            var target = (IUObject)args[0];
            var key = (string)args[1];
            var value = args[2];

            target.SetProperty(key, value);
            return new object();
        }
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.Move",
        (object[] args) =>
        {
            var target = (IUObject)args[0];
            var pos = (Vector)target.GetProperty("Position");
            var vel = (Vector)target.GetProperty("Velocity");
            var movable = new Mock<IMovable>();
            movable.SetupGet(m => m.Position).Returns(pos);
            movable.SetupGet(m => m.Velocity).Returns(vel);
            var move = new MoveCommand(movable.Object);
            return move;
        }
        ).Execute();

        var cmd = new Mock<SpaceBattle.Lib.ICommand>().Object;
        var injectCommand = new InjectCommand(cmd);
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Command.Inject",
        (object[] args) =>
        {
            var cmd = (SpaceBattle.Lib.ICommand)args[0];
            injectCommand.Inject(cmd);
            return injectCommand;
        }
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Queue",
        (object[] args) =>
        {
            return _queue.Object;
        }
        ).Execute();

    }

    [Given(@"отдан приказ на движение космического корабля, начальная позиция корабля \((.*), (.*)\) и мнгоновенная скорсоть корабля \((.*), (.*)\)")]
    public void ДопустимОтданПриказНаДвижениеКосмическогоКорабляНачальнаяПозицияКорабляИМнгоновеннаяСкорсотьКорабля(int x, int y, int dx, int dy)
    {
        
        var initialValues = new Dictionary<string, object>
        {
            { "Position", new Vector(new int[] { x, y }) },
            { "Velocity", new Vector(new int[] { dx, dy }) }
        };
        var dictionaryForUObject = new Dictionary<string, object>();
        var cmd = "Move";
        
        _order.SetupGet(order => order.Target).Returns(_uObject.Object);
        _order.SetupGet(order => order.Command).Returns(cmd);
        _order.SetupGet(order => order.InitialValues).Returns(initialValues);

        _uObject.Setup(uObject => uObject.SetProperty(It.IsAny<string>(), It.IsAny<object>())).Callback<string, object>(dictionaryForUObject.Add);
        _uObject.Setup(uObject => uObject.GetProperty(It.IsAny<string>())).Returns((string key) => dictionaryForUObject[key]);

        _queue.Setup(queue => queue.Add(It.IsAny<SpaceBattle.Lib.ICommand>())).Callback(_queueReal.Enqueue);
        _queue.Setup(queue => queue.Take()).Returns(()=> _queueReal.Dequeue());
    }

    [Given(@"космическому кораблю невозмозжно установить свойства")]
    public void ДопустимКосмическомуКораблюНевозмозжноУстановитьСвойства()
    {
        _uObject.Setup(uObject => uObject.SetProperty(It.IsAny<string>(), It.IsAny<object>())).Throws<Exception>();
    }

    [Given(@"свойства космического корабля невозможно прочитать")]
    public void ДопустимСвойстваКосмическогоКорабляНевозможноПрочитать()
    {
        _uObject.Setup(uObject => uObject.GetProperty(It.IsAny<string>())).Throws<Exception>();
    }

    [Given(@"команду нельзя добавить в очередь")]
    public void ДопустимКомандуНельзяДобавитьВОчередь()
    {
        _queue.Setup(queue => queue.Add(It.IsAny<SpaceBattle.Lib.ICommand>())).Throws<Exception>();
    }

    [When("приказ обрабатывается")]
    public void КогдаПриказОбрабатывается()
    {
        _startMove = new StartCommand(_order.Object);
    }

    [Then(@"команада, отданная игровому объекту, успешно добалвяется в очередь")]
    public void ТоКоманадаОтданнаяИгровомуОбъектуУспешноДобалвяетсяВОчередь()
    {
        _startMove.Execute();
        Assert.NotEmpty(_queueReal);
    }

    [Then(@"команада, отданная игровому объекту, успешно добалвяется в очередь, достаётся и выпоняется")]
    public void ТоКоманадаОтданнаяИгровомуОбъектуУспешноДобалвяетсяВОчередьДостаётсяИВыпоняется()
    {
        _startMove.Execute();
        Assert.NotEmpty(_queueReal);
        _queue.Object.Take().Execute();
        Assert.Empty(_queueReal);
    }

    [Then(@"возникает ошибка")]
    public void ТоВозникаетОшибка()
    {
        Assert.Throws<Exception>(() => _startMove.Execute());
    }
}

