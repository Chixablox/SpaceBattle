public class Vector
{
    int x { get; set; }
    int y { get; set; }

    public Vector(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector
        {
            x = v1.x + v2.x;
            y = v1.y + v2.y;
        };
    }
}
