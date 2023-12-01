namespace SpaceBattle.Lib;

public class Angle
{
    public int dir { get; set; }
    public int num { get;} = 8;

    public Angle(int d)
    {
        this.dir = d;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle((a1.dir + a2.dir)%a1.num);
    }
}
