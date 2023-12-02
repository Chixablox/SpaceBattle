namespace SpaceBattle.Lib;

public class Vector
{
    public int[] coord { get; set; }

    public Vector(int[] coord)
    {
        this.coord = coord;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.coord.Zip(v2.coord, (x, y) => x + y).ToArray());
    }
}