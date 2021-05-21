namespace DemoCrud.ViewModels
{
    public class DadosFiltrados
    {

        public DadosFiltrados(ParametrosPaginacao parametrosPaginacao)
        {
            rowCount = parametrosPaginacao.RowCount;
            current = parametrosPaginacao.Current;
        }
        
        //Nomes em minusculo porque serao serelizados no json
        public dynamic rows { get; set; }
        public int current { get; set; }
        public int rowCount { get; set; }
        public int total { get; set; }
    }
}