using FluentValidation.Results;
using System.Text.Json.Serialization;
namespace TesteGenialNet.Business.Interop.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonIgnore]
        public bool IsValid => ValidationResult.IsValid;
    }
}
