using System.Collections.Generic;

namespace Helpers
{
    public class ReturnCreatedVO
    {
        public ReturnCreatedVO(bool createdWithSuccess, List<string> messages)
        {
            CreatedWithSuccess = createdWithSuccess;
            Messages = messages;
        }

        public bool CreatedWithSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
    public class ReturnCreatedVO<T>
    {
        public ReturnCreatedVO(T elementsCreated, bool createdWithSuccess, List<string> messages)
        {
            ElementsCreated = elementsCreated;
            CreatedWithSuccess = createdWithSuccess;
            Messages = messages;
        }

        public T ElementsCreated { get; set; }
        public bool CreatedWithSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
}
