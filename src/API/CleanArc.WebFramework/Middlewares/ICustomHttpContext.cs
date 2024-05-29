using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.WebFramework.Middlewares
{
    public interface ICustomHttpContext
    {
        HttpRequest Request { get; }
        HttpResponse Response { get; }
        // Add other members you need...
    }
}
