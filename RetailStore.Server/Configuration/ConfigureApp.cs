namespace RetailStore.Server.Configuration;

public static class ConfigureApp
{
    public static async Task Config(this WebApplication app, WebApplicationBuilder builder)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        await app.ConfigureDatabase();
    }
}
