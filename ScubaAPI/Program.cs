using Microsoft.EntityFrameworkCore;
using ScubaAPI.Models;
using ScubaAPI;
using Type = ScubaAPI.Models.Type;
using Sted = ScubaAPI.Models.Sted;
using Tur = ScubaAPI.Models.Tur;

var builder = WebApplication.CreateBuilder(args);

// Set Content Root
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
// Add services to the container.

builder.Services.AddDbContext<StedContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("Scuba")
    ));
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
// Use Static Files
app.UseStaticFiles();

 // CORS
app.UseCors(options =>
{
    options.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
});

// GET
app.MapGet("/api/sted", async (StedContext db) => await db.Steder.Include(e => e.Type).ToListAsync());
app.MapGet("/api/tur", async (StedContext db) => await db.Turer.Include(e => e.Sted).ToListAsync());
app.MapGet("/api/img", async (StedContext db) => await db.Images.Include(e => e.Sted).ToListAsync());
app.MapGet("/api/signup", async (StedContext db) => await db.Signup.Include(e => e.Tur).ToListAsync());
app.MapGet("/api/kontakt", async (StedContext db) => await db.Kontakt.ToListAsync());
app.MapGet("/api/type", async (StedContext db) => await db.Type.ToListAsync());

// GET by ID
app.MapGet("/api/sted/{id}", async (StedContext db, int id) => await db.Steder.Include(e => e.Type).FirstOrDefaultAsync(e => e.Id == id));
app.MapGet("/api/tur/{id}", async (StedContext db) => await db.Turer.ToListAsync());
app.MapGet("/api/img/{id}", async (StedContext db) => await db.Steder.ToListAsync());
app.MapGet("/api/signup/{id}", async (StedContext db) => await db.Steder.ToListAsync());
app.MapGet("/api/kontakt/{id}", async (StedContext db) => await db.Steder.ToListAsync());
app.MapGet("/api/type/{id}", async (StedContext db, int id) => await db.Steder.Where(e => e.TypeID == id).ToListAsync());

// POST
app.MapPost("/api/sted", async (StedContext db, Sted steder) => 
{
  
    await db.Steder.AddAsync(steder);
    await db.SaveChangesAsync();

    return Results.Ok(steder.Id);
});
app.MapPost("/api/tur", async (StedContext db, Tur turer) =>
{

    await db.Turer.AddAsync(turer);
    await db.SaveChangesAsync();

    return Results.Ok(turer.Id);
});
app.MapPost("/api/img", async (StedContext db, IMG image) =>
{
    image.Image = Tools.ConvertBase64tofile(image.Image, builder.Environment.ContentRootPath + @"/wwwroot/");

    await db.Images.AddAsync(image);
    await db.SaveChangesAsync();

    return Results.Ok(image.Id);
});
app.MapPost("/api/singup", async (StedContext db, Tilmeld tilmeld) =>
{

    await db.Signup.AddAsync(tilmeld);
    await db.SaveChangesAsync();

    return Results.Ok(tilmeld.Id);
});
app.MapPost("/api/kontakt", async (StedContext db, Kontakt kontakt) =>
{

    await db.Kontakt.AddAsync(kontakt);
    await db.SaveChangesAsync();

    return Results.Ok(kontakt.Id);
});
app.MapPost("/api/type", async (StedContext db, Type type) =>
{

    await db.Type.AddAsync(type);
    await db.SaveChangesAsync();

    return Results.Ok(type.Id);
});

// DELETE by ID
app.MapDelete("/api/sted/{id}", async (StedContext db, int id) =>
{
    var item = await db.Steder.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.Steder.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();

});
app.MapDelete("/api/tur/{id}", async (StedContext db, int id) =>
{
    var item = await db.Turer.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.Turer.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();

});
app.MapDelete("/api/img/{id}", async (StedContext db, int id) =>
{
    var item = await db.Images.FindAsync(id);
    if (item == null) return Results.NotFound();

    File.Delete(builder.Environment.ContentRootPath + @"/wwwroot/" + item.Image);
    db.Images.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();

});
app.MapDelete("/api/signup/{id}", async (StedContext db, int id) =>
{
    var item = await db.Signup.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.Signup.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();

});
app.MapDelete("/api/kontakt/{id}", async (StedContext db, int id) =>
{
    var item = await db.Kontakt.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.Kontakt.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();

});
app.MapDelete("/api/type/{id}", async (StedContext db, int id) =>
{
    var item = await db.Type.FindAsync(id);
    if (item == null) return Results.NotFound();

    db.Type.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();

});

//PUT by ID
app.MapPut("/api/sted/{id}", async (StedContext db, int id, Sted steder) =>
{
    if (steder.Id != id) return Results.BadRequest();

    db.Steder.Update(steder);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPut("/api/tur/{id}", async (StedContext db, int id, Tur turer) =>
{
    if (turer.Id != id) return Results.BadRequest();

    db.Turer.Update(turer);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPut("/api/img/{id}", async (StedContext db, int id, IMG image) =>
{
    if (image.Id != id) return Results.BadRequest();

    if (Tools.IsBase64String(image.Image))
    {
        IMG oldImage = await db.Images.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        if (!string.IsNullOrEmpty(oldImage.Image))
        {
            File.Delete(builder.Environment.ContentRootPath + @"/wwwroot/" + oldImage.Image);
        }
        image.Image = Tools.ConvertBase64tofile(image.Image, builder.Environment.ContentRootPath + @"/wwwroot/");
    }
    db.Images.Update(image);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPut("/api/signup/{id}", async (StedContext db, int id, Tilmeld tilmeld) =>
{
    if (tilmeld.Id != id) return Results.BadRequest();

    db.Signup.Update(tilmeld);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPut("/api/kontakt/{id}", async (StedContext db, int id, Kontakt kontakt) =>
{
    if (kontakt.Id != id) return Results.BadRequest();

    db.Kontakt.Update(kontakt);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapPut("/api/type/{id}", async (StedContext db, int id, Type type) =>
{
    if (type.Id != id) return Results.BadRequest();

    db.Type.Update(type);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.Run();