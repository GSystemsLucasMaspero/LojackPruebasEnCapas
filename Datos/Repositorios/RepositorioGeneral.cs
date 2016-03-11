using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioGeneral
    {
        private Lojack_PruebaEntities DB = new Lojack_PruebaEntities();

        public DateTime ObtenerDateTimeServer()
        {
            return DB.Database.SqlQuery<DateTime>("SELECT GETDATE()").AsEnumerable().First();
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return DB.Usuarios.ToList();
        }

        public void AgregarUsuario(String userName, String encryptedPassword, String name, String surname)
        {
            Usuario userToAdd = new Usuario();

            userToAdd.userLogin = userName;
            userToAdd.nombre = name;
            userToAdd.apellido = surname;
            userToAdd.password = encryptedPassword;

            // Valores por defecto
            userToAdd.idCliente = 1;
            userToAdd.idSector = 1;
            userToAdd.operador = false;
            userToAdd.supervisor = false;
            userToAdd.fechaAlta = this.ObtenerDateTimeServer();
            userToAdd.fechaModificacion = this.ObtenerDateTimeServer();
            userToAdd.usuarioAlta = 20;
            userToAdd.usuarioModificacion = 20;
            userToAdd.nivelAuditoria = 1;
            userToAdd.perfilWindows = 1;
            userToAdd.perfilWeb = 1;
            userToAdd.multipais = false;
            userToAdd.demo = false;
            userToAdd.diasDemo = 1;
            userToAdd.idCuenta = 1;

            DB.Usuarios.Add(userToAdd);
            DB.SaveChanges();
        }

        public Usuario ObtenerUsuarioPorNombre(String userLogin)
        {
            return DB.Usuarios.FirstOrDefault(u => u.userLogin == userLogin);
        }

    }
}
