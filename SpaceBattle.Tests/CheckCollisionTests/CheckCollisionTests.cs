namespace SpaveBattle.Tests;

using SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class CheckCollisionTests
{
    private CheckCollision _checkCollision;
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

            obj.GetProperty(key);
            return new object();
        }
        ).Execute();

        IoC.Resolve<Hwdtech.ICommand>(
        "IoC.Register",
        "Game.Collision.Process",
        (object[] args) =>
        {
            throw new Exception("Collision");
        }
        ).Execute();

    }









}

