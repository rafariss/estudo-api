using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();


//metodo de envio de parametro
app.MapPost("/products", (Product product) => {
     ProductRepository.Add(product); 
    return Results.Created("/products/" + product.Code, product.Code);
     });

//Metodo de recebimento de dados
app.MapGet("/products/{code}", ([FromRoute]string code) => {
    var product =  ProductRepository.GetBy(code);
    if(product != null)
        return Results.Ok("Produto codigo: " + product.Code + " não encontrado!!");
    
    return Results.NotFound();
});

//Metodo de alteração do produto
app.MapPut("/products", (Product product) => {
    var productSaved =  ProductRepository.GetBy(product.Code);
    productSaved.Name =  product.Name;
    return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute]string code) =>{

    var productSaved =  ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    return Results.Ok();

});

app.Run();

public static class ProductRepository{

    public static List<Product> Products{get; set; }

    public static void Add(Product product){
        if(Products == null)
            Products = new List<Product>();

            Products.Add(product);      
        
        }
         
    
    public static Product GetBy(string code){
        return Products.FirstOrDefault(p => p.Code == code);
    
    }

     //metodo de remoção
    public static void Remove(Product product){

        Products.Remove(product);
    }
    

}

public class Product {
    public int id {get; set; }
    public String Code { get; set; }
    public String  Name { get; set; }
}

public class ApplicationDbContext : DbContext{ 
public DbSet<Product> Products { get; set; }

protected override  void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlServer("Server=localhost/sqlexpress;Database=Produtos;User Id=sa;Password=sa@2019;MultipleActiveResultsSets-true;Encrypt=YES;TrustServerCertificate=YES");
    
     
     
}