using System.ComponentModel;

namespace AlexaSkillProject.Domain
{
    public enum WordEnum
    {
        [Description("Word")]
        Word,

        [Description("WordPartOfSpeech")]
        WordPartOfSpeech,

        [Description("WordDefinition")]
        WordDefinition,

        [Description("WordExample")]
        WordExample,
    }

    public enum SuccessPhrases
    {
        [Description("That is correct! Great job!")]
        Great = 1,

        [Description("That is correct! Nice Work!")]
        Nice = 2,

        [Description("That is correct! Incredible job!")]
        Incredible = 3,

        [Description("That is correct!  You are on fire")]
        OnFire = 4,

        [Description("This is insanity!  Shut it down folks!")]
        Insanity = 5
    }

    public enum ErrorPhrases
    {
        [Description("I am sorry that is not correct")]
        Incorrect = 1
    }

    public enum StrategyHandlerTypes
    {
        LaunchRequest,
        SessionEndedRequest,
        IntentRequest
    }

   
}
