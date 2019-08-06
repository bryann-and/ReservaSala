using ReservaSala.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace ReservaSala.Bibliotecas
{
    public static class SqlServer
    {
        private static string StringConexao
        {
            get => @"Data Source=DESKTOP-UNN4F31\SQLSERVER;Initial Catalog=ReservaSala;Integrated Security=True";
        }

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
        public static bool PodeReservar(Reserva dados)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(StringConexao))
                {
                    using (SqlCommand comando = new SqlCommand("SELECT id FROM Reserva WHERE (data_inicio < @Data_inicio AND data_fim > @Data_inicio) OR (data_inicio < @Data_fim AND data_fim > @Data_fim)"))
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
                                return true;
                            }

                            return false;
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private static List<Reserva> BuscaReservas(SqlCommand comando)
        {
            using (SqlConnection conexao = new SqlConnection(StringConexao))
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
                                Id = (int)reader[4],
                                Nome = reader[5].ToString()
                            }
                        });
                    }

                    return salas;
                }
            }
        }
    }
}
