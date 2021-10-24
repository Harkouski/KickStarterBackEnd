using BLL;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Token;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BLL.Services.UserService;

namespace KickStarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GoogleConfig>(Configuration.GetSection("GOOGLE"));
            services.AddTransient<google>();

            services.AddTransient<IUserService<UserDTO>, UserService>();

            services.AddTransient<IElectedProjectService<ElectedProjectDTO>, ElectedProjectService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProjectService<ProjectDTO>, ProjectService>();

            services.AddTransient<ICommentService<CommentDTO>, CommentService>();

            services.AddTransient<IRatingService<RatingDTO>, RatingService>();

            services.AddTransient<IService<CategoryDTO>, CategoryService>();

            services.AddTransient<EmailService>();

            services.AddTransient<IHangFireService,HangFireService>();

            services.AddTransient<IDonationHistoryService<DonationHistoryDTO>, DonationHistoryService>();

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddEntityFrameworkStores<SampleContext>().AddDefaultTokenProviders();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DbConnection")));
            services.AddHangfireServer();



            services.AddDbContext<SampleContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddControllers();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KickStarter", Version = "v1" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,

                           ValidIssuer = AuthOptions.ISSUER,

                           ValidateAudience = true,

                           ValidAudience = AuthOptions.AUDIENCE,

                           ValidateLifetime = true,

                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                           ValidateIssuerSigningKey = true,
                       };
                   });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KickStarter v1"));
            }


            app.UseHangfireDashboard();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization(); 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
