using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using api.Repositories;
using api.Services;
using api.Context;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            // Inicializacion de repos
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<,>));
            services.AddScoped<ICharacterRepository, CharactersRepository>();
            services.AddScoped<IMovieRepository, MoviesRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICharacterMovieRepository, CharacterMovieRepository>();

            // Inicializacion de servicios
            services.AddScoped<ICharacterService, CharactersService>();
            services.AddScoped<IMovieService, MoviesService>();
            
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddEntityFrameworkSqlServer();

            // Context para CharactersDb
            services.AddDbContext<CharactersDbContext>(options => {
                options.UseSqlServer(this.Configuration["CharactersConnectionString"]);
            });

            // Context para UsersDb
            services.AddDbContext<UserContext>((services, options) => {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(this.Configuration["UsersConnectionString"]);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
