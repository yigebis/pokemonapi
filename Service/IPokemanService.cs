using POKEMON.Models;

public interface IPokemonService
{
    public Task AddPokemon(Pokemon Pokemon);
    public Task DeletePokemon(String id);
    public Task<Pokemon> GetPokemon(string id);
    public Task<List<Pokemon>> GetAllPokemons();
    public Task UpdatePokemon(string id, Pokemon pokemon);
    public Task<Pokemon> GetPokemonByName(string name);
    public Task<List<Pokemon>> GetPokemonsByType(string type);
}