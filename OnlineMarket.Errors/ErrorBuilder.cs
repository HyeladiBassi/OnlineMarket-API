using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineMarket.Errors
{
    public class ErrorBuilder<E> where E : System.Enum
    {
        private E type;
        private string message;
        private readonly ErrorTypes invalidRequest;
        private readonly ICollection<FieldError> fields;

        public ErrorBuilder(E type)
        {
            this.type = type;
            fields = new List<FieldError>();
        }

        public ErrorBuilder<E> ChangeType(E type)
        {
            this.type = type;
            return this;
        }

        public ErrorBuilder<E> SetMessage(string message)
        {
            this.message = message;
            return this;
        }

        public ErrorBuilder<E> AddField(string field, string message)
        {
            FieldError fieldError = new FieldError() { message = message, field = field };
            fields.Add(fieldError);
            return this;
        }

        public ErrorBuilder<E> AddField(ModelStateDictionary modelState)
        {
            modelState.ToList().ForEach(x =>
            {
                x.Value
                    .Errors
                    .ToList()
                    .ForEach(y => AddField(x.Key.Trim(new char[] { '$', '.' }), y.ErrorMessage));
            });
            return this;
        }

        public ErrorBuilder<E> AddFields(ModelStateDictionary modelState)
        {
            modelState.ToList().ForEach(x =>
            {
                x.Value.Errors.ToList().ForEach(y => AddField(x.Key.Trim(new char[] { '$', '.' }), y.ErrorMessage));
            });
            return this;
        }

        public APIError<E> Build()
        {
            return new APIError<E>(type) { fields = fields, message = message };
        }
    }
}