using System.Collections.Generic;

namespace Helpers
{
    public class ReturnGetVO<T>
    {
        public ReturnGetVO(T elementsReturned, bool foundWithSuccess, List<string> messages)
        {
            ElementsReturned = elementsReturned;
            Found = foundWithSuccess;
            Messages = messages;
        }

        public T ElementsReturned { get; set; }
        public bool Found { get; set; }
        public List<string> Messages { get; set; }
    }
}
