namespace WhatIsInIt.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public record PromptPayload(string Prompt, string InteractionId, string? MessageId);
    public record Interaction(string Id, string Title, string? Description, List<InteractionMessage>? messages);
    public record InteractionMessage(string Id, string Message, string From, DateTime? Time, string? Type, object? Data);
}