using FluentValidation.Results;
using System.Text.Json.Serialization;
namespace TesteGenialNet.Business.Commands
{
    public class BaseCommand
    {
        public int Id { get; set; }
    }
}
