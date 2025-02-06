
using Biogas_BackendEF.Models;
using Microsoft.EntityFrameworkCore;



namespace Biogas_BackendEF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Add Distributed Memory Cache (Required for Session)
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDbContext<BiogasDataBaseContext>(options =>
                options.UseSqlServer("name=conStr"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseSession();  // ✅ Ensure session middleware is enabled

            app.MapControllers();
            app.Run();
        }
    }
}
