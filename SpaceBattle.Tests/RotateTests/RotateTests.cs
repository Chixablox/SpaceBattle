using Moq;
using SpaceBattle.Lib;
using TechTalk.SpecFlow;

namespace SpaceBattleTests;
[Binding]
public class RotateTest
{
    private readonly Mock<IRotatable> _rotatable;
    private Action commandExecutionLambda;
    public RotateTest()
    {
        _rotatable = new Mock<IRotatable>();
        commandExecutionLambda = () => { };
    }

    [Given(@"космический корабль имеет угол наклона (.*) град к оси OX")]
    public void ДопустимКосмическийКорабльИмеетУголНаклона(int x)
    {
        _rotatable.SetupGet(r => r.Angle).Returns(new Angle(x/45, 8));
    }

    [Given(@"имеет мгновенную угловую скорость (.*) град")]
    public void ДопустимИмеетМгновеннуюУгловуюСкорость(int x)
    {
        _rotatable.SetupGet(r => r.Angle_Velocity).Returns(new Angle(x/45, 8));
    }

    [Given(@"мгновенную угловую скорость невозможно определить")]
    public void ДопустимМгновеннуюУгловуюСкоростьНевозможноОпределить()
    {
        _rotatable.SetupGet(r => r.Angle_Velocity).Throws<Exception>();
    }

    [Given(@"космический корабль, угол наклона которого невозможно определить")]
    public void ДопустимКосмическийКорабльУголНаклонаКоторогоНевозможноОпределить()
    {
        _rotatable.SetupGet(r => r.Angle).Throws<Exception>();
    }

    [Given(@"невозможно изменить угол наклона к оси OX космического корабля")]
    public void ДопустимНевозможноИзменитьУголНаклонаКОсиOXКосмическогоКорабля()
    {
        _rotatable.SetupGet(r => r.Angle).Throws<Exception>();
    }

    [When("происходит вращение вокруг собственной оси")]
    public void КогдаПроисходитВращениеВокругСобственнойОси()
    {
        var rc = new RotateCommand(_rotatable.Object);
        commandExecutionLambda = () => rc.Execute();
    }

    [Then(@"угол наклона космического корабля к оси OX составляет (.*) град")]
    public void ТоУголНаклонаКосмическогоКорабляКОсиOXСоставляет(int x)
    {
        commandExecutionLambda();
        _rotatable.VerifySet(r => r.Angle = It.Is<Angle>(p => p.d == x/45 && p.n == 8));
    }

    [Then(@"возникает ошибка Exception")]
    public void ТоВозникаетОшибкаException()
    {
        Assert.Throws<Exception>(() => commandExecutionLambda());

    }
}