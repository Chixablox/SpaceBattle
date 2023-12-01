namespace SpaceBattle.Lib;

public interface IOrder
{
    public IUObject Target {get;}
    public string Command {get;}
    public IDictionary<string, object> initialValues {get;}
}
