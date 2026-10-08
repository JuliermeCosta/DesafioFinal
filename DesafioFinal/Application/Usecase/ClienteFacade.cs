using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Cliente;
using DesafioFinal.Application.Mapper;
using DesafioFinal.Application.Port.In;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;
using DesafioFinal.Domain.Service;

namespace DesafioFinal.Application.Usecase
{
    public class ClienteFacade(IClienteRepository clienteRepository, ClienteValidatorService validatorService) : IClienteFacade
    {
        public async Task<ResultResponse> CriarAsync(ClientePostDto dto, CancellationToken cancellationToken)
        {
            //1. Mapeamento
            var cliente = ClienteMapper.ToModel(dto);

            //2. Validação das Regras de Negócio
            var erros = await validatorService.ValidarCliente(cliente, cancellationToken);
            if (erros.Count > 0)
                return ResultResponse.CriarFalha(erros, "Erro ao cadastrar cliente.");

            //3. Persistência
            await clienteRepository.AdicionarAsync(cliente, cancellationToken);

            //4. Retorno de Sucesso com o ID atualizado
            return ResultResponse.CriarSucesso($"ID: {cliente.Id}", "Cliente cadastrado com sucesso.");
        }

        public async Task<ResultResponse> AtualizarCompletoAsync(ClientePutDto dto, long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Cliente? cliente = await clienteRepository.ObterPorIdAsync(id, cancellationToken);

            if (cliente == null)
                return ResultResponse.CriarFalha(["Cliente não encontrado"], "Erro ao atualizar cliente.");

            //2. Validação de Alteração
            if (cliente.Nome.Equals(dto.Nome) && cliente.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && cliente.Celular == dto.Celular)
                return ResultResponse.CriarFalha(["Nenhum dado foi alterado."], "Erro ao atualizar cliente.");

            //3. Modificação dos Campos
            cliente.Nome = dto.Nome!;
            cliente.Email = dto.Email!;
            cliente.Celular = dto.Celular;

            return await AtualizarAsync(cliente, cancellationToken);
        }

        public async Task<ResultResponse> AtualizarParcialAsync(ClientePatchDto dto, long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente           
            Cliente? cliente = await clienteRepository.ObterPorIdAsync(id, cancellationToken);

            if (cliente == null)
                return ResultResponse.CriarFalha(["Cliente não encontrado"], "Erro ao atualizar cliente.");

            bool houveAlteracao = false;

            //2. Modificação e Comparação dos Campos Informados
            if (!string.IsNullOrWhiteSpace(dto.Nome) && !dto.Nome.Equals(cliente.Nome))
            {
                houveAlteracao = true;
                cliente.Nome = dto.Nome;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email) && !dto.Email.Equals(cliente.Email, StringComparison.OrdinalIgnoreCase))
            {
                houveAlteracao = true;
                cliente.Email = dto.Email;
            }

            if (dto.CelularFoiInformado && dto.Celular != cliente.Celular)
            {
                houveAlteracao = true;
                cliente.Celular = dto.Celular;
            }

            if (!houveAlteracao)
                return ResultResponse.CriarFalha(["Não houve alteração"], "Erro ao atualizar cliente.");

            return await AtualizarAsync(cliente, cancellationToken);
        }

        private async Task<ResultResponse> AtualizarAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            //2. Validação das Regras de Negócio
            var erros = await validatorService.ValidarCliente(cliente, cancellationToken);
            if (erros.Count > 0)
                return ResultResponse.CriarFalha(erros, "Erro ao atualizar cliente.");

            //3. Persistência
            await clienteRepository.AtualizarAsync(cliente, cancellationToken);

            //4. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"ID: {cliente.Id}", "Cliente atualizado com sucesso.");
        }

        public async Task<ResultResponse> ApagarAsync(long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Cliente? cliente = await clienteRepository.ObterPorIdAsync(id, cancellationToken);

            if (cliente == null)
                return ResultResponse.CriarFalha(["Cliente não encontrado"], "Erro ao atualizar cliente.");

            //2. Apaga o registro persistido
            await clienteRepository.RemoverAsync(cliente.Id, cancellationToken);

            //3. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"ID: {cliente.Id}", "Cliente apagado com sucesso.");
        }

        public async Task<ResultResponse> ObterQuantidadeTotalAsync(CancellationToken cancellationToken)
        {
            //1. Obtém a contagem de clientes
            int contagem = await clienteRepository.ObterContagemAsync(cancellationToken);

            //2. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"Contagem: {contagem}", "Contagem de clientes realizada com sucesso.");
        }

        public async Task<List<ClienteResponseDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Cliente> listaClientes = await clienteRepository.ObterTodosAsync(cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaClientes.Select(ClienteMapper.ToDto).ToList();

            //3. Retorno da lista de clientes
            return listaRetorno;
        }

        public async Task<ClienteResponseDto> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Cliente? cliente = await clienteRepository.ObterPorIdAsync(id, cancellationToken);

            //2. Mapeia e retorna o cliente
            return ClienteMapper.ToDto(cliente!);
        }

        public async Task<List<ClienteResponseDto>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Cliente> listaClientes = await clienteRepository.ObterPorNomeAsync(nome, cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaClientes.Select(ClienteMapper.ToDto).ToList();

            //3. Retorno da lista de clientes
            return listaRetorno;
        }

        public async Task<ClienteResponseDto> ObterPorCpfAsync(long cpf, CancellationToken cancellationToken)
        {
            //1. Valida se o CPF inserido é válido
            var erros = validatorService.ValidarCpf(cpf);
            if (erros.Count > 0)
                return null!;

            //2. Busca da Entidade Existente
            Cliente? cliente = await clienteRepository.ObterPorCpfAsync(cpf, cancellationToken);

            //3. Mapeia e retorna o cliente
            return ClienteMapper.ToDto(cliente!);
        }
    }
}