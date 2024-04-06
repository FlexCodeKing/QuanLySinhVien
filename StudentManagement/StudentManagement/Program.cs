using StudentManagement.DataContexts;
using StudentManagement.Services;
using Microsoft.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<StudentServices>();

builder.Services.AddScoped<StudentContexts>(provider =>
{
    string filePath = "CSV_File/studentinfo.csv";
    return new StudentContexts(filePath);
}
);
builder.Services.AddScoped<ManagerService>();

builder.Services.AddScoped<ManagerContext>(providers =>
{
    string fileManager = "CSV_File/Manager.csv";

    return new ManagerContext(fileManager);
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
