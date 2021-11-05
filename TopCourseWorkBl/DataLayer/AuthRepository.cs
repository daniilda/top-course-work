using System.Threading;
using System.Threading.Tasks;
using TopCourseWorkBl.DataLayer.Cmds.Auth;
using TopCourseWorkBl.Dtos.Auth;

namespace TopCourseWorkBl.DataLayer
{
    public class AuthRepository
    {
        private readonly DbExecuteWrapper _dbExecuteWrapper;

        public AuthRepository(DbExecuteWrapper dbExecuteWrapper)
            => _dbExecuteWrapper = dbExecuteWrapper;

        public async Task<User> InsertNewUserAsync(InsertNewUserCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var insertQuery = $@"INSERT INTO {SqlConstants.UsersTable}
                                    (username, password, role)
                                    VALUES(@Username, @Password, '{SqlConstants.DefaultRegistrationRole}');";
            var getQuery = $@"SELECT * FROM {SqlConstants.UsersTable}
                            WHERE username = @Username;";

            await _dbExecuteWrapper.ExecuteQueryAsync(insertQuery,
                new
                {
                    insertCmd.Username,
                    insertCmd.Password
                },
                cancellationToken);

            return await _dbExecuteWrapper.ExecuteQueryAsync<User>(getQuery,
                new
                {
                    insertCmd.Username
                },
                cancellationToken);
        }

        public async Task<User> GetUserByUsernameAsync(GetUserByUsernameCmd getCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"SELECT * FROM {SqlConstants.UsersTable}
                            WHERE username = @Username;";

            return await _dbExecuteWrapper.ExecuteQueryAsync<User>(query,
                new{getCmd.Username},
                cancellationToken);
        }

        public async Task<User> GetUserByRefreshTokenAsync(GetUserByRefreshTokenCmd getCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"SELECT ut.* FROM {SqlConstants.UsersTable} ut
                            INNER JOIN {SqlConstants.RefreshTokensTable} rtt ON rtt.user_id = ut.id
                            WHERE rtt.token = @RefreshToken;";

            return await _dbExecuteWrapper.ExecuteQueryAsync<User>(
                query,
                new{getCmd.RefreshToken},
                cancellationToken);
        }

        public async Task<RefreshToken> GetRefreshToken(GetRefreshTokenCmd getCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"SELECT id Id, user_id UserId, token Token, expires_at ExpiresAt, 
                            created_at CreatedAt, revoked_at RevokedAt, revoked_by_ip RevokedByIp, 
                            replaced_by_token ReplacedByToken FROM {SqlConstants.RefreshTokensTable}
                            WHERE token = @Token;";

            return await _dbExecuteWrapper.ExecuteQueryAsync<RefreshToken>(query,
                new{getCmd.Token},
                cancellationToken);
        }

        public async Task UpdateRefreshTokenAsync(UpdateRefreshTokenCmd updateCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"UPDATE {SqlConstants.RefreshTokensTable}
                            SET revoked_at = @RevokedAt,
                                replaced_by_token = @ReplacedByToken,
                                revoked_by_ip = @RevokedByIp
                            WHERE token = @Token;";

            await _dbExecuteWrapper.ExecuteQueryAsync(
                query,
                new
                {
                    updateCmd.RevokedAt,
                    updateCmd.ReplacedByToken,
                    updateCmd.RevokedByIp,
                    updateCmd.Token
                }, 
                cancellationToken);
        }

        public async Task InsertRefreshTokenAsync(InsertRefreshTokenCmd insertCmd,
            CancellationToken cancellationToken)
        {
            var query = $@"INSERT INTO {SqlConstants.RefreshTokensTable}
                            (user_id, token, expires_at)
                            VALUES(@UserId, @Token, @ExpiresAt);";

            await _dbExecuteWrapper.ExecuteQueryAsync(
                query,
                new
                {
                    insertCmd.RefreshToken.UserId,
                    insertCmd.RefreshToken.Token,
                    insertCmd.RefreshToken.ExpiresAt
                },
                cancellationToken);
        }
    }
}