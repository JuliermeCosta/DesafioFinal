using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO
{
    public class ResultResponse
    {
        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; init; }

        [JsonPropertyName("mensagem")]
        public string Mensagem { get; init; } = string.Empty;

        [JsonPropertyName("complemento")]
        public string? Complemento { get; init; }

        [JsonPropertyName("erros")]
        public List<string>? Erros { get; init; }

        public static ResultResponse CriarSucesso(string complemento, string mensagem = "Operação realizada com sucesso.")
        {
            return new ResultResponse
            {
                Sucesso = true,
                Mensagem = mensagem,
                Complemento = complemento
            };
        }

        public static ResultResponse CriarFalha(List<string> erros, string mensagem = "Ocorreu um erro ao processar a solicitação.")
        {
            return new ResultResponse
            {
                Sucesso = false,
                Mensagem = mensagem,
                Erros = erros
            };
        }

        public static ResultResponse CriarFalha(Exception exception, string mensagem = "Ocorreu um erro ao processar a solicitação.")
        {
            return new ResultResponse
            {
                Sucesso = false,
                Mensagem = mensagem,
                Erros = [exception.Message]
            };
        }
    }
}
