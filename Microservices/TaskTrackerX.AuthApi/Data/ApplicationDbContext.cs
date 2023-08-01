﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskTrackerX.AuthApi.Models;

namespace TaskTrackerX.AuthApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .Ignore(t => t.RoleName);
        }

        public static void EnsureRolesCreated(IServiceProvider context)
        {
            var roleManager = context.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole<Guid>("Admin")).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole<Guid>("Manager")).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole<Guid>("User")).GetAwaiter().GetResult();
            }
        }

        public static void EnsureUserCreated(IServiceProvider context)
        {
            var userManager = context.GetRequiredService<UserManager<User>>();

            if (!userManager.Users.Any())
            {
                // Создаем администратора
                var adminUser = new User
                {
                    Name = "Admitistrator",
                    UserName = "Admin"
                };

                // Создаем пользователя и указываем пароль
                userManager.CreateAsync(adminUser, "Admin123456!")
                    .GetAwaiter().GetResult();

                // Назначаем роль "Admin" пользователю
                userManager.AddToRoleAsync(adminUser, "Admin")
                    .GetAwaiter().GetResult();
            }
        }
    }
}
