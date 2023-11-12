using Moq;
using SpaceBattle.Lib;
using TechTalk.SpecFlow;

namespace SpaceBattleTests;
[Binding]
public class RotateTest
{
    private readonly Mock<IRotatable> _rotatable = new Mock<IRotatable>();
    private RotateCommand _rotate;

    [Given(@"космический корабль находится в секторе (.*)")]
    public void ДопустимКосмическийКорабльНаходитсяВСекторе(int x)
    {
        _rotatable.SetupGet(r => r.Angle).Returns(new Angle(x));
    }

    [Given(@"имеет мгновенную угловую скорость (.*) сектор")]
    public void ДопустимИмеетМгновеннуюУгловуюСкорость(int x)
    {
        _rotatable.SetupGet(r => r.AngleVelocity).Returns(new Angle(x));
    }

    [Given(@"мгновенную угловую скорость невозможно определить")]
    public void ДопустимМгновеннуюУгловуюСкоростьНевозможноОпределить()
    {
        _rotatable.SetupGet(r => r.AngleVelocity).Throws<Exception>();
    }

    [Given(@"космический корабль, сектор которого невозможно определить")]
    public void ДопустимКосмическийКорабльСекторКоторогоНевозможноОпределить()
    {
        _rotatable.SetupGet(r => r.Angle).Throws<Exception>();
    }

    [Given(@"невозможно изменить сектор нахождения космического корабля")]
    public void ДопустимНевозможноИзменитьСекторНахожденияКосмическогоКорабля()
    {
        _rotatable.SetupGet(r => r.Angle).Throws<Exception>();
    }

    [When("происходит вращение вокруг собственной оси")]
    public void КогдаПроисходитВращениеВокругСобственнойОси()
    {
        _rotate = new RotateCommand(_rotatable.Object);
    }

    [Then(@"космический корабль находится в (.*) секторе")]
    public void ТоКосмическийКорабльНаходитсяВСекторе(int x)
    {
        _rotate.Execute();
        _rotatable.VerifySet(r => r.Angle = It.Is<Angle>(p => p.dir == x));
    }

    [Then(@"возникает ошибка Exception")]
    public void ТоВозникаетОшибкаException()
    {
        Assert.Throws<Exception>(() => _rotate.Execute());

    }
}