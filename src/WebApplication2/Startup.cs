using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using WebApplication2.Services;
using WebApplication2.Services.EntityLogger;
using AutoMapper;


namespace WebApplication2
{
    public class Startup
    {
        private IHostingEnvironment CurrentEnvironment { get; }

        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add Application settings to the services container.
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Add framework services.
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TffFacultyContext>()
                .AddDefaultTokenProviders();

            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<TffFacultyContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionStringDev"]));
            }

            if (CurrentEnvironment.IsStaging())
            {
                services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<TffFacultyContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionStringStage"]));
            }

            if (CurrentEnvironment.IsProduction())
            {
                services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<TffFacultyContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionStringProd"]));
            }

            services.AddTransient<TffFacultyContextSeedData>();
            
            services.AddScoped<ITffFacultyRepository, TffFacultyRepository>();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            TffFacultyContextSeedData seeder, 
            IServiceProvider serviceProvider, 
            TffFacultyContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Error);
            loggerFactory.MinimumLevel = LogLevel.Debug;

            Func<string, LogLevel, bool> filter = (category, level) =>
            {
                if (env.IsDevelopment())
                {
                    if (level <= LogLevel.Information)
                    {
                        return false;
                    }
                }

                if (env.IsStaging())
                {
                    if (level <= LogLevel.Warning)
                    {
                        return false;
                    }
                }

                if (env.IsProduction())
                {
                    if (level <= LogLevel.Warning)
                    {
                        return false;
                    }
                }



                return true;
            };

            loggerFactory.AddEntityFramework<TffFacultyContext, EntityFrameworkLog>(serviceProvider, filter);

            //var sourceSwitch = new SourceSwitch("Sample Logger");
            //sourceSwitch.Level = SourceLevels.Error;
            //loggerFactory.AddTraceSource(sourceSwitch,
            //    new ConsoleTraceListener(false));
            //loggerFactory.AddTraceSource(sourceSwitch,
            //    new EventLogTraceListener("Application"));

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<TffFacultyContext>()
                             .Database.Migrate();
                    }
                }
                catch
                {
                    // ignored
                }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();
            

            Mapper.Initialize(config =>
            {
                //          Mapper.CreateMap<Person, Employee>()
                //.ForMember(e => e.Forename, o => o.MapFrom(p => p.Forename.ToLower()));

                config.CreateMap<User, FacultyProfileViewModel>().ReverseMap();
                config.CreateMap<User, FacultyProfileApiModel>().ReverseMap();
                config.CreateMap<User, HomeIndexFacultyViewModel>().ReverseMap();
                config.CreateMap<Event, EventViewModel>().ReverseMap();
                config.CreateMap<CreateUserViewModel, User>().ReverseMap();
                config.CreateMap<AdminEventFacultyViewModel, EventFaculty>().ReverseMap();
                config.CreateMap<AdminAddFactultyToEventViewModel, EventFaculty>().ReverseMap();
                config.CreateMap<HomeEventFacultyViewModel, EventFaculty>().ReverseMap();
                config.CreateMap<AdminProfileViewModel, User>().ReverseMap();
                config.CreateMap<Disclosure, DisclosureViewModel>()
                    .ForMember("WillImpactPresentation", opt => opt.ResolveUsing(src => src.WillImpactPresentation ? "Yes" : "No"))
                    .ForMember("CommercialInterest", opt => opt.ResolveUsing(src => src.CommercialInterest ? "Yes" : "No"))
                    .ForMember("DisclosedAllRelevantRelationships", opt => opt.ResolveUsing(src => src.DisclosedAllRelevantRelationships ? "Yes" : "No"));
                config.CreateMap<DisclosureViewModel, Disclosure>()
                    .ForMember("WillImpactPresentation", opt => opt.ResolveUsing(src => src.WillImpactPresentation == "Yes"))
                    .ForMember("CommercialInterest", opt => opt.ResolveUsing(src => src.CommercialInterest == "Yes"))
                    .ForMember("DisclosedAllRelevantRelationships", opt => opt.ResolveUsing(src => src.DisclosedAllRelevantRelationships == "Yes"));

                //config.CreateMap<Event, Event>().ReverseMap();
            });
            

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            await seeder.EnsureSeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
