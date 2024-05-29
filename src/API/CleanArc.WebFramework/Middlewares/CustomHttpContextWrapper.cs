using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.WebFramework.Middlewares
{
    public class CustomHttpContextWrapper : ICustomHttpContext
    {
        private readonly HttpContext _context;

        public CustomHttpContextWrapper(HttpContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public HttpRequest Request => _context.Request;

        public HttpResponse Response => _context.Response;

        // Implement other members of ICustomHttpContext if needed
    }
}
