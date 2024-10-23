namespace BudgieBudgeting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Add session services
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout period
                options.Cookie.HttpOnly = true; // Make the session cookie HttpOnly for security
                options.Cookie.IsEssential = true; // Required for GDPR compliance
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Enable session middleware
            app.UseSession();

            app.MapRazorPages();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/login");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}
