public static class AfterChapterTwoDialogs
{
    // Llamar después de completar exitosamente el minijuego y antes de pasar a la pantalla del tercer capítulo.
    public static Message[] Start = new Message[]
    {
        new Message(CharacterNames.Rob, "I never realized... there were moments of joy too."),
        new Message(CharacterNames.Anne14yo, "Yes, Rob. Even amidst the darkness, there were glimpses of light."),
        new Message(CharacterNames.Rob, "I can see that now. I remember my mother’s smile... and the times we laughed together."),
        new Message(CharacterNames.Rob, "It’s hard to believe that those moments were real, given all the pain I felt."),
        new Message(CharacterNames.Anne14yo, "Those memories are a part of you, just as much as the painful ones. They are precious."),
        new Message(CharacterNames.Rob, "Thank you, Anne. Your help has allowed me to see all this picture and find some peace."),
        new Message(CharacterNames.Anne14yo, "I’m truly glad I could help you, Rob. You deserve to find peace."),
        new Message(CharacterNames.Rob, "I think I’m ready to move on now. It’s time for me to let go of this pain."),
        new Message(CharacterNames.Anne14yo, "You’ll be alright, Rob. Farewell, and may you find the peace you’ve been seeking."),
        new Message(CharacterNames.Rob, "Goodbye, Anne. And... thank you. For everything you’ve done for me."),
        
        // Empezar siguiente capítulo, pasar a la pantalla del tercer capítulo
    };
}
