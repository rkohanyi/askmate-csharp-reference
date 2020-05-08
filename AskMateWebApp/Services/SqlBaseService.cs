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
                // NOTE: accessing "SqlState" might not work with every underlying RDBMS.
                if (int.TryParse((string)ex.Data["SqlState"], out int sqlState))
                {
                    if (sqlState == 23505)
                    {
                        throw new AskMateException();
                    }
                    else if (sqlState == 45000)
                    {
                        throw new AskMateNotAuthorizedException(ex);
                    }
                    else if (sqlState == 45001)
                    {
                        throw new AskMateCannotVoteException(ex);
                    }
                }
                throw;
            }
        }
    }
}
