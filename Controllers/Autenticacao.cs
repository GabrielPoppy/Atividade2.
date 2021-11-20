using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Biblioteca.Models;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static void verificaSeUsuarioAdminExsite(BibliotecaContext bc){

            IQueryable<Usuario> userEncontrato = bc.Usuarios.Where(u => u.Login == "Admin");

            if(userEncontrato.ToList().Count==0){

                Usuario Admin = new Usuario();
                Admin.Login = "Admin";
                Admin.Senha = Criptografo.TextoCriptografado("123");
                Admin.Tipo = Usuario.ADMIN;
                Admin.Nome = "Administrador";

                bc.Usuarios.Add(Admin);
                bc.SaveChanges();
            }


        }

        public void incluirUsuario(Usuario novoUser){

            using(BibliotecaContext bc = new BibliotecaContext()){
            
                bc.Usuarios.Add(novoUser);
                bc.SaveChanges();
                
            }

        }

        public static bool verificaLoginSenha(string login, string senha, Controller controller){

             using(BibliotecaContext bc = new BibliotecaContext()){

                verificaSeUsuarioAdminExsite(bc);

                senha = Criptografo.TextoCriptografado(senha);

                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login == login && u.Senha==senha);
                List<Usuario> ListaUsuarioEncontrado = UsuarioEncontrado.ToList();

                if(ListaUsuarioEncontrado.Count == 0){

                    return false;

                }else{

                    controller.HttpContext.Session.SetString("login", ListaUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListaUsuarioEncontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", ListaUsuarioEncontrado[0].Tipo);

                    return true;

                }

            }

        }

        public static void verificaSeUsuarioEAdmin(Controller controller){

            if(!(controller.HttpContext.Session.GetInt32("Tipo") == Usuario.ADMIN)){

                controller.Request.HttpContext.Response.Redirect("/Usuario/NeedAdmin");

            }

        }
    }
}