using MongoDB.Driver;
using POKEMON.Models;


public class PokemonService : IPokemonService{
    // private Dictionary<string, Pokemon> pokeMen = new Dictionary<string, Pokemon>();

    private readonly IMongoCollection<Pokemon> _pokemonCollection;

    public PokemonService(IConfiguration configuration){
        var client = new MongoClient(configuration["MongoDBSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDBSettings:DatabaseName"]);
        _pokemonCollection = database.GetCollection<Pokemon>(configuration["MongoDBSettings:CollectionName"]);
    }

    public async Task AddPokemon(Pokemon pokemon){
        // pokeMen[pokemon.Id] = pokemon;
        await _pokemonCollection.InsertOneAsync(pokemon);
    }

    public async Task<Pokemon> GetPokemon(string id){
        var result = await _pokemonCollection.Find<Pokemon>(pM => pM.Id == id).FirstOrDefaultAsync();
        if (result == null){
            throw new Exception("Pokemon not found");
        }
        return result;
    }    

    public async Task<List<Pokemon>> GetAllPokemons(){
        return await _pokemonCollection.Find<Pokemon>(pM => true).ToListAsync();
    }

    public async Task UpdatePokemon(string id, Pokemon pokemon){
        await _pokemonCollection.ReplaceOneAsync(pM => pM.Id == id, pokemon);
    }

    public async Task DeletePokemon(string id){
        await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
    }

    public async Task<Pokemon> GetPokemonByName(string name){
        return await _pokemonCollection.Find<Pokemon>(pM => pM.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<Pokemon>> GetPokemonsByType(string type){
        return await _pokemonCollection.Find<Pokemon>(pM => pM.Type == type).ToListAsync();
    }
    
}