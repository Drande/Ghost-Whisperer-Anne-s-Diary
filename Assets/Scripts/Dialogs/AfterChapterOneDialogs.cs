public static class AfterChapterOneDialogs
{
    // Llamar después de pasar el minijuego y antes de pasar a la pantalla del segundo capítulo.
    public static Message[] Start = new Message[]
    {
        new Message(CharacterNames.Emma, "So... they didn't leave me."),
        new Message(CharacterNames.Anne, "No, it looks like they really wanted to find you."),
        new Message(CharacterNames.Emma, "Oh... Now I can go with my family. Thank you."),
        new Message(CharacterNames.Anne, "You're welcome, Emma. I hope you find peace now."),
        new Message(CharacterNames.Emma, "I will. Goodbye, Anne."),
        new Message(CharacterNames.Anne, "Goodbye, Emma."),
        
        //Mostrar el periodico que dice que estaba perdida pero la estaban buscando desesperadamente sus padres
    };
}