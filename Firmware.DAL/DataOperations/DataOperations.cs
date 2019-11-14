using Firmware.DAL.Models;
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
            OpenConnection();
            List<SoftwarePackage> inventory = new List<SoftwarePackage>();

            string sql = "select * from ;";
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    inventory.Add(
                        new SoftwarePackage
                        {

                        }
                        );
                }
            }

            return inventory;
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
        public void AddSoftwarePackage(byte[] Swpackage , byte[] Swhelpdoc , @SwPkgUID uniqueidentifier, @SwAddedDate datetime, @SwPkgVersion VARCHAR, @SwPkgDescription NVARCHAR, @SwColorStandardID INT, @AddedDate DATETIME2, @SwVersion INT,
                                                    @SwFileDetailsUID uniqueidentifier, @SwFileName nvarchar, @SwFileFormat nvarchar, @SwFileSize varchar, @SwFileURL nvarchar, @SwFileUploadDate datetime2, @SwFileChecksum varchar, @SwFileChecksumType varchar, @SwCreatedBy nvarchar,
                                                    @BlobUID uniqueidentifier, @BlobDescription varchar, @BlobTypeID uniqueidentifier, @MapUID uniqueidentifier)
        {
            OpenConnection();
           
            // Execute using our connection.
            using (SqlCommand command = new SqlCommand("[Inventory].[usp_AddSoftwarePackage]", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.Add(new SqlParameter {ParameterName = "@Swpackage", SqlDbType = SqlDbType.VarBinary, Value =  });

                command.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
}
