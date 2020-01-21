using CsvHelper.Configuration.Attributes;

namespace Negative_Association_Rules_Miner.model
{
    public class ExampleModel : ICsvModel
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("name")]
        public string Name { get; set; }
        [Name("surname")]
        public string Surname { get; set; }
    }
}
