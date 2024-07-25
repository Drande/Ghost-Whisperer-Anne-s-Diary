public static class AfterFailChapterTwoDialogs
{
    // Llamar cuando el jugador falla el minijuego del Capítulo Dos.
    public static Message[] Start = new Message[]
    {
        new Message(CharacterNames.Anne14yo, "Oh no, I messed up..."),
        new Message(CharacterNames.Rob, "What did you do, Anne?! You ruined everything!"),
        new Message(CharacterNames.Anne14yo, "I'm sorry, Rob. I didn't mean to..."),
        new Message(CharacterNames.Rob, "Sorry won't fix this! You have no idea how much this means to me!"),
        new Message(CharacterNames.Anne14yo, "I really tried, Rob. Please, give me another chance."),
        new Message(CharacterNames.Rob, "Another chance? You don't deserve another chance!"),
        new Message(CharacterNames.Rob, "You've made everything worse!"),
        new Message(CharacterNames.Anne14yo, "Please, Rob. Let me try again. I won't make the same mistake."),
        new Message(CharacterNames.Rob, "Fine, but please, try to get it right this time. I need this to be right."),
        
        // Volver al minijuego o reiniciar la secuencia
    };
}

