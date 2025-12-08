using MySql.Data.MySqlClient;
using ProjetoFinal.Models;

namespace ProjetoFinal.Repositorio
{
    public class PagamentoRepositorio
    {
        private readonly string _conexao;

        public PagamentoRepositorio(IConfiguration configuration)
        {
            _conexao = configuration.GetConnectionString("conexaoMySQL");
        }

        public void CadastrarPagamento(Pagamento pagamento)
        {
            using (MySqlConnection con = new MySqlConnection(_conexao))
            {
                con.Open();

                string sql = @"INSERT INTO tbPagamento (Titular, Valor, MetodoPagamento, Codigo_Usuario)
                               VALUES (@titular, @valor, @metodo, @usuario)";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@titular", pagamento.Titular);
                cmd.Parameters.AddWithValue("@valor", pagamento.Valor);
                cmd.Parameters.AddWithValue("@metodo", pagamento.MetodoPagamento);
                cmd.Parameters.AddWithValue("@usuario", pagamento.Codigo_Usuario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
