using Moq;
using SpaceBattle.Lib;
using TechTalk.SpecFlow;

namespace SpaceBattleTests;
[Binding]
public class RotateTest
{
    private readonly Mock<IRotatable> _rotatable = new Mock<IRotatable>();
    private RotateCommand _rotate;

    [Given(@"космический корабль имеет угол наклона (.*) град к оси OX")]
    public void ДопустимКосмическийКорабльИмеетУголНаклона(int x)
    {
        _rotatable.SetupGet(r => r.Angle).Returns(new Angle(x/45, 8));
    }

    [Given(@"имеет мгновенную угловую скорость (.*) град")]
    public void ДопустимИмеетМгновеннуюУгловуюСкорость(int x)
    {
        _rotatable.SetupGet(r => r.AngleVelocity).Returns(new Angle(x/45, 8));
    }

    [Given(@"мгновенную угловую скорость невозможно определить")]
    public void ДопустимМгновеннуюУгловуюСкоростьНевозможноОпределить()
    {
        _rotatable.SetupGet(r => r.AngleVelocity).Throws<Exception>();
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
        _rotate = new RotateCommand(_rotatable.Object);
    }

    [Then(@"угол наклона космического корабля к оси OX составляет (.*) град")]
    public void ТоУголНаклонаКосмическогоКорабляКОсиOXСоставляет(int x)
    {
        _rotate.Execute();
        _rotatable.VerifySet(r => r.Angle = It.Is<Angle>(p => p.dir == x/45 && p.num == 8));
    }

    [Then(@"возникает ошибка Exception")]
    public void ТоВозникаетОшибкаException()
    {
        Assert.Throws<Exception>(() => _rotate.Execute());

    }
}