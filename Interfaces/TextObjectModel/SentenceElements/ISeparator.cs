namespace Interfaces.TextObjectModel.SentenceElements
{
    public interface ISeparator : ISentenceElement
    {
        bool IsSpaceMark();
        bool IsWordSeparationMark();
        bool IsSentenceSeparationMark();
        bool IsExclamationMark();
        bool IsQuestionMark();
    }
}
