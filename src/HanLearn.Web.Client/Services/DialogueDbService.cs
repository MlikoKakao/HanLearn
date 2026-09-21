
// A short term replacement for DB - dialogues will be loaded from db later.
// Then they will be separated as <span> for definition on hover
// This page can be loaded into browser memory thus no wait for character
// definition lookup

// The db will need to handle both full text and separating it into spans
// - unless i find a way to do this automatically, and I can't think of a 
// way that's reliable now, characters can have either meaning based on 
// context. 你好 can have both separate meaning even when next to each other
// or the compound meaning. 

// There's an option for using an NLP to automate this but .. that'd add 
// so much delay - although the stories shouldn't have too much
// nuance so it should be possible.

public class DialogueDbService
{
    public string dialogue1 = "你好好味";
    public string[] separatedDialogue1 = ["你好", "好味"];
}