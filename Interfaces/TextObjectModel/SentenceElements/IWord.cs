namespace Interfaces.TextObjectModel.SentenceElements
{
    public interface IWord : ISentenceElement
    {
        int Length { get; }

        bool StartWithVowel();

        bool StartWithConsonant();
    }
}
