using StudentManagement.DataContexts;
using StudentManagement.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// ... other middleware configurations

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

builder.Services.AddScoped<CoursesService>();

builder.Services.AddScoped<CoursesContext>(provider =>
{
    string coursesFilePath = "CSV_File/Course.csv";
    return new CoursesContext(coursesFilePath);
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
