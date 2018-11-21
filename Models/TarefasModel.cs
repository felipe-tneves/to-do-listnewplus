using System;

namespace Senai.Tarefas.Mvc.Web.Models {
    public class TarefasModel {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}