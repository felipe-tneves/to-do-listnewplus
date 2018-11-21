using System;
using System.Collections.Generic;
using System.IO;
using Senai.Tarefas.Mvc.Web.Models;

namespace Senai.Tarefas.Mvc.Web.Repositorios {
    public class UsuarioRepositorio {
        public UsuarioModel Buscar (string email, string senha) {
            List<UsuarioModel> usuariosCadastrados = CarregarDados ();

            foreach (UsuarioModel usuario in usuariosCadastrados) {
                if (usuario.Email == email && usuario.Senha == senha) {
                    return usuario;
                }
            }

            return null;
        }
        private List<UsuarioModel> CarregarDados () {
            List<UsuarioModel> lsUsuarios = new List<UsuarioModel> ();

            string[] linhas = File.ReadAllLines ("usuario.csv");

            foreach (string linha in linhas) {
                string[] dados = linha.Split (";");

                UsuarioModel usuario = new UsuarioModel {

                    ID = int.Parse (dados[0]),
                    Nome = dados[1],
                    Email = dados[2],
                    Senha = dados[3],
                    Tipo = dados[4],
                    DataNascimento = DateTime.Parse (dados[5])
                };

                lsUsuarios.Add (usuario);
            }

            return lsUsuarios;
        }
    }
}