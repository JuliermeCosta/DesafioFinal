using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Common.Converters
{
    public class DecimalDuasCasasConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDecimal();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            //Escreve o número formatado com duas casas decimais usando ponto como separador
            writer.WriteRawValue(Math.Round(value, 2).ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
