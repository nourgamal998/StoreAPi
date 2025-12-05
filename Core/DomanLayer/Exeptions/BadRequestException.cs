using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exeptions
{
    public sealed class BadRequestException(List<string> errors) : Exception("Validation Feilded")
    {
        public List<string> Errors { get; } = errors;

    }
}
