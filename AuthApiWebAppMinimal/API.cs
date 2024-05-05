namespace AuthApiWebAppMinimal
{
    public static class API
    {
        
        public static void ConfigurationAPI(this WebApplication app)
        {
            app.MapPost(pattern: "/Register", RegisterAsync);
            app.MapPost(pattern: "/Login", LoginAsync);
        }



        public static async Task<IResult> RegisterAsync(IAuthService authService , RegisteringModel registeringModel)
        {
            try
            {
                return Results.Ok(await authService.RegisterMethod(registeringModel));
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static async Task<IResult> LoginAsync(IAuthService authService,UserInput userInput)
        {
            try
            {
                return Results.Ok(await authService.LoginAsync(userInput));
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
