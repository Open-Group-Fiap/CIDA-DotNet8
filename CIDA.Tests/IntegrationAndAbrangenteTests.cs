using CIDA.Api.Repository;
using Cida.Data;
using CIDA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIDA.Tests;

public class IntegrationAndAbrangenteTests : IDisposable
{
    private readonly CidaDbContext _context;

    private readonly UsuarioRepository _repository;

    public IntegrationAndAbrangenteTests()
    {
        var options = new DbContextOptionsBuilder<CidaDbContext>()
            .UseInMemoryDatabase("IntegrationAndAbrangenteTests")
            .Options;

        _context = new CidaDbContext(options);
        _context.Database.EnsureDeleted();

        _repository = new UsuarioRepository(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task CreateUsuarioAsync_ShouldCreateUsuario()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "12345678901",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        };

        var createdUsuario = await _repository.CreateUsuarioAsync(usuario);

        Assert.NotNull(createdUsuario);
    }

    [Fact]
    public async Task GetUsuariosByNomeAsync_ShouldReturnUsuario()
    {
        _context.Usuarios.AddRange(Enumerable.Range(10, 5).Select(x => new Usuario
        {
            IdUsuario = x,
            IdAutenticacao = x,
            Nome = $"Leonardo",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = $"1234567890{x}",
            Telefone = $"1234567890{x}",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        }));

        _context.Usuarios.Add(new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "(11) 99999-9999",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        });

        await _context.SaveChangesAsync();

        var usuarios = await _repository.GetUsuariosByNomeAsync("Leonardo");

        Assert.NotEmpty(usuarios);
        Assert.Equal(5, usuarios.Count());
    }

    [Fact]
    public async Task GetUsuarioByNumDocumentoAsync_ShouldReturnUsuario()
    {
        _context.Usuarios.AddRange(Enumerable.Range(10, 5).Select(x => new Usuario
        {
            IdUsuario = x,
            IdAutenticacao = x,
            Nome = $"Leonardo",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = $"1234567890{x}",
            Telefone = $"1234567890{x}",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        }));

        _context.Usuarios.Add(new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "(11) 99999-9999",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        });

        await _context.SaveChangesAsync();

        var usuario = await _repository.GetUsuarioByNumDocumentoAsync("12345678901");

        Assert.NotNull(usuario);
    }

    [Fact]
    public async Task GetUsuarioByTelefoneAsync_ShouldReturnUsuario()
    {
        _context.Usuarios.AddRange(Enumerable.Range(10, 5).Select(x => new Usuario
        {
            IdUsuario = x,
            IdAutenticacao = x,
            Nome = $"Leonardo",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = $"1234567890{x}",
            Telefone = $"1234567890{x}",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        }));

        _context.Usuarios.Add(new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "(11) 99999-9999",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        });

        await _context.SaveChangesAsync();

        var usuario = await _repository.GetUsuarioByTelefoneAsync("(11) 99999-9999");

        Assert.NotNull(usuario);
    }

    [Fact]
    public async Task GetUsuariosAsync_ShouldReturnUsuarios()
    {
        _context.Usuarios.AddRange(Enumerable.Range(10, 5).Select(x => new Usuario
        {
            IdUsuario = x,
            IdAutenticacao = x,
            Nome = $"Leonardo",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = $"1234567890{x}",
            Telefone = $"1234567890{x}",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        }));

        _context.Usuarios.Add(new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "(11) 99999-9999",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        });

        await _context.SaveChangesAsync();

        var usuarios = await _repository.GetUsuariosAsync();

        Assert.NotEmpty(usuarios);
        Assert.Equal(6, usuarios.Count());
    }

    [Fact]
    public async Task UpdateUsuarioAsync_ShouldUpdateUsuario()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "12345678901",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        };

        var createdUsuario = await _repository.CreateUsuarioAsync(usuario);

        createdUsuario.Nome = "Teste Update";

        var updatedUsuario = await _repository.UpdateUsuarioAsync(createdUsuario);

        Assert.Equal("Teste Update", updatedUsuario.Nome);
    }

    [Fact]
    public async Task DeleteUsuarioAsync_ShouldDeleteUsuario()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "12345678901",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        };

        var createdUsuario = await _repository.CreateUsuarioAsync(usuario);

        await _repository.DeleteUsuarioAsync(createdUsuario.IdUsuario);

        var deletedUsuario = await _repository.GetUsuarioAsync(createdUsuario.IdUsuario);

        Assert.Null(deletedUsuario);
    }

    [Fact]
    public async Task GetUsuarioAsync_ShouldReturnUsuario()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "12345678901",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        };

        var createdUsuario = await _repository.CreateUsuarioAsync(usuario);

        var getUsuario = await _repository.GetUsuarioAsync(createdUsuario.IdUsuario);

        Assert.NotNull(getUsuario);
    }

    [Fact]
    public async Task GetUsuarioAsync_ShouldReturnNull()
    {
        var usuario = new Usuario
        {
            IdUsuario = 1,
            IdAutenticacao = 1,
            Nome = "Teste",
            TipoDocumento = TipoDocumento.CPF,
            NumDocumento = "12345678901",
            Telefone = "12345678901",
            DataCriacao = DateTime.Now,
            Status = Status.ativo
        };

        var createdUsuario = await _repository.CreateUsuarioAsync(usuario);

        var getUsuario = await _repository.GetUsuarioAsync(2);

        Assert.Null(getUsuario);
    }
}