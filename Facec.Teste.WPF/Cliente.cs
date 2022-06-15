using System;
using System.Text.Json.Serialization;

namespace Facec.Teste.WPF
{
    public class Cliente
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("documento")]
        public string Documento { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        public Cliente()
        {

        }

        public Cliente(string documento, string nome)
        {
            Documento = documento;
            Nome = nome;
        }
        [JsonConstructor]
        public Cliente(Guid id, string documento, string nome)
        {
            Id = id;
            Documento = documento;
            Nome = nome;
        }
    }
}