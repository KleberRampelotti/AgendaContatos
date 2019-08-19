using AgendaContatos.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaContatos.Models
{
    public class AgendaModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Informe o Nome do Contato!")]
        public string nome { get; set; }
        public string NomeFiltro { get; set; } // Utilizado para o filtro do pesquisar contato
        public string logradouro { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string email { get; set; }
        public string tel_residencial { get; set; }        
        public string TelResFiltro { get; set; } // Utilizado para o filtro do pesquisar contato
        public string tel_celular { get; set; }        
        public string TelCelFiltro { get; set; } // Utilizado para o filtro do pesquisar contato
        public string observacao { get; set; }        
        public IHttpContextAccessor HttpContextAccessor { get; set; }        

        public AgendaModel()
        {

        }

        //Recebe o contexto para acesso as variáveis de sessão.
        public AgendaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<AgendaModel> ListaAgenda()
        {
            List<AgendaModel> lista = new List<AgendaModel>();
            AgendaModel item;

            //Utilizado pela View Pesquisar Contato
            string filtro = "";
            
            if (NomeFiltro != null)
            {
                filtro += $"nome like '%{NomeFiltro}%'";               
            }
            else
            {
                filtro += $"nome !=''";
            }
            
            if (TelResFiltro != null)
            {
                filtro += $" and tel_residencial = '{TelResFiltro}'";
            }            
            
            if (TelCelFiltro != null)
            {
                filtro += $" and tel_celular = '{TelCelFiltro}'";
            }

            //Fim

            string sql = $"SELECT ID, NOME, LOGRADOURO, NUMERO, BAIRRO, CIDADE, ESTADO, EMAIL, TEL_RESIDENCIAL, TEL_CELULAR, OBSERVACAO " +
                         $"FROM AGENDA where {filtro} order by nome;";                                  
            DAL objDal = new DAL();
            DataTable dt = objDal.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new AgendaModel();
                item.id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.nome = dt.Rows[i]["NOME"].ToString();
                item.logradouro = dt.Rows[i]["LOGRADOURO"].ToString();
                item.numero = int.Parse(dt.Rows[i]["NUMERO"].ToString());
                item.bairro = dt.Rows[i]["BAIRRO"].ToString();
                item.cidade = dt.Rows[i]["CIDADE"].ToString();
                item.estado = dt.Rows[i]["ESTADO"].ToString();
                item.email = dt.Rows[i]["EMAIL"].ToString();
                item.tel_residencial = dt.Rows[i]["TEL_RESIDENCIAL"].ToString();
                item.tel_celular = dt.Rows[i]["TEL_CELULAR"].ToString();
                item.observacao = dt.Rows[i]["OBSERVACAO"].ToString();                
                lista.Add(item);
            }
            return lista;           
        }

        public void Insert()
        {
            string sql = $"INSERT INTO AGENDA (NOME,LOGRADOURO,NUMERO,BAIRRO,CIDADE,ESTADO,EMAIL,TEL_RESIDENCIAL,TEL_CELULAR,OBSERVACAO) " +
                         $"VALUES('{nome}','{logradouro}','{numero}','{bairro}','{cidade}','{estado}','{email}','{tel_residencial}','{tel_celular}'" +
                         $",'{observacao}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandosSQL(sql);
        }

        public AgendaModel CarregarRegistro(int? id)
        {
            AgendaModel item = new AgendaModel();

            string sql = $"SELECT ID,NOME,LOGRADOURO,NUMERO,BAIRRO,CIDADE,ESTADO,EMAIL,TEL_RESIDENCIAL,TEL_CELULAR,OBSERVACAO FROM AGENDA " +
                         $"WHERE ID = {id}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);
                      
            item.id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.nome = dt.Rows[0]["NOME"].ToString();
            item.logradouro = dt.Rows[0]["LOGRADOURO"].ToString();
            item.numero = int.Parse(dt.Rows[0]["NUMERO"].ToString());
            item.bairro = dt.Rows[0]["BAIRRO"].ToString();
            item.cidade = dt.Rows[0]["CIDADE"].ToString();
            item.estado = dt.Rows[0]["ESTADO"].ToString();
            item.email = dt.Rows[0]["EMAIL"].ToString();
            item.tel_residencial = dt.Rows[0]["TEL_RESIDENCIAL"].ToString();
            item.tel_celular = dt.Rows[0]["TEL_CELULAR"].ToString();
            item.observacao = dt.Rows[0]["OBSERVACAO"].ToString();            

            return item;
        }

        public void Excluir(int id_contato)
        {
            new DAL().ExecutarComandosSQL("DELETE FROM AGENDA WHERE ID = " + id_contato);
        }
    }
}
