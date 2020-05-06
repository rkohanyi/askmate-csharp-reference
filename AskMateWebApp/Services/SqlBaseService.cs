using AskMateWebApp.Domain;
using System.Data;
using System.Data.Common;

namespace AskMateWebApp.Services
{
    public abstract class SqlBaseService
    {
        protected static void HandleExecuteNonQuery(IDbCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                string sqlState = (string)ex.Data["SqlState"];
                if (sqlState == "45000")
                {
                    throw new AskMateNotAuthorizedException(ex);
                }
                else if (sqlState == "45001")
                {
                    throw new AskMateCannotVoteException(ex);
                }
                throw;
            }
        }
    }
}
