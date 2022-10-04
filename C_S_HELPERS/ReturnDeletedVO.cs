using System.Collections.Generic;

namespace Helpers
{
    public class ReturnDeletedVO
    {
        public ReturnDeletedVO(bool deletedWithSuccess, List<string> messages)
        {
            DeletedWithSuccess = deletedWithSuccess;
            Messages = messages;
        }

        public bool DeletedWithSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
    public class ReturnDeletedVO<T>
    {
        public ReturnDeletedVO(T elementDeleted, bool deletedWithSuccess, List<string> messages)
        {
            ElementDeleted = elementDeleted;
            DeletedWithSuccess = deletedWithSuccess;
            Messages = messages;
        }

        public T ElementDeleted { get; set; }
        public bool DeletedWithSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
}
