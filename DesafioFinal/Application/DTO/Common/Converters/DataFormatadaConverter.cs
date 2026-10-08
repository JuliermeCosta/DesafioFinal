using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Common.Converters
{
    public class DataFormatadaConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            //Escreve a data e horário formatados
            writer.WriteStringValue(value.ToString("dd/MM/yyyy HH:mm:ss.fff"));
        }
    }
}
