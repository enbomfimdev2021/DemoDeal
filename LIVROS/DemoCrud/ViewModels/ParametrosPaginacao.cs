using System.Collections.Specialized;
using System.Linq;

namespace DemoCrud.ViewModels
{
    public class ParametrosPaginacao
    {
        public ParametrosPaginacao(NameValueCollection dados)
        {
            //sort[Titulo] || sort[Autor] || sort[autor] sort[AnoEdição] || sort[Valor]

            //Dados representa o Request do Form
            string chave = dados.AllKeys.Where(k => k.StartsWith("sort")).First();

            string ordenacao = dados[chave];
            string campo = chave.Replace("sort[", string.Empty).Replace("]", string.Empty);

            CampoOrdenado = string.Format("{0} {1}", campo, ordenacao);

            Current = int.Parse(dados["current"]);
            RowCount = int.Parse(dados["rowCount"]);
            SearchPhrase = dados["searchPhrase"];
          //  Id = int.Parse(dados["id"]);
        }
        public int Current { get; set; }
        public int RowCount { get; set; }
        //public int Id { get; set; }
        public string SearchPhrase { get; set; }
        public string CampoOrdenado { get; set; }
    }
}