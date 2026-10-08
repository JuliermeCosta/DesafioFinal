using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Domain.Service
{
    public class ClienteValidatorService(IClienteRepository clienteRepository)
    {
        public async Task<List<string>> ValidarCliente(Cliente cliente, CancellationToken cancellationToken = default)
        {
            var erros = new List<string>();

            //Validações realizadas somente em novos clientes
            if (cliente.Id == 0)
            {
                //1. Validar CPF
                erros.AddRange(ValidarCpf(cliente.Cpf));

                //2. Validar unicidade do CPF no banco
                var clienteExistenteCpf = await clienteRepository.ObterPorCpfAsync(cliente.Cpf, cancellationToken);
                if (clienteExistenteCpf != null)
                    erros.Add("Já existe um cliente cadastrado com este CPF.");
            }

            //3. Validar unicidade do e-mail no banco
            var clienteExistenteEmail = await clienteRepository.ObterPorEmailAsync(cliente.Email, cancellationToken);
            if (clienteExistenteEmail != null && clienteExistenteEmail.Any(c => c.Id != cliente.Id))
                erros.Add("Já existe um cliente cadastrado com este e-mail.");

            return erros;
        }

        public List<string> ValidarCpf(long cpf)
        {
            var erros = new List<string>();

            if (!ValidarDígitosCpf(cpf))
                erros.Add("O CPF informado é inválido.");

            return erros;
        }

        private static bool ValidarDígitosCpf(long cpf)
        {
            string cpfStr = cpf.ToString("D11");

            if (cpfStr.Length != 11 || cpfStr.Distinct().Count() == 1)
                return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpfStr)
                    return false;

            string tempCpf = cpfStr[..9];
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpfStr.EndsWith(digito);
        }
    }
}
