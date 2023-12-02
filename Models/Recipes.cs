using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace RecipesAPI.Models
{
    public class Recipes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string RecipeName { get; set; } = null!;

        public List<string> ingredients { get; set; } = null!;

        public string instructions { get; set; } = null!;

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<string> recipeIds {  get; set; } = null!; 
    }
}
