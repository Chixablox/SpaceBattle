namespace SpaceBattle.Lib;

public static class IDictionaryExp
{
    public static TValue? GetValueOrDefault<TKey,TValue>
        (this IDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var ret) ? ret : default;
}

