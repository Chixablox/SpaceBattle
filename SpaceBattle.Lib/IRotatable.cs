namespace SpaceBattle.Lib;

public interface IRotatable
{
    public Angle Angle { get; set; }
    public Angle Angle_Velocity { get; }
}
