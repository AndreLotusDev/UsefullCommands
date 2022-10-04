using System.Collections.Generic;

namespace Helpers
{
    public class ReturnUpdatedVO
    {
        public ReturnUpdatedVO(bool updateWithSuccess, List<string> messages)
        {
            UpdateWithSuccess = updateWithSuccess;
            Messages = messages;
        }

        public bool UpdateWithSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
    public class ReturnUpdatedVO<T>
    {
        public ReturnUpdatedVO(T elementUpdated, bool updateWithSuccess, List<string> messages)
        {
            ElementUpdated = elementUpdated;
            UpdateWithSuccess = updateWithSuccess;
            Messages = messages;
        }

        public T ElementUpdated { get; set; }
        public bool UpdateWithSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
}
