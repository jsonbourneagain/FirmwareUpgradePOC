using Firmware.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Firmware.DAL.DataOperations
{
    public class DataOperations
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FirmwareSqlProvider"].ConnectionString;
        private SqlConnection _sqlConnection = null;

        private void OpenConnection()
        {
            _sqlConnection = new SqlConnection { ConnectionString = _connectionString };
            _sqlConnection.Open();
        }
        private void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
            }
        }
        public List<SoftwarePackage> GetSoftwarePackage()
        {
            try
            {
                OpenConnection();
                List<SoftwarePackage> inventory = new List<SoftwarePackage>();

                using (SqlCommand command = new SqlCommand("Inventory.usp_GetAllSoftwarePackages", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (dataReader.Read())
                    {
                        inventory.Add(
                            new SoftwarePackage
                            {
                                SwPkgUID = (Guid)dataReader["SwPkgUID"],
                                SwPkgVersion = (string)dataReader["SwPkgVersion"],
                                SwColorStandardID = (int)dataReader["SwColorStandardID"],
                                SwAddedDate = (DateTime)dataReader["SwAddedDate"],
                                SwFileName = (string)dataReader["SwFileName"],
                                SwFileSize = (long)dataReader["SwFileSize"]
                            }
                            );
                    }
                }

                return inventory;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }

        }
        public SoftwarePackage GetSoftwarePackage(string id)
        {
            OpenConnection();

            SoftwarePackage swPackg = null;
            string sql = $"";
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    swPackg = new SoftwarePackage
                    {
                    };
                }
                dataReader.Close();
            }
            return swPackg;
        }
        public bool AddSoftwarePackage(byte[] Swpackage, byte[] Swhelpdoc, string SwPkgVersion, string SwPkgDescription, int SwColorStandardID, int SwVersion, string SwFileName, string SwFileFormat, long SwFileSize, string SwFileURL, string SwFileChecksum, string SwFileChecksumType, string SwCreatedBy, string BlobDescription)
        {
            try
            {
                OpenConnection();

                // Execute using our connection.
                using (SqlCommand command = new SqlCommand("Inventory.usp_AddSoftwarePackage", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter { ParameterName = "@Swpackage", SqlDbType = SqlDbType.VarBinary, Value = Swpackage });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Swhelpdoc", SqlDbType = SqlDbType.VarBinary, Value = Swhelpdoc });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwPkgUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = Guid.NewGuid() });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwAddedDate", SqlDbType = SqlDbType.DateTime2, Value = DateTime.Now });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwPkgVersion", SqlDbType = SqlDbType.VarChar, Value = SwPkgVersion });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwPkgDescription", SqlDbType = SqlDbType.VarChar, Value = SwPkgDescription });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwColorStandardID", SqlDbType = SqlDbType.Int, Value = SwColorStandardID });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwVersion", SqlDbType = SqlDbType.Int, Value = SwVersion });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileDetailsUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = Guid.NewGuid() });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileName", SqlDbType = SqlDbType.VarChar, Value = SwFileName });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileFormat", SqlDbType = SqlDbType.VarChar, Value = SwFileFormat });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileSize", SqlDbType = SqlDbType.VarChar, Value = SwFileSize.ToString() });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileURL", SqlDbType = SqlDbType.VarChar, Value = DBNull.Value });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileUploadDate", SqlDbType = SqlDbType.DateTime2, Value = DateTime.Now });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileChecksum", SqlDbType = SqlDbType.VarChar, Value = DBNull.Value });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwFileChecksumType", SqlDbType = SqlDbType.VarChar, Value = DBNull.Value });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@SwCreatedBy", SqlDbType = SqlDbType.VarChar, Value = DBNull.Value });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@BlobUID", SqlDbType = SqlDbType.UniqueIdentifier, Value = Guid.NewGuid() });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@BlobDescription", SqlDbType = SqlDbType.VarChar, Value = SwPkgDescription });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@BlobTypeID", SqlDbType = SqlDbType.UniqueIdentifier, Value = Guid.NewGuid() });


                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log it to the log file.

                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
    }
}
