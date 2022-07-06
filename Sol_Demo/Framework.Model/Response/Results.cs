using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model.Response
{
    public class Results<T>
    {
        public bool? Success { get; set; }

        public T? Value { get; set; }

        public string? Message { get; set; }

        public int? StatusCode { get; set; }
    }
}