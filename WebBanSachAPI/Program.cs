using Microsoft.EntityFrameworkCore;
using WebBanSachModel.Entity;
using WebBanSachModel.Helper;
using WebBanSachRepository;
using WebBanSachRepository.CategogyRepo;
using WebBanSachRepository.ProductRepo;
using WebBanSachRepository.UserRepo;
using WebBanSachService.Category;
using WebBanSachService.Product;
using WebBanSachService.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BanSachContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("Myconn")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ICategogyRepo, CategogyRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
