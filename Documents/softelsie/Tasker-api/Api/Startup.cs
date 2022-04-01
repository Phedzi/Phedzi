using AutoMapper;
using DAL.Checker;
using DAL.Checker.Repository;
using DAL.Common;
using Logic.Checker;
using Logic.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Models.Checker;
using Models.Common;
using Models.Extra;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Api
{
    public class Startup
    {
        private const string SecretKey = "ManangeSophiKhakhuMusundwaCalvin"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<CompanyDbContext>(options =>
                    options.UseSqlServer
                    (Configuration.GetConnectionString("SQLConnectionRemote")));
            // for migration ** A Guess **
            services.AddIdentity<UserModel, IdentityRole>()
                    .AddEntityFrameworkStores<CompanyDbContext>()
                    .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            });


            //Helpers
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            var mapConfig = new MapperConfiguration(config => config.AddProfile(new Transformer()));// you can add more profiles by using multyline braces {}
            IMapper mapper = mapConfig.CreateMapper();
            services.AddScoped(typeof(IResponseService<>), typeof(ResponseService<>));
            services.AddSingleton(mapper);

            services.AddScoped(typeof(IResponseService<>), typeof(ResponseService<>));

            // Enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            //Authentication and Authorization
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim("User", "taxi Owner"));
                options.AddPolicy("SAdmin", policy => policy.RequireClaim("Admin", "taxi Admin"));
            });

            //Repositories
            //services.AddSingleton<IContextRepository, ContextRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICheckingRepository, CheckingRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<IAgreementRepository, AgreementRepository>();
            services.AddScoped<IAgreementTypeRepository, AgreementTypeRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ISubMenuRepository, SubMenuRepository>();

            //Services
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ICheckingService, CheckingService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IAdministration, Administration>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IAgreementTypeService, AgreementTypeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ISubMenuService, SubMenuService>();
            services.AddScoped<IUserService, UserService>();
            //Transformers
            services.AddControllers(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
