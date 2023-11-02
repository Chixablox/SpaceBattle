using Moq;
using SpaceBattle.Lib;
using TechTalk.SpecFlow;

namespace SpaceBattleTests;
[Binding]
public class MoveTest
{
    private readonly Mock<IMovable> _movable;
    private Action commandExecutionLambda;
    public MoveTest()
    {
        _movable = new Mock<IMovable>();
        commandExecutionLambda = () => { };
    }

    [When("происходит прямолинейное равномерное движение без деформации")]
    public void КогдаПроисходитПрямолинейноеРавномерноеДвижениеБезДеформации()
    {
        var mc = new MoveCommand(_movable.Object);
        commandExecutionLambda = () => mc.Execute();
    }

    [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
    public void ДопустимКосмическийКорабльНаходитсяВТочкеПространстваСКоординатами(int x, int y)
    {
        _movable.SetupGet(m => m.Position).Returns(new Vector(x, y));
    }

    [Given(@"космический корабль, положение в пространстве которого невозможно определить")]
    public void ДопустимКосмическийКорабльПоложениеВПространствеКоторогоНевозможноОпределить()
    {
        _movable.SetupGet(m => m.Position).Throws<Exception>();
    }

    [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
    public void ДопустимИмеетМгновеннуюСкорость(int x, int y)
    {
        _movable.SetupGet(m => m.Velocity).Returns(new Vector(x, y));
    }

    [Given(@"скорость корабля определить невозможно")]
    public void ДопустимСкоростьКорабляОпределитьНевозможно()
    {
        _movable.SetupGet(m => m.Velocity).Throws<Exception>();
    }

    [Given(@"изменить положение в пространстве космического корабля невозможно")]
    public void ДопустимИзменитьПоложениеВПространствеКосмическогоКорабляНевозможно()
    {
        _movable.SetupGet(m => m.Velocity).Throws<Exception>();
    }

    [Then(@"космический корабль перемещается в точку пространства с координатами \((.*), (.*)\)")]
    public void ТоКосмическийКорабльПеремещаетсяВТочкуПространстваСКоординатами(int a, int b)
    {
        commandExecutionLambda();
        _movable.VerifySet(m => m.Position = It.Is<Vector>(p => p.x == a && p.y == b));
    }

    [Then(@"возникает ошибка Exception")]
    public void ТоВозникаетОшибкаException()
    {
        Assert.Throws<Exception>(() => commandExecutionLambda());

    }
}
