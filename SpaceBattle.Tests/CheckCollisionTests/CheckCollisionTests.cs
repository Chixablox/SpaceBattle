namespace SpaveBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using TechTalk.SpecFlow;
using System.Collections;

[Binding]
public class CheckCollisionTests
{
    private CheckCollisionCommand _checkCollision;
    private readonly Mock<IUObject> _uObject1 = new();
    private readonly Mock<IUObject> _uObject2 = new();

    public CheckCollisionTests(){

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Object.GetProperty",
        (object[] args) =>
        {
            var obj = (IUObject)args[0];
            var key = (string)args[1];

            var vec = (Vector)obj.GetProperty(key);
            return vec;
        }
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Collision.Process",
        (object[] args) =>
        {
            var tree = (Hashtable)args[0];
            var exp = (Action)tree["Exception"];
            exp();

            return new object();
        }
        ).Execute();

        Action defaultAction = () => throw new Exception("Collision!");
        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Collision.GetTree",
        (object[] args) =>
        {
            return new Hashtable(){
                {1, new Hashtable(){
                    {1, new Hashtable(){
                        {0, new Hashtable(){
                            {1, new Hashtable(){
                                {"Exception", defaultAction}
                            }
                            },
                            {2, new Hashtable(){
                                {"Exception", defaultAction}
                            }
                            }
                        }
                        }   
                    }
                    }
                }
                }
            };

        }
        ).Execute();
    }

    [Given(@"первый космический корабль, находящийся в точке пространства с координатами \((.*), (.*)\) и имеющий мгновенную скорость \((.*), (.*)\)")]
    public void ДопустимПервыйКосмическийКорабльНаходящийсяВТочкеПространстваСКоординатамиИИмеющийМгновеннуюСкорость(int x1, int y1, int dx1, int dy1){

        var dictionaryForFirstUObject = new Dictionary<string, object>
        {
            { "Position", new Vector(new int[] { x1, y1 }) },
            { "Velocity", new Vector(new int[] { dx1, dy1 }) }
        };
        
        _uObject1.Setup(uObject => uObject.GetProperty(It.IsAny<string>())).Returns((string key) => dictionaryForFirstUObject[key]);

    }

    [Given(@"второй космический корабль, находящийся в точке пространства с координатами \((.*), (.*)\) и имеющий мгновенную скорость \((.*), (.*)\)")]
    public void ДопустимВторойКосмическийКорабльНаходящийсяВТочкеПространстваСКоординатамиИИмеющийМгновеннуюСкорость(int x2, int y2, int dx2, int dy2){

        var dictionaryForSecondUObject = new Dictionary<string, object>
        {
            { "Position", new Vector(new int[] { x2, y2 }) },
            { "Velocity", new Vector(new int[] { dx2, dy2 }) }
        };
        
        _uObject2.Setup(uObject => uObject.GetProperty(It.IsAny<string>())).Returns((string key) => dictionaryForSecondUObject[key]);

    }

    [Given(@"свойства космических кораблей не могут быть прочитаны")]
    public void ДопустимСвойстваКосмическихКораблейНеМогутБытьПрочитаны(){

        _uObject2.Setup(uObject => uObject.GetProperty(It.IsAny<string>())).Returns((string key) => throw new Exception());

    }


    [When("происходит проверка на коллизию")]
    public void КогдаПроисходитПроверкаНаКоллизию()
    {
        _checkCollision = new CheckCollisionCommand(_uObject1.Object, _uObject2.Object);
    }
    
    [Then(@"возникает ошибка Collisison")]
    public void ТоВозникаетОшибкаCollisison()
    {
        var exception = Assert.Throws<Exception>(() => _checkCollision.Execute());
        Assert.Equal("Collision!", exception.Message);
    }

    [Then(@"возникает ошибка NotCollision")]
    public void ТоВозникаетОшибкаNotCollision()
    {
        var exception = Assert.Throws<Exception>(() => _checkCollision.Execute());
        Assert.Equal("NotCollision!", exception.Message);
    }

    [Then(@"возникает ошибка")]
    public void ТоВозникаетОшибка()
    {
        Assert.Throws<Exception>(() => _checkCollision.Execute());
    }
}

