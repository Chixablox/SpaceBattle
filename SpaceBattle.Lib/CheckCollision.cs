using Hwdtech;

namespace SpaceBattle.Lib;

public class CheckCollision : ICommand
{
    private readonly IUObject _obj1;
    private readonly IUObject _obj2;

    public CheckCollision(IUObject obj1, IUObject obj2)
    {
        _obj1 = obj1;
        _obj2 = obj2;
    }

    public void Execute()
    {
        var pos1 = IoC.Resolve<Vector>("Game.Object.GetProperty", _obj1, "Position");
        var vel1 = IoC.Resolve<Vector>("Game.Object.GetProperty", _obj1, "Velocity");
        var pos2 = IoC.Resolve<Vector>("Game.Object.GetProperty", _obj2, "Position");
        var vel2 = IoC.Resolve<Vector>("Game.Object.GetProperty", _obj2, "Velocity");

        var newCoord = new int[4];
        newCoord[0] = pos2.coord[0] - pos1.coord[0];
        newCoord[1] = pos2.coord[1] - pos1.coord[1];
        newCoord[2] = vel2.coord[0] - vel1.coord[0];
        newCoord[3] = vel2.coord[1] - pos1.coord[1];

        Action defaultAction = ()=> throw new Exception("Collision!");

        var tree = IoC.Resolve<IDictionary<object, object>>("Game.Collision.GetTree");

        newCoord.ToList().ForEach(c => tree = (IDictionary<object, object>)tree[c]);

        IoC.Resolve<Exception>("Game.Collision.Process");
    }
}

