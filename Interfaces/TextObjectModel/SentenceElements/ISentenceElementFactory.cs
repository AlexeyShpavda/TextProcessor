namespace Interfaces.TextObjectModel.SentenceElements
{
    public interface ISentenceElementFactory
    {
        ISentenceElement GetSentenceElement(string key);
    }
}
