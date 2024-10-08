using MongoDB.Bson;
public class Carro
{
    public ObjectId Id { get; set; }
    public string Modelo { get; set; }
    public string Marca { get; set; }
    public int Ano { get; set; }
}
