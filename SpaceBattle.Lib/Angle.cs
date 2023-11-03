namespace SpaceBattle.Lib;

public class Angle
{
    public int dir { get; set; }
    public int num { get; set; }

    public Angle(int d, int n)
    {
        this.dir = d;
        this.num = n;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle((a1.dir + a2.dir)%a1.num, a1.num);
    }
}
