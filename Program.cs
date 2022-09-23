using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//metodo de envio de parametro
app.MapPost("/product", (Product product) => { ProductRepository.Add(product); });

//Metodo de recebimento de dados
app.MapGet("/product/{code}", ([FromRoute]string code) => {
    var product =  ProductRepository.GetBy(code);
    return product;
});

//Metodo de alteração do produto
app.MapPut("/product", (Product product) => {
    var productSaved =  ProductRepository.GetBy(product.Code);
    productSaved.Name =  product.Name;
});

app.MapDelete("/product/{code}", ([FromRoute]string code) =>{

    var productSaved =  ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);

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