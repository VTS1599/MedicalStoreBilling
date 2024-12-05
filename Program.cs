using Plugins.DataStore.InMemory;
using UseCases.CategoriesUseCases;
using UseCases;
using UseCases.CategoriesUSeCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;
using Plugins.DataStore.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MedBilling.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services for Database
builder.Services.AddDbContext<AccountContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("AccountManagement"));
});

builder.Services.AddDbContext<MedicalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MedicineManagement"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountContext>();

//Add dependency for RazorPages
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Inventory", p => p.RequireClaim("Position", "Inventory"));
	options.AddPolicy("Cashiers", p => p.RequireClaim("Position", "Cashier"));
});

//Add Dependencies

if(builder.Environment.IsEnvironment("QA"))
{
	builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
	builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
	builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();
}
else
{
	builder.Services.AddTransient<ICategoryRepository, CategorySQLRepository>();
	builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
	builder.Services.AddTransient<ITransactionRepository, TransactionSQLRepository>();
}

builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IViewProductsInCategoryUseCase, ViewProductsInCategoryUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();

builder.Services.AddTransient<IRecordTransactionUseCase, RecordTransactionUseCase>();
builder.Services.AddTransient<IGetTodayTransactionsUseCase, GetTodayTransactionsUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
