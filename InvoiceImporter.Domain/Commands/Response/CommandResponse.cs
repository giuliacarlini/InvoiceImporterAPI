using ImporterInvoice.Domain.Shared.Commands;

namespace InvoiceImporter.Domain.Commands.Response
{
    public class CommandResponse : ICommandResult
    {
        public CommandResponse() { }

        public CommandResponse(bool success, string message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }

    }
}
