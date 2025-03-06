using Bootcamp.LaboBackEnd.BLL.CustomExceptions;

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
                case InvalidLoginException:
                    // 400 - erreur de la part de l'utilisateur (de mauvaise donnée lors de l'input)
                    context.Response.StatusCode = 400;
                    _logger.LogError(ex, "An unexpected error occurred.");
                    break;

                case UtilisateurNotFoundException:
                    // 404 - la resource n'existe pas
                    context.Response.StatusCode = 404;
                    _logger.LogError(ex, "An unexpected error occurred.");
                    break;
                default:
                    // 500 - l'erreur provient du serveur (merci de réessayer plus tard)
                    context.Response.StatusCode = 500;
                    _logger.LogError(ex, "An unexpected error occurred.");
                    break;
            }

            string responseMessage = context.Response.StatusCode == 500 ? "Server error" : ex.Message;

            // TODO logging et StatusCode = 500, alors il faudrait écrire des logs

            await context.Response.WriteAsync(responseMessage);
        }
    }
}
