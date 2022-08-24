using Microsoft.EntityFrameworkCore;
using WebAPI;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

//Set content Root

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

// Add services to the container.

// SQL LITE
//builder.Services.AddDbContext<SubaContext>(options => options.UseSqlite(
//    builder.Configuration.GetConnectionString("SubaTrip")
//));

// MYSQL
builder.Services.AddDbContext<SubaContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("SubaDatabase"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
// Cors

app.UseCors(options =>
{
    options.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
});

//Requests

app.MapGet("/", () =>
{
    return "API working if you see this page =)";
});

// db.[databaseName].

app.MapGet("/api/contactinfo", async (SubaContext db) => await db.ContactInfo.ToListAsync());

app.MapGet("/api/contactinfo/{id}", async (SubaContext db, int id) => await db.ContactInfo.FindAsync(id));

app.MapGet("/api/tourinfo", async (SubaContext db) => await db.TourInfo.ToListAsync());

app.MapGet("/api/tourinfo/{id}", async (SubaContext db, int id) => await db.TourInfo.FindAsync(id));

app.MapGet("/api/products/", async (SubaContext db) => await db.Products.ToListAsync());

app.MapGet("/api/products/{id}", async (SubaContext db, int id) => await db.Products.FindAsync(id));

app.MapGet("/api/inputinfo/", async (SubaContext db) => await db.InputInfo.ToListAsync());

app.MapGet("/api/inputinfo/{id}", async (SubaContext db, int id) => await db.InputInfo.FindAsync(id));

app.MapGet("/api/gallery/", async (SubaContext db) => await db.Gallery.ToListAsync());

app.MapGet("/api/gallery/{id}", async (SubaContext db, int id) => await db.Gallery.FindAsync(id));

// Post Requests

app.MapPost("/api/contactinfo", async (SubaContext db, ContactInfo contactinfo) =>
{
    await db.ContactInfo.AddAsync(contactinfo);
    await db.SaveChangesAsync();

    return Results.Ok(contactinfo);
});

app.MapPost("/api/tourinfo", async (SubaContext db, TourInfo tourInfo) =>
{
    await db.TourInfo.AddAsync(tourInfo);
    await db.SaveChangesAsync();

    return Results.Ok(tourInfo);
});

app.MapPost("/api/inputinfo", async (SubaContext db, InputInfo inputinfo) =>
{
    await db.InputInfo.AddAsync(inputinfo);
    await db.SaveChangesAsync();

    return Results.Ok(inputinfo);
});

app.MapPost("/api/gallery", async (SubaContext db, Gallery gallery) =>
{
    await db.Gallery.AddAsync(gallery);
    await db.SaveChangesAsync();

    return Results.Ok(gallery);
});

app.MapPost("/api/products", async (SubaContext db, Products products) =>
{
    products.Image = Tools.ConvertBase64ToFile(products.Image, builder.Environment.ContentRootPath + @"\wwwroot\");

    await db.Products.AddAsync(products);
    await db.SaveChangesAsync();

    return Results.Ok(products);
});

// Delete Requests

app.MapDelete("/api/contactinfo/{id}", async (SubaContext db, int id) =>
{
    var item = await db.ContactInfo.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.ContactInfo.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/api/tourinfo/{id}", async (SubaContext db, int id) =>
{
    var item = await db.TourInfo.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.TourInfo.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/api/products/{id}", async (SubaContext db, int id) =>
{
    var item = await db.Products.FindAsync(id);
    if (item == null) return Results.NotFound();

    File.Delete(builder.Environment.ContentRootPath + @"\wwwroot\" + item.ID);
    db.Products.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/api/inputinfo/{id}", async (SubaContext db, int id) =>
{
    var item = await db.InputInfo.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.InputInfo.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/api/gallery/{id}", async (SubaContext db, int id) =>
{
    var item = await db.Gallery.FindAsync(id);
    if (item == null) return Results.NotFound();

    File.Delete(builder.Environment.ContentRootPath + @"\wwwroot\" + item.ID);
    db.Gallery.Remove(item);

    await db.SaveChangesAsync();

    return Results.NoContent();

});

// PUT Requests
app.MapPut("/api/contactinfo/{id}", async (SubaContext db, int id, ContactInfo contactinfo) =>
{
    if (contactinfo.ID != id) return Results.BadRequest();

    db.ContactInfo.Update(contactinfo);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/tourinfo/{id}", async (SubaContext db, int id, TourInfo tourinfo) =>
{
    if (tourinfo.ID != id) return Results.BadRequest();

    db.TourInfo.Update(tourinfo);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/inputinfo/{id}", async (SubaContext db, int id, InputInfo inputinfo) =>
{
    if (inputinfo.ID != id) return Results.BadRequest();

    db.InputInfo.Update(inputinfo);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/gallery/{id}", async (SubaContext db, int id, Gallery gallery) =>
{
    if (gallery.ID != id) return Results.BadRequest();

    db.Gallery.Update(gallery);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/products/{id}", async (SubaContext db, int id, Products products) =>
{
    if (products.ID != id) return Results.BadRequest();

    if (Tools.IsBase64String(products.Image))
    {
        Products OldProducts = await db.Products.AsNoTracking().SingleOrDefaultAsync(e => e.ID == id);
        if (!string.IsNullOrEmpty(OldProducts.Image))
        {
            File.Delete(builder.Environment.ContentRootPath + @"\wwwroot\" + OldProducts.Image);
        }

        products.Image = Tools.ConvertBase64ToFile(products.Image, builder.Environment.ContentRootPath + @"\wwwroot\");
    }

    db.Products.Update(products);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();