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

    public static readonly Dictionary<GameCharacters, string> CharacterNameMap = new Dictionary<GameCharacters, string>
    {
        { GameCharacters.Anne, "Anne" },
        { GameCharacters.AnneDefeated, "Anne" },
        { GameCharacters.Rob, "Rob" },
        { GameCharacters.Millie, "Millie" },
        { GameCharacters.Emma, "Emma" },
        { GameCharacters.RobAngry, "Rob" }
    };

    public static string GetRealName(GameCharacters name) 
    {
        return CharacterNameMap[name] ?? "Unknown"; 
    }
}
