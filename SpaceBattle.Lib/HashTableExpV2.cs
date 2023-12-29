using System.Collections;

namespace SpaceBattle.Lib;

public static class HashTableExp2
{
    public static object GetValueOrDefaultValue(this Hashtable hashtable, object key)
    {
        return hashtable.ContainsKey(key) ? hashtable[key] : hashtable["*"];
    } 
}

