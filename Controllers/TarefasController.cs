using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Tarefas.Mvc.Web.Models;

namespace Senai.Tarefas.Mvc.Web.Controllers {
    public class TarefasController : Controller {

        [HttpGet]
        public IActionResult Cadastrar () {

            if (string.IsNullOrEmpty (HttpContext.Session.GetString ("idUsuario"))) {
                return RedirectToAction ("Login", "Usuario");
            }

            return View ();
        }

        [HttpPost]
        public IActionResult Cadastrar (IFormCollection form) {

            UsuarioModel usuario = new UsuarioModel ();
            usuario.ID = HttpContext.Session.GetInt32 ("idUsuario").Value;

            TarefasModel tarefas = new TarefasModel ();

            if (System.IO.File.Exists ("tarefas.csv")) {
                String[] linhas = System.IO.File.ReadAllLines ("tarefas.csv");
                tarefas.ID = linhas.Length + 1;
            } else {
                tarefas.ID = 1;
            }

            tarefas.Nome = form["nome"];
            tarefas.Descricao = form["descricao"];
            tarefas.Tipo = form["tipo"];
            tarefas.DataCriacao = DateTime.Now;

            using (StreamWriter sw = new StreamWriter ("tarefas.csv", true)) {
                sw.WriteLine ($"{tarefas.ID};{tarefas.Descricao};{tarefas.Tipo};{usuario.ID};{tarefas.DataCriacao}");
            }

            ViewBag.Mensagem = "Tarefas Cadastradas";

            return View ();
        }
    }
}