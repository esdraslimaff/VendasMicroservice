using Vendas.Inventory.Domain.Entities;

namespace Vendas.Inventory.Tests.Domain;

public class ProductDomainTests
{

    [Fact]
    public void Deve_Criar_Produto_Valido()
    {
        string name = "Produto de teste";
        string description = "Esse produto é para um teste básico";
        decimal price = 100.20m;
        int quantity = 20;

        Product produto = new Product(name, description, price, quantity);

        Assert.NotNull(produto);
        Assert.Equal(name, produto.Name);
        Assert.Equal(description, produto.Description);
        Assert.Equal(price, produto.Price);
        Assert.Equal(quantity, produto.StockQuantity);
    }

    [Fact]
    public void Nao_Deve_Criar_Produto_Com_Preco_Negativo()
    {
        Assert.Throws<ArgumentException>(() =>
            new Product("Produto", "Desc", -10, 5)
        );
    }
}
