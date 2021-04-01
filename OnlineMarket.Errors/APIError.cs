using System.Collections.Generic;

namespace OnlineMarket.Errors
{
    public class APIError<E> where E: System.Enum
    {
        public E type { get; set; }
        public string typeName { get; set; }
        public string message { get; set; }
        public ICollection<FieldError> fields { get; set; }

        public APIError(E type)
        {
            this.type = type;
            typeName = System.Enum.GetName(typeof(E), type);
            fields = new List<FieldError>();
        }
    }
}