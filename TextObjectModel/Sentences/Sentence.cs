using System.Collections.Generic;
using Interfaces.TextObjectModel;

namespace TextObjectModel.Sentences
{
    public class Sentence
    {
        private ICollection<IToken> Tokens { get; set; }
    }
}
