using EmailManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.DataSeeder
{
    public static class ModelBuilderExtensions
    {
        public static void UserRoleSeeder(this ModelBuilder builder)
        {
            // Seeding Roles.

            builder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "771f568e-a7d5-496b-90c4-72ff997368e6", Name = "Manager", NormalizedName = "MANAGER" },
               new IdentityRole { Id = "93c66dd9-11c5-4836-b104-a9c333549530", Name = "Operator", NormalizedName = "OPERATOR" }
           );

            // Seeding Users.

            var hasher = new PasswordHasher<User>();

            User krisiManager = new User { Id = "fe86f129-41f3-4ab8-a61c-5f47239a1393", UserName = "krisi", NormalizedUserName = "KRISI", Email = "krisi@gmail.com", NormalizedEmail = "KRISI@GMAIL.COM", LockoutEnabled = true, SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN"};
            krisiManager.PasswordHash = hasher.HashPassword(krisiManager, "krisi123");

            User madinManager = new User { Id = "565dfbc0-2681-4f29-bc97-a619eacf339c", UserName = "madinftw", NormalizedUserName = "MADINFTW", Email = "madinftw@gmail.com", NormalizedEmail = "MADINFTW@GMAIL.COM", LockoutEnabled = true, SecurityStamp = "15CLJEKQCTLPRXMVXXNSWXZH6R6KJRRU"};
            madinManager.PasswordHash = hasher.HashPassword(madinManager, "madin123");

            builder.Entity<User>().HasData(krisiManager, madinManager);

            // Assign Users to Roles.

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "771f568e-a7d5-496b-90c4-72ff997368e6",
                    UserId = krisiManager.Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = "771f568e-a7d5-496b-90c4-72ff997368e6",
                    UserId = madinManager.Id
                });
        }

        public static void StatusSeeder(this ModelBuilder builder)
        {
            builder.Entity<StatusApplication>().HasData(
                new StatusApplication { Id = "61cb6584-591b-4560-bc4a-a89950b15cc3", StatusType = "New" },
                new StatusApplication { Id = "645ad030-3b7f-47fb-93e1-1c9315b34673", StatusType = "Open" },
                new StatusApplication { Id = "6c60cb0a-5395-49b1-abfd-40a4db7a355a", StatusType = "Closed" }
                );

            builder.Entity<StatusEmail>().HasData(
                new StatusEmail { Id = "a0e53404-d40e-4a1e-8fe5-9a5fc0139ed9", StatusType = "Not Reviewed" },
                new StatusEmail { Id = "165e4e23-7fed-4bd6-a859-530026625ffc", StatusType = "Invalid Application" }
                );
        }
    }
}