namespace SpaceBattle.Lib;

public class Vector
{
    public int x { get; set; }
    public int y { get; set; }

    public Vector(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.x + v2.x, v1.y + v2.y);
    }
}
