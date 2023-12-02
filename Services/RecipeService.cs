using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RecipesAPI.Models;
using RecipesAPI.Controllers;

namespace RecipesAPI.Services
{
    public class RecipeService
    {
        private readonly IMongoCollection<Recipes> _recipeCollection;

        public RecipeService(IOptionsMonitor<RecipeDatabaseSettings> recipeDatabaseSettings)
        {
            var mongoClient = new MongoClient(recipeDatabaseSettings.CurrentValue.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(recipeDatabaseSettings.CurrentValue.DatabaseName);

            _recipeCollection = mongoDatabase.GetCollection<Recipes>(recipeDatabaseSettings.CurrentValue.RecipeCollectionName);
        }

        public async Task CreateAsync(Recipes recipe) 
        {
            await _recipeCollection.InsertOneAsync(recipe);
            return;
        }

        public async Task<List<Recipes>> GetAsync()
        {
            return await _recipeCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddtoRecipesAsync(string id, string recipeId)
        {
            FilterDefinition<Recipes> filter = Builders<Recipes>.Filter.Eq("Id", id);
            UpdateDefinition<Recipes> update = Builders<Recipes>.Update.AddToSet<string>("items", recipeId);
            await _recipeCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Recipes> filter = Builders<Recipes>.Filter.Eq("Id", id);
            await _recipeCollection.DeleteOneAsync(filter);
            return;
        }
            
    }
}
