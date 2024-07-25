public static class ChapterTwoDialogs
{
    // Llamar después de la pantalla chapter 2, antes del minijuego, este diálogo da paso al minijuego.
    public static Message[] Start = new Message[]
    {
        new Message(CharacterNames.Rob, "Who's there? I can hear you..."),
        new Message(CharacterNames.Anne14yo, "My name is Anne. Who are you?"),
        new Message(CharacterNames.Rob, "I’m Rob. This is my house. Why are you here?"),
        new Message(CharacterNames.Anne14yo, "Your house? I didn’t realize..."),
        new Message(CharacterNames.Rob, "Yes. I used to live here with my father. He was... cruel. He hurt me."),
        new Message(CharacterNames.Anne14yo, "I’m so sorry, Rob. That sounds so painful. How can I help you?"),
        new Message(CharacterNames.Rob, "Help? How can you help me? I’m stuck here, reliving the worst moments of my life."),
        new Message(CharacterNames.Anne14yo, "Maybe I can help you find some peace. Can you tell me more about your memories?"),
        new Message(CharacterNames.Rob, "Memories? They’re all dark and twisted. My father... he beat me for the smallest things."),
        new Message(CharacterNames.Rob, "I thought no one cared. I thought I was alone, that nobody loved me."),
        new Message(CharacterNames.Anne14yo, "I want to understand what happened. Please, let me help you."),
        new Message(CharacterNames.Rob, "Why should I trust you? You’re just another person who doesn’t understand."),
        new Message(CharacterNames.Anne14yo, "I genuinely want to help. Let me try to ease your suffering."),
        new Message(CharacterNames.Rob, "Fine. Look around the house. There are objects that trigger my memories."),
        new Message(CharacterNames.Rob, "But be careful. If you make a mistake, it might make things worse."),
        new Message(CharacterNames.Anne14yo, "I’ll be careful. Let’s start with something small and see what we find."),
        new Message(CharacterNames.Rob, "Alright. Just don’t make things worse. I’ve lived through enough torment."),
        new Message(CharacterNames.Anne14yo, "I promise to be careful. Let’s work through this together."),
        new Message(CharacterNames.Rob, "Fine. I just want to be free of this pain..."),

        // Empezar minijuego
    };
}

}