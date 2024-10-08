using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CarrosController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public CarrosController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Carro>>> Get()
    {
        var produtos = await _mongoDBService.ObterTodos();
        return produtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Carro>> Get(string id)
    {
        var produto = await _mongoDBService.ObterPorId(id);

        if (produto == null)
        {
            return NotFound();
        }

        return produto;
    }

    [HttpPost]
    public async Task<ActionResult<Carro>> Post(Carro carro)
    {
        await _mongoDBService.Iniciar(carro);
        return CreatedAtAction("Get", new { id = carro.Id }, carro);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Carro carro)
    {
        if (id != carro.Id.ToString())
        {
            return BadRequest();
        }

        await _mongoDBService.Load(carro);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(string id)
    {
        await _mongoDBService.Remover(id);
        return NoContent();
    }
}