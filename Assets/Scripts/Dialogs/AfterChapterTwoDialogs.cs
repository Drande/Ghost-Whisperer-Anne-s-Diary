public static class AfterChapterTwoDialogs
{
    // Llamar después de completar exitosamente el minijuego y antes de pasar a la pantalla del tercer capítulo.
    public static Message[] Start = new Message[]
    {
        new Message(CharacterNames.Rob, "I didn't realize... there were good times too."),
        new Message(CharacterNames.Anne, "Yes, Rob. Not everything was bad."),
        new Message(CharacterNames.Rob, "I can see that now. I remember my mom's smile and the times we laughed."),
        new Message(CharacterNames.Anne, "Those memories are just as real as the painful ones."),
        new Message(CharacterNames.Rob, "Thank you, Anne. You've helped me find peace."),
        new Message(CharacterNames.Anne, "I'm glad I could help, Rob."),
        new Message(CharacterNames.Rob, "I think it's time for me to move on."),
        new Message(CharacterNames.Anne, "You'll be alright. Goodbye, Rob."),
        new Message(CharacterNames.Rob, "Goodbye, Anne. And... thank you, for everything.")
        
        // Empezar siguiente capítulo, pasar a la pantalla del tercer capitulo
    };
}
