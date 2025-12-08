using MySql.Data.MySqlClient;
using ProjetoFinal.Models;

namespace ProjetoFinal.Repositorio
{
    public class EditarPetRepositorio
    {
        private readonly string _conexao;

        public EditarPetRepositorio(IConfiguration configuration)
        {
            _conexao = configuration.GetConnectionString("conexaoMySQL");
        }

        public List<Pet> ListarPets(string nome)
        {
            List<Pet> lista = new List<Pet>();

            using (MySqlConnection con = new MySqlConnection(_conexao))
            {
                con.Open();

                string sql = @"SELECT Codigo_Pet, Nome, Tipo, Raca, Porte, Idade
                               FROM tbPet
                               WHERE Nome LIKE @nome";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Pet
                        {
                            Codigo_Pet = dr.GetInt32("Codigo_Pet"),
                            Nome = dr.GetString("Nome"),
                            Tipo = dr.GetString("Tipo"),
                            Raca = dr.GetString("Raca"),
                            Porte = dr.GetString("Porte"),
                            Idade = dr.GetInt32("Idade")
                        });
                    }
                }
            }

            return lista;
        }

        public Pet BuscarPorId(int id)
        {
            Pet pet = new Pet();

            using (MySqlConnection con = new MySqlConnection(_conexao))
            {
                con.Open();

                string sql = @"SELECT Codigo_Pet, Nome, Tipo, Raca, Porte, Idade
                               FROM tbPet
                               WHERE Codigo_Pet = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        pet.Codigo_Pet = dr.GetInt32("Codigo_Pet");
                        pet.Nome = dr.GetString("Nome");
                        pet.Tipo = dr.GetString("Tipo");
                        pet.Raca = dr.GetString("Raca");
                        pet.Porte = dr.GetString("Porte");
                        pet.Idade = dr.GetInt32("Idade");
                    }
                }
            }

            return pet;
        }

        public void AtualizarPet(Pet pet)
        {
            using (MySqlConnection con = new MySqlConnection(_conexao))
            {
                con.Open();

                string sql = @"UPDATE tbPet
                               SET Nome = @nome,
                                   Tipo = @tipo,
                                   Raca = @raca,
                                   Porte = @porte,
                                   Idade = @idade
                               WHERE Codigo_Pet = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@nome", pet.Nome);
                cmd.Parameters.AddWithValue("@tipo", pet.Tipo);
                cmd.Parameters.AddWithValue("@raca", pet.Raca);
                cmd.Parameters.AddWithValue("@porte", pet.Porte);
                cmd.Parameters.AddWithValue("@idade", pet.Idade);
                cmd.Parameters.AddWithValue("@id", pet.Codigo_Pet);

                cmd.ExecuteNonQuery();
            }
        }
    }
}