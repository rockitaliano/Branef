using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project.Branef.Application.Core
{
    public abstract class RequestBase
    {
        [JsonIgnore]
        public Guid CorrelationId { get; set; }

        [JsonIgnore]
        public IEnumerable<string> Notifications { get; set; }

        protected RequestBase()
        {
            CorrelationId = CorrelationId == Guid.Empty
                ? Guid.NewGuid()
                : CorrelationId;
        }
    }
}
