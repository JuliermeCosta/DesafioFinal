using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DesafioFinal.Application.DTO.Common.Converters
{
    public class CpfConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string entrada = reader.GetString()!;

            if (!string.IsNullOrWhiteSpace(entrada) && entrada.Length == 11)
                entrada = Regex.Replace(entrada, @"\D", string.Empty);

            return entrada;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrWhiteSpace(value) && long.TryParse(value, out long cpf))
            {
                //Escreve o CPF formatado
                writer.WriteStringValue(cpf.ToString(@"000\.000\.000\-00"));
            }
            else
            {
                writer.WriteStringValue($"CPF inválido: {value}");
            }
        }
    }
}
