var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public class Product {
    public int id {get; set; }
    public String Code { get; set; }
    public String  Name { get; set; }
}