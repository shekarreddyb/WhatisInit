using Microsoft.AspNetCore.Mvc;
using WhatIsInIt.Models;
using WhatIsInIt.Services;

namespace WhatIsInIt.Endpoints
{
    public static class InteractionEndpoints
    {
        public static void MapInteractions(this WebApplication app)
        {
            var group = app.MapGroup("api/interactions")
                .WithGroupName("Interactions");

            group.MapGet("/", async ([FromServices] IInteractionService svc) =>
            {
                var interactions = await svc.GetInteractions();
                return interactions;
            }).WithName("GetAllInteractions");

            group.MapGet("/{id}", async (string id, [FromServices] IInteractionService svc) =>
            {
                var interactions = await svc.GetInteraction(id);
                return interactions;
            }).WithName("GetInteractionById");

            group.MapDelete("/{id}", ([FromServices] IInteractionService svc) =>
            {
            }).WithName("GeDeleteInteractionById");

            group.MapPost("/{id}/prompt",
                        async ([FromBody] PromptPayload prompt,
                        [FromRoute] string id,
                        [FromServices] IInteractionService svc) =>
            {
                return await svc.SendPrompt(id, prompt.Prompt);
            }).WithName("PromptInteractionById");
        }
    }
}