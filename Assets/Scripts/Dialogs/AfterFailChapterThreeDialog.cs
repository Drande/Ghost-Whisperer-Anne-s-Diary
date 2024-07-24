public static class ChapterThreeFailureDialogs
{
    // Llamar cuando el jugador falla el minijuego del Capítulo Tres.
    public static Message[] Failure = new Message[]
    {
        new Message(CharacterNames.Anne, "Ah! It hurts... What is happening to me?"),
        new Message(CharacterNames.Millie, "I warned you, Anne. This is what happens when you don't follow my lead."),
        new Message(CharacterNames.Anne, "But... I tried my best!"),
        new Message(CharacterNames.Millie, "Your best wasn't good enough. Now you feel a fraction of my pain."),
        new Message(CharacterNames.Anne, "Please, Millie... Stop this. I don't want to be hurt."),
        new Message(CharacterNames.Millie, "Then focus. Do better. This pain is a reminder of my suffering. Don't let it be in vain."),
        new Message(CharacterNames.Anne, "I... I'll try again. I won't give up."),
        new Message(CharacterNames.Millie, "Good. But remember, one more mistake and the pain will be worse. Concentrate, Anne."),
        
        // reiniciar minijuego o reiniciar la escena del capitulo tres
    };
}
