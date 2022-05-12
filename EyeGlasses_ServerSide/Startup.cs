using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System.Text;
using BL.Configuration;
using DAL.Models;
using BL.Services;
using BL.Interfaces;
using BL.AppServices;
using BL.Bases;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace EyeGlasses_ServerSide
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
            services.AddAutoMapper(typeof(Mapper_Configuration));
            services.Configure<JWT>(Configuration.GetSection("JWT"));

            services.AddIdentity<User_Identity, IdentityRole>().AddEntityFrameworkStores<AppDB_Context>();
            services.AddScoped<IAccount, Authentication_Service>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<Product_AppService>();
            services.AddTransient<ShoppingCart_AppService>();
            services.AddTransient<ShoppingCartItem_AppService>();
            services.AddHttpContextAccessor();
            services.AddTransient<Order_AppService>();
            services.AddTransient<OrderItem_AppService>();
            services.AddDbContext<AppDB_Context>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AccessDBString"))
           );
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters =
                       new TokenValidationParameters()
                       {
                           ValidateIssuer = true,
                           ValidIssuer = Configuration["JWT:Issuer"],
                           ValidateAudience = true,
                           ValidAudience = Configuration["JWT:Audience"],
                           IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret_Key"]))
                       };
            });
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EyeGlasses_ServerSide", Version = "v1" });
            });
            services.AddCors();
            services.AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins("http://localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod().AllowCredentials();

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EyeGlasses_ServerSide v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
