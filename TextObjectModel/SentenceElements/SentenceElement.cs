using System.Runtime.Serialization;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    [DataContract(Namespace = "")]
    public abstract class SentenceElement : ISentenceElement
    {
        [DataMember]
        public string Chars { get; set; }

        protected SentenceElement(string str)
        {
            Chars = str;
        }
    }
}
