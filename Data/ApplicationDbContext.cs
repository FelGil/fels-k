﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Projekt.Models.Sites> Sites { get; set; }
    public DbSet<Projekt.Models.Comments> Comments { get; set; }
    public DbSet<Projekt.Models.Categories> Categories { get; set; }

}
