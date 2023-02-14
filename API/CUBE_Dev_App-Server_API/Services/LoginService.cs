using CUBE_Dev_App_Server_API.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace CUBE_Dev_App_Server_API.Services
{
    public static class LoginService
    {
        public static bool HeavyClientLogin(string? username, string? password)
        {
            string sql = @"SELECT * FROM DataUtils
                            WHERE DataKey = 'username' OR DataKey = 'password'";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                return false;
            }

            List<DataUtils> dataUtils = new List<DataUtils>();

            while (reader.Read())
            {
                dataUtils.Add(new DataUtils
                {
                    DataKey = reader.GetString("DataKey"),
                    DataValue = reader.IsDBNull("DataValue") ? null : reader.GetString("DataValue")
                });
            }

            // Récupération des infos
            string? correctUsername = dataUtils.Where(x => x.DataKey == "username").Select(x => x.DataValue).FirstOrDefault();
            string? correctPassword = dataUtils.Where(x => x.DataKey == "password").Select(x => x.DataValue).FirstOrDefault();

            // Retourne true si les informations sont identiques
            if (correctUsername == username
                && correctPassword == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateLoginInfos(string? username, string? password)
        {
            string sql = @"UPDATE DataUtils
                            SET
	                            DataValue = 
		                            CASE WHEN Datakey = 'username' THEN @username
                                    WHEN Datakey = 'password' THEN @password
                                    END";

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                if (!DBConnection.ExecuteCommandToDB(command))
                    return false;
            }

            return true;
        }

        public static bool EmailNotAlreadyUsed(string email)
        {
            string sql = $@"SELECT COUNT(*) EmailCount FROM Client
                            WHERE Email = '{email}'";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                return false;
            }

            if (reader.Read())
            {
                string emailCount = reader.GetString("EmailCount");

                if (!int.TryParse(emailCount, out int count))
                {
                    return false;
                }

                if (count == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool WebsiteLogin(string email, string password, out int idClient)
        {
            idClient = -1;

            string sql = $@"SELECT IDClient, Password FROM Client
                            WHERE Email = '{email}'
                            LIMIT 1";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                return false;
            }

            if (reader.Read())
            {
                if (reader.IsDBNull("Password") || reader.IsDBNull("IDClient"))
                {
                    return false;
                }

                string realPassword = reader.GetString("Password");

                if (password == realPassword)
                {
                    string idClientString = reader.GetString("IDClient");

                    if (!int.TryParse(idClientString, out idClient))
                    {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
