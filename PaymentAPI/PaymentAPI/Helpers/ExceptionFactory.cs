using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using PaymentAPI.Domain.ModelView;
using PaymentAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentAPI.Helpers
{
    public static class ExceptionFactory
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, int StatusCode = 0, string message = "")
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        LogTraceFactory.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new Response()
                        {
                            statuscode = context.Response.StatusCode.ToString(),
                            message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
