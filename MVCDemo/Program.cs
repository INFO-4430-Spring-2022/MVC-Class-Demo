var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Place key and IV here to keep consistency.
// Key needs to be 32 characters or 256 bits.
// IV needs to be 16 characters or 128 bits.
//Tools.DataEncryptor.SetKey("34a490asdlkJ0945lkjads09743145kj", "MVCExamples-4430");

// For Base 64
// Key should be at least 44 Base-64 characters; first 32 bytes will be used.
// IV should be at least 24 Base-64 characters; first 16 bytes will be used.
Tools.DataEncryptor.SetKeyBase64("34a490asdlkJ0945lkjads09743145kj", "MVCExamples+4430+2022");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
