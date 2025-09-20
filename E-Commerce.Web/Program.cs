
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data;
using Service.Mapping;
using System.Threading.Tasks;
using AutoMapper;
using ServiceAbstraction;
using Service;
using Persistence.Repositories;
using E_Commerce.Web.CustomMiddleWares;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using E_Commerce.Web.Factories;
using E_Commerce.Web.Extensions;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            #endregion

            var app = builder.Build();
            #region DataSeeding

            await app.SeedDataBaseAsync();

            #endregion

            #region Configure the HTTP request pipeline.
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
          //  app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
