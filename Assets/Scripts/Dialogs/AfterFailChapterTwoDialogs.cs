public static class AfterFailChapterTwoDialogs
{
    // Llamar cuando el jugador falla el minijuego del Capítulo Dos.
    public static Message[] Failure = new Message[]
    {
        new Message(CharacterNames.Anne, "Oh no, I messed up..."),
        new Message(CharacterNames.Rob, "What did you do, Anne?! You ruined everything!"),
        new Message(CharacterNames.Anne, "I'm sorry, Rob. I didn't mean to..."),
        new Message(CharacterNames.Rob, "Sorry won't fix this! You have no idea how much this means to me!"),
        new Message(CharacterNames.Anne, "I really tried, Rob. Please, give me another chance."),
        new Message(CharacterNames.Rob, "Another chance? You don't deserve another chance! You've made everything worse!"),
        new Message(CharacterNames.Anne, "Please, Rob. Let me try again. I won't make the same mistake."),
        new Message(CharacterNames.Rob, "Fine, but please, try to get it right this time. I need this to be right."),
        
        // Volver al minijuego o reiniciar la secuencia
    };
}

