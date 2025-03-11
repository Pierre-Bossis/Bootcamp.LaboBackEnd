using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
using System.Security.Claims;

namespace Bootcamp.LaboBackEnd.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        #region Traitement de l'erreur
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            int statusCode = 0;
            switch (ex)
            {
                case EmailAlreadyExistsException:
                case NameCategorieAlreadyExistsException:
                case InvalidLoginException:
                case BusinessException:
                    context.Response.StatusCode = 400;
                    _logger.LogError(ex, "An unexpected error occurred for request {RequestPath}", context.Request.Path);
                    break;

                case UtilisateurNotFoundException:
                    context.Response.StatusCode = 404;
                    _logger.LogError(ex, "An unexpected error occurred for request {RequestPath}", context.Request.Path);
                    break;
                default:
                    context.Response.StatusCode = 500;
                    _logger.LogError(ex, "An unexpected error occurred for request {RequestPath}", context.Request.Path);
                    break;
            }

            string responseMessage = context.Response.StatusCode == 500 ? "Server error" : ex.Message;
            #endregion

            #region Gestion Logs
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);
            string logFilePath = Path.Combine(logDirectory, "app-logs.txt");

            string logMessage = $"{DateTime.Now} - Error {context.Response.StatusCode} - Exception: {ex.Message}\n" +
                    $"StackTrace: {ex.StackTrace}\n" +
                    $"Request Path: {context.Request.Path}\n" +
                    $"HTTP Method: {context.Request.Method}\n" +
                    /*$"User ID: {User.FindFirst(ClaimTypes.Sid)?.Value}\n" +*/
                    $"Machine: {Environment.MachineName}\n" +
                    "-----------------------------------------\n" +
                    "\n\n";
            File.AppendAllText(logFilePath, logMessage);
            #endregion

            await context.Response.WriteAsync(responseMessage);
        }
    }
}
