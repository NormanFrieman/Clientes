using Clientes.Application.Abstractions.ErrorsMessage;
using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Clientes.Application.Services;
using Clientes.Application.Validators;
using Clientes.Domain.Entities;
using Clientes.Domain.Enums;
using Clientes.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Newtonsoft.Json;

namespace UnitTests.Application
{
    public class ClienteServiceTests
    {
        #region Mocks
        private Mock<IClienteRepository> _clienteRepositoryMock { get; }
        private Mock<ITelefoneRepository> _telefoneRepositoryMock { get; }
        private Mock<IValidator<ClienteDto>> _validatorMock { get; }
        #endregion

        private IClienteService _clienteService { get; }

        public ClienteServiceTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _telefoneRepositoryMock = new Mock<ITelefoneRepository>();

            _validatorMock = new Mock<IValidator<ClienteDto>>();
            _clienteService = new ClienteService(new ClienteValidator(), _clienteRepositoryMock.Object, _telefoneRepositoryMock.Object);
        }

        #region CreateCliente
        [Fact]
        public async Task CreateCliente_Success()
        {
            // Arrange
            var cliente = new Cliente("Cliente Sucesso", "email@email.com", new List<Telefone>() { new("Numero", "ddd", ETelefoneTipo.Fixo) });
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(false);
            _clienteRepositoryMock.Setup(x => x.CreateCliente(It.IsAny<Cliente>())).ReturnsAsync(cliente);

            _telefoneRepositoryMock.Setup(x => x.PhonesAlreadUsed(It.IsAny<IEnumerable<string>>())).ReturnsAsync(Enumerable.Empty<string>());

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ClienteDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            #endregion

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(JsonConvert.SerializeObject(clienteDto), JsonConvert.SerializeObject(result.Body));
        }

        [Fact]
        public async Task CreateCliente_ValidationFailure_NomeVazio()
        {
            // Arrange
            var clienteDto = new ClienteDto(string.Empty, "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("É necessário informar o nome do cliente", result.Error.Messages.Single());
        }

        [Fact]
        public async Task CreateCliente_ValidationFailure_EmailVazio()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", string.Empty, new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("É necessário informar o email do cliente", result.Error.Messages.Single());
        }

        [Fact]
        public async Task CreateCliente_ValidationFailure_EmailInvalido()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", "email_invalido", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email informado não está em um formato válido", result.Error.Messages.Single());
        }

        [Fact]
        public async Task CreateCliente_ValidationFailure_TelefoneVazio()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", Enumerable.Empty<TelefoneDto>());

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("É necessário informar pelo menos um telefone", result.Error.Messages.Single());
        }

        [Fact]
        public async Task CreateCliente_EmailAlreadyUsed()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("9999999999", "27", TelefoneTipo.FIXO) });

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(true);
            _clienteRepositoryMock.Setup(x => x.CreateCliente(It.IsAny<Cliente>())).ReturnsAsync(It.IsAny<Cliente>());

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ClienteDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            #endregion

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ClienteErrors.EmailAlreadyUsed.Messages.Single(), result.Error.Messages.Single());
        }

        [Fact]
        public async Task CreateCliente_PhonesAlreadUsed()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });
            var telefones = new string[] { "279999999999" };

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(false);
            _clienteRepositoryMock.Setup(x => x.CreateCliente(It.IsAny<Cliente>())).ReturnsAsync(It.IsAny<Cliente>());

            _telefoneRepositoryMock.Setup(x => x.PhonesAlreadUsed(It.IsAny<IEnumerable<string>>())).ReturnsAsync(telefones);

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<ClienteDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            #endregion

            // Act
            var result = await _clienteService.CreateCliente(clienteDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(TelefoneErros.PhonesAlreadyUsed(telefones).Messages.Single(), result.Error.Messages.Single());
        }
        #endregion

        #region DeleteCliente
        [Fact]
        public async Task DeleteCliente_Success()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(true);
            _clienteRepositoryMock.Setup(x => x.DeleteCliente(It.IsAny<string>()));
            #endregion

            // Act
            var result = await _clienteService.DeleteCliente("ddd");

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteCliente_EmailNotFound()
        {
            // Arrange
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(false);
            _clienteRepositoryMock.Setup(x => x.DeleteCliente(It.IsAny<string>()));
            #endregion

            // Act
            var result = await _clienteService.DeleteCliente("ddd");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ClienteErrors.EmailNotFound.Messages.Single(), result.Error.Messages.Single());

        }
        #endregion

        #region ReadCliente
        [Fact]
        public async Task GetCliente_Success()
        {
            // Arrange
            var cliente = new Cliente("Cliente Sucesso", "email@email.com", new List<Telefone>() { new("Numero", "ddd", ETelefoneTipo.Fixo) });
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.GetClientesAsync(It.IsAny<string>())).ReturnsAsync(new List<Cliente>() { cliente });
            #endregion

            // Act
            var result = await _clienteService.GetClientes("ddd");

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(new List<ClienteDto>() { clienteDto }), JsonConvert.SerializeObject(result));
        }
        #endregion

        #region UpdateCliente
        [Fact]
        public async Task UpdateCliente_Success()
        {
            // Arrange
            var cliente = new Cliente("Cliente Sucesso", "email@email.com", new List<Telefone>() { new("Numero", "ddd", ETelefoneTipo.Fixo) });
            var clienteDto = new ClienteDto("Cliente Sucesso", "email@email.com", new List<TelefoneDto>() { new("Numero", "ddd", TelefoneTipo.FIXO) });

            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.UserExists(It.IsAny<Guid>())).ReturnsAsync(true);
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(false);
            _clienteRepositoryMock.Setup(x => x.UpdateCliente(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(cliente);
            #endregion

            // Act
            var result = await _clienteService.UpdateCliente(clienteDto.Id, "email@email.com");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(JsonConvert.SerializeObject(clienteDto), JsonConvert.SerializeObject(result.Body));
        }

        [Fact]
        public async Task UpdateCliente_UserNotFound()
        {
            // Arrange
            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.UserExists(It.IsAny<Guid>())).ReturnsAsync(false);
            #endregion

            // Act
            var result = await _clienteService.UpdateCliente(Guid.NewGuid(), "email@email.com");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ClienteErrors.UserNotFound.Messages.Single(), result.Error.Messages.Single());
        }

        [Fact]
        public async Task UpdateCliente_EmailAlreadUsed()
        {
            // Arrange
            #region Setup Test
            _clienteRepositoryMock.Setup(x => x.UserExists(It.IsAny<Guid>())).ReturnsAsync(true);
            _clienteRepositoryMock.Setup(x => x.EmailAlreadyUsed(It.IsAny<string>())).ReturnsAsync(true);
            #endregion

            // Act
            var result = await _clienteService.UpdateCliente(Guid.NewGuid(), "email@email.com");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ClienteErrors.EmailAlreadyUsed.Messages.Single(), result.Error.Messages.Single());
        }
        #endregion
    }
}
