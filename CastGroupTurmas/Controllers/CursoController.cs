using CastGroupTurmas.Data;
using CastGroupTurmas.Models;
using CastGroupTurmas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;


namespace CastGroupTurmas.Controllers
{

    public class CursoController : ApiController
    {
        private CastContext db = new CastContext();

        /// <summary>
        ///     Consulta todos os cursos disponíveis na base de dados.
        /// </summary>
        /// <returns>Lista de Cursos</returns>

        [HttpGet]
        [Route("api/curso/consultar")]
        public List<Curso> Consultar()
        {
            return db.Curso.ToList();
        }

        /// <summary>
        ///     Recupera o curso por código de identificação.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Curso.</returns>

        [HttpGet]
        [Route("api/curso/recuperar")]
        public Curso Get(int id)
        {
            return db.Curso.FirstOrDefault(x => x.Id == id);
        }       

        /// <summary>
        ///     Cria um novo registro.
        /// </summary>
        /// <param name="Curso"></param>
        /// <returns></returns>
        [Route("api/curso/criar")]
        [HttpPost]
        public List<string> Post([FromBody]CursoInsertViewModel Curso)
        {
            Curso novoCurso = new Curso();
            List<string> errorList = new List<string>();

            if (Curso.IdCategoria.Equals(0))
            {
                errorList.Add("Id da Categoria deve ser maior que zero.");
            }            

            if (Curso.DataInicio.Equals(DateTime.MinValue))
            {
                errorList.Add("Data de Início do Curso deve ser informada.");
            }

            if (Curso.DataFim.Equals(DateTime.MinValue))
            {
                errorList.Add("Data de Final do Curso deve ser informada.");
            }

            if(db.Curso.Any(x => (Curso.DataInicio >= x.DataInicio && Curso.DataInicio <= x.DataFim) ||
                                 (Curso.DataFim >= x.DataInicio && Curso.DataFim <= x.DataFim)
                ))
            {
                errorList.Add("Existe(m) curso(s) planejado(s) dentro do período Informado.");
            }

            if (Curso.DataInicio.Date < DateTime.Today)
            {
                errorList.Add("A data inicial do curso deve ser maior que a data atual.");
            }

            if (!errorList.Any() && ModelState.IsValid)
            {
                novoCurso.DataInicio = Curso.DataInicio;
                novoCurso.DataFim = Curso.DataFim;
                novoCurso.Descricao = Curso.Descricao;
                novoCurso.QtdeAluno = Curso.QtdeAluno;
                novoCurso.CategoriaId = Curso.IdCategoria;
                
                db.Curso.Add(novoCurso);
                db.SaveChanges();
            }
            else
            {
                errorList.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList());
            }
                        
            return errorList.Any() ? errorList : new List<string>() { novoCurso.Id.ToString() } ;
        }

        /// <summary>
        ///     Edita um registro existente.
        /// </summary>
        /// <param name="Curso"></param>
        [Route("api/curso/editar")]
        [HttpPut]
        public List<string> Put([FromBody]CursoUpdateViewModel Curso)
        {
            Curso CursoEdicao = db.Curso.FirstOrDefault(x => x.Id == Curso.Id);
            List<string> errorList = new List<string>();

            if (Curso.IdCategoria.Equals(0))
            {
                errorList.Add("Id da Categoria deve ser maior que zero.");
            }

            if (Curso.DataInicio.Equals(DateTime.MinValue))
            {
                errorList.Add("Data de Início do Curso deve ser informada.");
            }

            if (Curso.DataFim.Equals(DateTime.MinValue))
            {
                errorList.Add("Data de Final do Curso deve ser informada.");
            }

            if (db.Curso.Any(x => (x.DataInicio >= Curso.DataInicio || Curso.DataInicio <= x.DataFim) && x.Id != Curso.Id))
            {
                errorList.Add("Existe(m) curso(s) planejado(s) dentro do período Informado.");
            }

            if (Curso.DataInicio.Date < DateTime.Today)
            {
                errorList.Add("A data inicial do curso deve ser maior que a data atual.");
            }

            if (!errorList.Any() && ModelState.IsValid)
            {
                CursoEdicao.DataInicio = Curso.DataInicio;
                CursoEdicao.DataFim = Curso.DataFim;
                CursoEdicao.Descricao = Curso.Descricao;
                CursoEdicao.QtdeAluno = Curso.QtdeAluno;
                CursoEdicao.CategoriaId = Curso.IdCategoria;

                db.Entry(CursoEdicao).State = EntityState.Modified;
                db.SaveChanges();                
            }
            else
            {
                errorList.AddRange(ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList());
            }

            return errorList.Any() ? errorList : new List<string>() { CursoEdicao.Id.ToString() };
        }

        /// <summary>
        ///  Apaga um registro por identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/curso/apagar")]
        [HttpDelete]
        public string Delete(int id)
        {
            Curso curso = db.Curso.Find(id);
            if(curso != null)
            {
                db.Curso.Remove(curso);
                db.SaveChanges();
            }

            return "OK";

        }
    }
}
