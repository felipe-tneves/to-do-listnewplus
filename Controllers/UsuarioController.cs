using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Tarefas.Mvc.Web.Models;
using Senai.Tarefas.Mvc.Web.Repositorios;

namespace Senai.Tarefas.Mvc.Web.Controllers {
    public class UsuarioController : Controller {

        [HttpGet]
        public ActionResult Cadastro () {
            return View ();
        }

        [HttpPost]
        public ActionResult Cadastro (IFormCollection form) {

            UsuarioModel usuarioModel = new UsuarioModel ();

            if (System.IO.File.Exists ("usuario.csv")) {
                String[] linhas = System.IO.File.ReadAllLines ("usuario.csv");
                usuarioModel.ID = linhas.Length + 1;
            } else {
                usuarioModel.ID = 1;
            }

            usuarioModel.Nome = form["nome"];
            usuarioModel.Email = form["email"];
            usuarioModel.Senha = form["senha"];
            usuarioModel.Tipo = form["tipo"];
            usuarioModel.DataNascimento = DateTime.Parse (form["nascimento"]);

            using (StreamWriter sw = new StreamWriter ("usuario.csv", true)) {
                sw.WriteLine ($"{usuarioModel.ID};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha};{usuarioModel.Tipo};{usuarioModel.DataNascimento}");
            }

            ViewBag.Mensagem = "Usuário Cadastrado";

            return View ();
        }

        [HttpGet]

        public IActionResult Login () => View ();

        [HttpPost]

        public IActionResult Login (IFormCollection form) {

            UsuarioModel usuario = new UsuarioModel {

                Email = form["email"],
                Senha = form["senha"]

            };

            UsuarioRepositorio usuarioRep = new UsuarioRepositorio ();
            UsuarioModel usuarioModel = usuarioRep.Buscar (usuario.Email, usuario.Senha);

            if (usuarioModel != null) {
                HttpContext.Session.SetInt32 ("idUsuario", usuarioModel.ID);

                ViewBag.Mensagem = "Login realizado com sucesso!";

                return RedirectToAction ("Cadastrar", "Tarefas");

            } else {

                ViewBag.Mensagem = "Acesso negado";
            }

            return View ();
        }
    }
}