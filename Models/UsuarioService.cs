using System.Linq;
using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class UsuarioService
    {

        public List<Usuario> Listar(){

            using(BibliotecaContext bc = new BibliotecaContext()){

                return bc.Usuarios.ToList();

            }

        }

        public Usuario Listar(int id){

            using(BibliotecaContext bc = new BibliotecaContext()){

                return bc.Usuarios.Find(id);

            }

        }

        public void incluirUsuario(Usuario NovoUser){

            using(BibliotecaContext bc = new BibliotecaContext()){

                bc.Usuarios.Add(NovoUser);
                bc.SaveChanges();

            }

        }

        public void editarUsuario(Usuario U){

            using(BibliotecaContext bc = new BibliotecaContext()){

                Usuario Usuario = bc.Usuarios.Find(U.Id);

                Usuario.Nome = U.Nome;
                Usuario.Login = U.Login;
                Usuario.Senha = U.Senha;

                bc.SaveChanges();

            }

        }

        public void excluirUsuario(int id){

            using(BibliotecaContext bc = new BibliotecaContext()){

                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();

            }

        }
    }
}