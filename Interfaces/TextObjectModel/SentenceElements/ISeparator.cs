namespace Interfaces.TextObjectModel.SentenceElements
{
    public interface ISeparator : ISentenceElement
    {
        bool IsWordSeparator();
        bool IsExclamationMark();
        bool IsQuestionMark();
        bool IsDeclarativeMark();
    }
}
