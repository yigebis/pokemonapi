using Microsoft.AspNetCore.Mvc;
using POKEMON.Models;

namespace POKEmon.Controller
{
    [ApiController]
    [Route("[Controller]")]

    public class PokemonController : ControllerBase{

        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService _ps){
            this._pokemonService = _ps;
        }

        [HttpPost("/pokemon/add")]
        public async Task<ActionResult> AddPokemon(Pokemon pokemon){
            if (!ModelState.IsValid){
                return BadRequest("invalid pokemon data");
            }
            try{
                //only allow id generation by database
                pokemon.Id = "";

                await _pokemonService.AddPokemon(pokemon);
                return Ok("Pokemon added successfully");
            }
            catch{
                return BadRequest("Failed to add Pokemon");
            }
        }

        [HttpGet("/pokemon/all")]
        public async Task<ActionResult> GetAllPokemons(){
            try{
                return Ok(await _pokemonService.GetAllPokemons());
            }
            catch{
                return NotFound("No pokemon found");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id){
            try{
                return Ok(await _pokemonService.GetPokemon(id));
            }
            catch(Exception e){
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePokemon(string id, Pokemon pokemon){
            try{
                await _pokemonService.UpdatePokemon(id, pokemon);
                return Ok("pokemon updated successfully");
            }
            catch{
                return NotFound("pokemon not found");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePokemon(string id){
            try{
                await _pokemonService.DeletePokemon(id);
                return Ok("pokemon deleted successfully");
            }
            catch{
                return BadRequest("pokemon not found");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult> GetPokemonByName(string name){
            try{
                return Ok(await _pokemonService.GetPokemonByName(name));
            }
            catch{
                return NotFound("Pokemon not found");
            }
        }
        
        [HttpGet("type/{type}")]
        public async Task<ActionResult> GetPokemonsByType(string type){
            try{
                return Ok(await _pokemonService.GetPokemonsByType(type));
            }
            catch{
                return NotFound("No pokemon found");
            }
        }

    }
}


