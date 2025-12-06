using MySql.Data.MySqlClient;
using ProjetoFinal.Models;

namespace ProjetoFinal.Repositorio
{
    public class CadastroClienteRepositorio
    {
        private readonly string _conexaoMySQL;

        public CadastroClienteRepositorio(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");
        }

        public void CadastrarCliente(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // 1. Salvar endereço
                string sqlEndereco = @"
                    INSERT INTO tbEndereco (Rua, Numero, Complemento)
                    VALUES (@rua, @numero, @complemento);
                    SELECT LAST_INSERT_ID();
                ";

                MySqlCommand cmdEnd = new(sqlEndereco, conexao);
                cmdEnd.Parameters.AddWithValue("@rua", usuario.Endereco.Rua);
                cmdEnd.Parameters.AddWithValue("@numero", usuario.Endereco.Numero);
                cmdEnd.Parameters.AddWithValue("@complemento",
                    usuario.Endereco.Complemento ?? (object)DBNull.Value);

                int codigoEndereco = Convert.ToInt32(cmdEnd.ExecuteScalar());

                // 2. Salvar usuário com FK
                string sqlUsuario = @"
                    INSERT INTO tbUsuario (Nome, Cpf, Idade, Email, Codigo_Endereco)
                    VALUES (@nome, @cpf, @idade, @email, @codEnd);
                ";

                MySqlCommand cmdUser = new(sqlUsuario, conexao);
                cmdUser.Parameters.AddWithValue("@nome", usuario.Nome);
                cmdUser.Parameters.AddWithValue("@cpf", usuario.Cpf);
                cmdUser.Parameters.AddWithValue("@idade", usuario.Idade);
                cmdUser.Parameters.AddWithValue("@email", usuario.Email);
                cmdUser.Parameters.AddWithValue("@codEnd", codigoEndereco);

                cmdUser.ExecuteNonQuery();
            }
        }
    }
}
