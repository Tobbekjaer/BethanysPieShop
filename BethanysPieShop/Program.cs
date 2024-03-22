var builder = WebApplication.CreateBuilder(args);

// Adding services 
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Adding middleware
app.UseStaticFiles();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

app.Run();
