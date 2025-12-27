using FluentValidation.Results;

namespace LootTradeServices
{
    public class ValidatorResponse
    {
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
    }
}
