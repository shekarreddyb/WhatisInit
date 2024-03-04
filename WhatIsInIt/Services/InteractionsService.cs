using System.Text.Json;
using System;
using WhatIsInIt.Models;

namespace WhatIsInIt.Services
{
    public interface IInteractionService
    {
        Task<IEnumerable<Interaction>> GetInteractions();

        Task<IEnumerable<InteractionMessage>> GetInteraction(string id);

        Task<InteractionMessage> SendPrompt(string interactionId, string prompt);
    }

    public class InteractionsService : IInteractionService
    {
        public async Task<IEnumerable<InteractionMessage>> GetInteraction(string id)
        {
            var pathToJson = Path.Combine(Directory.GetCurrentDirectory(), "StaticData", $"messages-{id}.json");
            // Read the JSON file and deserialize it to the Person record
            var jsonString = await File.ReadAllTextAsync(pathToJson);
            var interactions = JsonSerializer.Deserialize<List<InteractionMessage>>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return interactions;
        }

        public async Task<IEnumerable<Interaction>> GetInteractions()
        {
            var pathToJson = Path.Combine(Directory.GetCurrentDirectory(), "StaticData", "interactions.json");
            // Read the JSON file and deserialize it to the Person record
            var jsonString = await File.ReadAllTextAsync(pathToJson);
            var interactions = JsonSerializer.Deserialize<List<Interaction>>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return interactions;
        }

        public async Task<InteractionMessage> SendPrompt(string interactionId, string prompt)
        {
            var interactions = (await GetInteraction(interactionId)).ToList();
            var message = new InteractionMessage(Guid.NewGuid().ToString(), prompt, "user", null, null, null);
            interactions.Add(message);
            var pathToJson = Path.Combine(Directory.GetCurrentDirectory(), "StaticData", $"messages-{interactionId}.json");
            // Read the JSON file and deserialize it to the Person record
            var jsonString = JsonSerializer.Serialize<List<InteractionMessage>>(interactions, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await File.WriteAllTextAsync(pathToJson, jsonString);
            return message;
        }
    }
}