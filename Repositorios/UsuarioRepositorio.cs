using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _DbContext;



        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _DbContext = sistemaTarefasDBContext;
        }


        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _DbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscasTodosUsuarios()
        {
            return await _DbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _DbContext.Usuarios.AddAsync(usuario);
           await _DbContext.SaveChangesAsync();
            return usuario;

        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"O usuário com ID: {id} não foi encontrado.");
            }
            usuarioPorId.Name = usuario.Name;
            usuarioPorId.Email = usuario.Email;
            _DbContext.Usuarios.Update(usuarioPorId);
            await _DbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"O usuário com ID: {id} não foi encontrado.");
            }

            _DbContext.Usuarios.Remove(usuarioPorId);
           await _DbContext.SaveChangesAsync();
            return true;
        }


    }
}
