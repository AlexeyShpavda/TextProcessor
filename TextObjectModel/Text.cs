using System.Collections.Generic;
using System.Text;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.Sentences;

namespace TextObjectModel
{
    public class Text : IText
    {
        public IList<ISentence> Sentences { get; set; }

        public Text(IList<ISentence> sentences)
        {
            Sentences = sentences;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in Sentences)
            {
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }
    }
}
