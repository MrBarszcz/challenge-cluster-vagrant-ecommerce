using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Ecommerce.Web.Controllers;

public class CheckoutController : Controller {
    
    [HttpPost]
    public async Task<IActionResult> Process() {
        // 1. Pega os itens do carrinho na Sessão
        var cartJson = HttpContext.Session.GetString("Cart");
        if (string.IsNullOrEmpty(cartJson) || cartJson == "[]") {
            return RedirectToAction("Index", "Cart");
        }

        var cartIds = JsonSerializer.Deserialize<List<Guid>>(cartJson);

        // 2. Monta o "Evento" de compra que será enviado para a fila
        var checkoutEvent = new {
            OrderId = Guid.NewGuid(),
            VariationIds = cartIds,
            CreatedAt = DateTime.UtcNow,
            Status = "Aguardando Pagamento"
        };

        // 3. Conecta no RabbitMQ e publica a mensagem
        var factory = new ConnectionFactory { 
            HostName = "192.168.56.11",
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER"), 
            Password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS")
        }; 
        
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        // Garante que a fila existe no RabbitMQ antes de mandar a mensagem
        await channel.QueueDeclareAsync(
            queue: "checkout_queue", 
            durable: true, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null
        );

        // Transforma o objeto em bytes para trafegar na rede
        var message = JsonSerializer.Serialize(checkoutEvent);
        var body = Encoding.UTF8.GetBytes(message);

        // Dispara para a fila
        await channel.BasicPublishAsync(
            exchange: "", 
            routingKey: "checkout_queue", 
            body: body
        );

        // 4. Limpa o carrinho do usuário após o envio com sucesso
        HttpContext.Session.Remove("Cart");

        // 5. Envia o ID do pedido para a tela de sucesso
        return View("Success", checkoutEvent.OrderId);
    }
}