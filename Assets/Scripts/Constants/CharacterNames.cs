using System.Collections.Generic;

public static class CharacterNames
{
    public const string Anne7yo = "Anne7yo";
    public const string Anne14yo = "Anne14yo";
    public const string Anne20yo = "Anne20yo";
    public const string AnneDefeated = "AnneDefeated";
    public const string Rob = "Rob";
    public const string Millie = "Millie";
    public const string Emma = "Emma";
    public const string RobAngry = "RobAngry";

    public static readonly Dictionary<string, string> CharacterNameMap = new Dictionary<string, string>
    {
        { Anne7yo, "Anne" },
        { Anne14yo, "Anne" },
        { Anne20yo, "Anne" },
        { AnneDefeated, "Anne" },
        { Rob, "Rob" },
        { Millie, "Millie" },
        { Emma, "Emma" },
        { RobAngry, "Rob" }
    };

    public static string GetRealName(string name) 
    {
        return CharacterNameMap[name] ?? "Unknown"; 
    }
}
