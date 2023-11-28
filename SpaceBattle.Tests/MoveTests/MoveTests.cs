using Moq;
using SpaceBattle.Lib;
using TechTalk.SpecFlow;

namespace SpaceBattleTests;
[Binding]
public class MoveTest
{
    private readonly Mock<IMovable> _movable = new Mock<IMovable>();
    private MoveCommand _move;

    [When("происходит прямолинейное равномерное движение без деформации")]
    public void КогдаПроисходитПрямолинейноеРавномерноеДвижениеБезДеформации()
    {
        _move = new MoveCommand(_movable.Object);
    }

    [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
    public void ДопустимКосмическийКорабльНаходитсяВТочкеПространстваСКоординатами(int x, int y)
    {
        _movable.SetupGet(m => m.Position).Returns(new Vector(new int[] {x, y}));
    }

    [Given(@"космический корабль, положение в пространстве которого невозможно определить")]
    public void ДопустимКосмическийКорабльПоложениеВПространствеКоторогоНевозможноОпределить()
    {
        _movable.SetupGet(m => m.Position).Throws<Exception>();
    }

    [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
    public void ДопустимИмеетМгновеннуюСкорость(int x, int y)
    {
        _movable.SetupGet(m => m.Velocity).Returns(new Vector(new int[] {x, y}));
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
        _move.Execute();
        _movable.VerifySet(m => m.Position = It.Is<Vector>(p => p.coord[0] == a && p.coord[1] == b));
    }

    [Then(@"возникает ошибка Exception")]
    public void ТоВозникаетОшибкаException()
    {
        Assert.Throws<Exception>(() => _move.Execute());

    }
}
