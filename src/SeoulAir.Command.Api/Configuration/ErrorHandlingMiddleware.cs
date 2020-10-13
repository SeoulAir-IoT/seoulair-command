﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeoulAir.Command.Domain.Extensions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Api.Configuration
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = GenerateProblemDetails(ex);
            var jsonSetting = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = JsonSerializer.Serialize(problemDetails, jsonSetting);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)problemDetails.Status;

            return context.Response.WriteAsync(result);
        }

        private ProblemDetails GenerateProblemDetails(Exception exception)
        {
            string type;
            string title;
            HttpStatusCode code;

            switch (exception)
            {
                case ArgumentException argumentException:
                    code = HttpStatusCode.InternalServerError;
                    type = InternalServerErrorUri;
                    title = InternalServerErrorTitle;
                    _logger.LogError(exception.ToString()
                        .FormatAsExceptionMessage());
                    break;
                default:
                    code = HttpStatusCode.NotImplemented;
                    type = ConflictUri;
                    title = ConflictTitle;
                    _logger.LogError(exception.ToString()
                        .FormatAsExceptionMessage());
                    break;
            }

            return new ProblemDetails()
            {
                Type = type,
                Title = title,
                Detail = exception.Message,
                Status = (int)code
            };
        }
    }
}
