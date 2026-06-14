using BookingSundorbon.Views.DTOs.NotificationView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookingSundorbon.Features.Repositories.NotificationRepository
{
    internal class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<long> CreateNotificationAsync(CreateNotificationView notification)
        {
            if (notification.RecipientUserIds == null || notification.RecipientUserIds.Count == 0)
            {
                throw new ArgumentException("At least one recipient is required.", nameof(notification));
            }

            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Title", notification.Title, DbType.String);
            parameters.Add("@Message", notification.Message, DbType.String);
            parameters.Add("@NotificationType", notification.NotificationType, DbType.String);
            parameters.Add("@ReferenceId", notification.ReferenceId, DbType.Int64);
            parameters.Add("@ReferenceType", notification.ReferenceType, DbType.String);
            parameters.Add("@RedirectUrl", notification.RedirectUrl, DbType.String);
            parameters.Add("@CreatedBy", notification.CreatedBy, DbType.Int64);
            parameters.Add("@RecipientUserIds", string.Join(",", notification.RecipientUserIds), DbType.String);

            return await dbConnection.ExecuteScalarAsync<long>(
                "[dbo].[SP_CreateNotification]",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<UserNotificationView>> GetUserNotificationsAsync(
            string userId,
            bool unreadOnly = false,
            int page = 1,
            int pageSize = 20)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.String);
            parameters.Add("@UnreadOnly", unreadOnly, DbType.Boolean);
            parameters.Add("@Page", page, DbType.Int32);
            parameters.Add("@PageSize", pageSize, DbType.Int32);

            return await dbConnection.QueryAsync<UserNotificationView>(
                "[dbo].[SP_GetNotificationsByUserId]",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.String);

            return await dbConnection.ExecuteScalarAsync<int>(
                "[dbo].[SP_GetUnreadNotificationCountByUserId]",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> MarkAsReadAsync(long recipientId, string userId)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@RecipientId", recipientId, DbType.Int64);
            parameters.Add("@UserId", userId, DbType.String);

            var rowsAffected = await dbConnection.ExecuteAsync(
                "[dbo].[SP_MarkNotificationRecipientAsRead]",
                parameters,
                commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.String);

            await dbConnection.ExecuteAsync(
                "[dbo].[SP_MarkAllNotificationRecipientsAsRead]",
                parameters,
                commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> SoftDeleteAsync(long recipientId, string userId)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@RecipientId", recipientId, DbType.Int64);
            parameters.Add("@UserId", userId, DbType.String);

            var rowsAffected = await dbConnection.ExecuteAsync(
                "[dbo].[SP_SoftDeleteNotificationRecipient]",
                parameters,
                commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }
    }
}
