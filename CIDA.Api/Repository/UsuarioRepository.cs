using Cida.Data;
using CIDA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIDA.Api.Repository;

public class UsuarioRepository
{
    private readonly CidaDbContext _context;

    public UsuarioRepository(CidaDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetUsuarioAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosByNomeAsync(string nome)
    {
        return await _context.Usuarios.Where(x => x.Nome == nome).ToListAsync();
    }

    public async Task<Usuario> GetUsuarioByNumDocumentoAsync(string numDocumento)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.NumDocumento == numDocumento);
    }

    public async Task<Usuario> GetUsuarioByTelefoneAsync(string telefone)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Telefone == telefone);
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}