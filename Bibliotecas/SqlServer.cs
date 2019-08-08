using ReservaSala.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace ReservaSala.Bibliotecas
{
    /// <summary>
    /// Usada para interação com o banco de dados
    /// </summary>
    public static class SqlServer
    {
        /// <summary>
        /// Armazena a string de conexão com o banco de dados
        /// </summary>
        private static string StringConexao
        {
            get => @"Data Source=DESKTOP-UNN4F31\SQLSERVER;Initial Catalog=ReservaSala;Integrated Security=True";
        }

        /// <summary>
        /// Insere as informações da sala no banco de dados
        /// </summary>
        /// <param name="dados">Objeto do tipo <see cref="Sala"/> a ser inserido no banco</param>
        public static void Inserir(Sala dados)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(StringConexao))
                {
                    using (SqlCommand comando = new SqlCommand("INSERT INTO Sala (nome) VALUES(@Nome)", conexao))
                    {
                        // adiciona os dados em forma de parametro, dessa forma evitando a possibilidade de SQL Injection
                        comando.Parameters.AddWithValue("@Nome", dados.Nome);

                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw new AcessoBancoException();
            }
        }
        /// <summary>
        /// Busca todas as salas existentes
        /// </summary>
        /// <returns>Uma lista das salas</returns>
        public static List<Sala> ListaSalas()
        {
            try
            {
                using (SqlCommand comando = new SqlCommand("SELECT * FROM Sala"))
                {
                    return BuscaSalas(comando);
                }
            }
            catch
            {
                // em caso de erro, retornar uma lista vazia
                return new List<Sala>();
            }
        }
        /// <summary>
        /// Busca uma sala com um Id especifico
        /// </summary>
        /// <param name="id">Id da sala</param>
        /// <returns>O objeto to tipo Sala pertencente ao id passado</returns>
        public static Sala ListaSalas(int id)
        {
            try
            {
                using (SqlCommand comando = new SqlCommand("SELECT * FROM Sala WHERE id = @Id"))
                {
                    // adiciona os dados em forma de parametro, dessa forma evitando a possibilidade de SQL Injection
                    comando.Parameters.AddWithValue("@Id", id);

                    return BuscaSalas(comando)[0];
                }
            }
            catch
            {
                // em caso de erro, retornar uma lista vazia
                return new Sala();
            }
        }
        /// <summary>
        /// Função de uso interno, para reaproveitamento de codigo
        /// </summary>
        /// <param name="comando">Comando a ser usado para a busca no banco</param>
        /// <returns>uma Lista de salas</returns>
        private static List<Sala> BuscaSalas(SqlCommand comando)
        {
            using (SqlConnection conexao = new SqlConnection(StringConexao))
            {
                comando.Connection = conexao;
                conexao.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    List<Sala> salas = new List<Sala>();

                    while (reader.Read())
                    {
                        salas.Add(new Sala
                        {
                            Id = (int) reader[0],
                            Nome = reader[1].ToString()
                        });
                    }

                    return salas;
                }
            }
        }

        /// <summary>
        /// Insere as informações da reserva no banco de dados
        /// </summary>
        /// <param name="dados">Objeto do tipo <see cref="Reserva"/> a ser inserido no banco</param>
        public static void Inserir(Reserva dados)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(StringConexao))
                {
                    using (SqlCommand comando = new SqlCommand("INSERT INTO Reserva (descricao, data_inicio, data_fim, id_sala) VALUES(@Descricao, @Data_inicio, @Data_fim, @Id_sala)", conexao))
                    {
                        // adiciona os dados em forma de parametro, dessa forma evitando a possibilidade de SQL Injection
                        comando.Parameters.AddWithValue("@Descricao", dados.Descricao);
                        comando.Parameters.AddWithValue("@Data_inicio", DateTime.ParseExact(dados.DataInicio, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("yyyyMMdd HH:mm"));
                        comando.Parameters.AddWithValue("@Data_fim", DateTime.ParseExact(dados.DataTermino, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("yyyyMMdd HH:mm"));
                        comando.Parameters.AddWithValue("@Id_sala", dados.Sala.Id);

                        conexao.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw new AcessoBancoException();
            }
        }
        /// <summary>
        /// Verifica se a reserva pode ser feita de acordo com o horario desejado
        /// </summary>
        /// <param name="dados">Objeto da reserva a ser verificada a disponibilidade de horario</param>
        /// <returns>True se pode reservar, false se existe conflito de horario</returns>
        public static bool PodeReservar(Reserva dados)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(StringConexao))
                {
                    using (SqlCommand comando = new SqlCommand("SELECT id FROM Reserva WHERE (data_inicio <= @Data_inicio AND data_fim >= @Data_inicio) OR (data_inicio <= @Data_fim AND data_fim >= @Data_fim)"))
                    {
                        // adiciona os dados em forma de parametro, dessa forma evitando a possibilidade de SQL Injection
                        comando.Parameters.AddWithValue("@Data_inicio", DateTime.ParseExact(dados.DataInicio, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("yyyyMMdd HH:mm"));
                        comando.Parameters.AddWithValue("@Data_fim", DateTime.ParseExact(dados.DataTermino, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("yyyyMMdd HH:mm"));

                        comando.Connection = conexao;
                        conexao.Open();

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return false;
                            }

                            return true;
                        }
                    }
                }
                
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Busca todas as reservas existentes
        /// </summary>
        /// <returns>Uma Lista das reservas</returns>
        public static List<Reserva> ListaReservas()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(StringConexao))
                {
                    using (SqlCommand comando = new SqlCommand("SELECT res.id, res.descricao, FORMAT(res.data_inicio, 'dd/MM/yyyy HH:mm'), FORMAT(res.data_fim, 'dd/MM/yyyy HH:mm'), sala.id, sala.nome FROM Reserva res JOIN Sala sala ON sala.id = res.id_sala"))
                    {
                        comando.Connection = conexao;
                        conexao.Open();

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            List<Reserva> salas = new List<Reserva>();

                            while (reader.Read())
                            {
                                salas.Add(new Reserva
                                {
                                    Id = (int) reader[0],
                                    Descricao = reader[1].ToString(),
                                    DataInicio = reader[2].ToString(),
                                    DataTermino = reader[3].ToString(),
                                    Sala = new Sala
                                    {
                                        Id = (int) reader[4],
                                        Nome = reader[5].ToString()
                                    }
                                });
                            }

                            return salas;
                        }
                    }
                }
            }
            catch
            {
                // em caso de erro, retornar uma lista vazia
                return new List<Reserva>();
            }
        }
    }
}
