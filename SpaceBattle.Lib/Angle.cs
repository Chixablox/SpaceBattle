namespace SpaceBattle.Lib;

public class Angle
{
    int d { get; set; }
    int n { get; set; }

    public Angle(int d, int n)
    {
        this.d = d;
        this.n = n;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        return new Angle((a1.d + a2.d)%a1.n, a2.n);
    }
}
