using BookingSundorbon.Views.DTOs.ProhibitedItemView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using BookingSundorbon.Views.DTOs.TransitionCostView;
using System.Text.Json;
using BookingSundorbon.Features.Helpers;

namespace BookingSundorbon.Features.Repositories.GetTransitionCostRepository
{
    public class TransitionCostRepository : ITransitionCostRepository
    {
        private readonly string _connectionString;

        public TransitionCostRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CreateParcelBookingOutputView> CreateParcelBookingAsync(CreateParcelBookingView createParcelBookingView)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    //string barcode = Guid.NewGuid().ToString();

                    string barcode = Guid.NewGuid().ToString("N").Substring(0, 8) + Guid.NewGuid().ToString("N").Substring(24, 8);

                    string userId=null;
                    string password=null;
                


                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", createParcelBookingView.CompanyId, DbType.Int32);
                    parameters.Add("@RoutingTypeId", createParcelBookingView.RoutingTypeId, DbType.Int32);
                    parameters.Add("@RouteCost", createParcelBookingView.RouteCost, DbType.Decimal);

                    parameters.Add("@SenderName", createParcelBookingView.SenderName, DbType.String);
                    parameters.Add("@SenderEmail", createParcelBookingView.SenderEmail, DbType.String);
                    parameters.Add("@SenderMobileNo", createParcelBookingView.SenderMobileNo, DbType.String);
                    parameters.Add("@SenderCountryId", createParcelBookingView.SenderCountryId, DbType.Int32);
                    parameters.Add("@SenderCityId", createParcelBookingView.SenderCityId, DbType.Int32);
                    parameters.Add("@SenderLandmark", createParcelBookingView.SenderLandMark, DbType.String);
                    parameters.Add("@SenderAdditionalAddressInfo", createParcelBookingView.SenderAdditionalAddressInfo, DbType.String);

                    parameters.Add("@ReceiverName", createParcelBookingView.ReceiverName, DbType.String);
                    parameters.Add("@ReceiverEmail", createParcelBookingView.ReceiverEmail, DbType.String);
                    parameters.Add("@ReceiverMobileNo", createParcelBookingView.ReceiverMobileNo, DbType.String);
                    parameters.Add("@ReceiverCountryId", createParcelBookingView.ReceiverCountryId, DbType.Int32);
                    parameters.Add("@ReceiverCityId", createParcelBookingView.ReceiverCityId, DbType.Int32);
                    parameters.Add("@ReceiverLandmark", createParcelBookingView.ReceiverLandMark, DbType.String);
                    parameters.Add("@ReceiverAdditionalAddressInfo", createParcelBookingView.ReceiverAdditionalAddressInfo, DbType.String);

                    parameters.Add("@ItemCategoryId", createParcelBookingView.ItemCategoryId, DbType.Int32);
                    parameters.Add("@ItemTypeId", createParcelBookingView.ItemTypeId, DbType.Int32);
                    parameters.Add("@ItemTypeCost", createParcelBookingView.ItemTypeCost, DbType.Decimal);

                    parameters.Add("@DimensionId", createParcelBookingView.DimensionId, DbType.Int32);
                    parameters.Add("@DimensionCost", createParcelBookingView.DimensionCost, DbType.Decimal);

                    parameters.Add("@WeightId", createParcelBookingView.WeightId, DbType.Int32);
                    parameters.Add("@WeightCost", createParcelBookingView.WeightCost, DbType.Decimal);

                    parameters.Add("@UniqItemDescription", createParcelBookingView.UniqItemDescription, DbType.String);
                    parameters.Add("@ItemValue", createParcelBookingView.ItemValue, DbType.Decimal);
                    parameters.Add("@IsExtraPackaging", createParcelBookingView.IsExtraPackaging, DbType.Boolean);
                    parameters.Add("@ExtraPackagingCost", createParcelBookingView.ExtraPackagingCost, DbType.Decimal);
                    parameters.Add("@ParcelAdditionalInfo", createParcelBookingView.ParcelAdditionalInfo, DbType.String);

                    parameters.Add("@IsPickup", createParcelBookingView.IsPickup, DbType.Boolean);
                    parameters.Add("@PickupDate", createParcelBookingView.PickupDate, DbType.DateTime);
                    parameters.Add("@FromTime", createParcelBookingView.FromTime, DbType.String);
                    parameters.Add("@ToTime", createParcelBookingView.ToTime, DbType.String);
                    parameters.Add("@PickUpCountryId", createParcelBookingView.PickUpCountryId, DbType.Int32);
                    parameters.Add("@PickUpCityId", createParcelBookingView.PickUpCityId, DbType.Int32);
                    parameters.Add("@PickUpLandMark", createParcelBookingView.PickUpLandMark, DbType.String);
                    parameters.Add("@PickUpAdditionalAddressInfo", createParcelBookingView.PickUpAdditionalAddressInfo, DbType.String);
                    parameters.Add("@ParcelPickUpInstructions", createParcelBookingView.ParcelPickUpInstractions, DbType.String);
                    parameters.Add("@PickUpCost", createParcelBookingView.PickupCost, DbType.Decimal);

                    parameters.Add("@CargoTypeId", createParcelBookingView.CargoTypeId, DbType.Decimal);
                    parameters.Add("@CargoCost", createParcelBookingView.CargoCost, DbType.Decimal);

                    parameters.Add("@SubTotal", createParcelBookingView.SubTotal, DbType.Decimal);
                    parameters.Add("@VAT_TaxPercentage", createParcelBookingView.VAT_TaxParcentage, DbType.Decimal);
                    parameters.Add("@VAT_TaxAmount", createParcelBookingView.VAT_TaxAmount, DbType.Decimal);
                    parameters.Add("@OrderPayableAmount", createParcelBookingView.OrderPayableAmount, DbType.Decimal);

                    parameters.Add("@ShippingServiceId", createParcelBookingView.ShippingServiceId, DbType.Int32);
                    parameters.Add("@ShippingServicePercentage", createParcelBookingView.ShippingServicePercentage, DbType.Decimal);
                    parameters.Add("@ShippingServiceAmount", createParcelBookingView.ShippingServiceAmount, DbType.Decimal);

                    parameters.Add("@DiscountPercentage", createParcelBookingView.DiscountPercentage, DbType.Decimal);
                    parameters.Add("@DiscountedOfferId", createParcelBookingView.DiscountedOfferId, DbType.Int32);
                    parameters.Add("@DiscountAmount", createParcelBookingView.DiscountAmount, DbType.Decimal);

                    parameters.Add("@CreatorId", createParcelBookingView.CreatorId, DbType.String);
                    parameters.Add("@ModifierId", createParcelBookingView.ModifierId, DbType.String);
                    parameters.Add("@IsActive", createParcelBookingView.IsActive, DbType.Boolean);
                    parameters.Add("@Barcode", barcode, DbType.String);

                    parameters.Add("@IsAgent", createParcelBookingView.IsAgent, DbType.Boolean);
                    parameters.Add("@AgentId", createParcelBookingView.AgentId, DbType.Int32);

                    parameters.Add("@BranchId", createParcelBookingView.BranchId, DbType.Int32);
                    parameters.Add("@RecordSerialNo", createParcelBookingView.RecordSerialNo, DbType.String);
                    parameters.Add("@PaymentTypeMethodId", createParcelBookingView.PaymentTypeMethodId, DbType.Int32);
                



                    if (!createParcelBookingView.IsAgent)
                    {
                         userId = createParcelBookingView.SenderName;
                         password = PasswordGenerator.GenerateRandomPassword();

                        var user = new
                        {
                            Id = 0,
                            RoleId = 3,
                            UserName = userId,
                            IsEmailConfirmed = false,
                            UserEmail = string.IsNullOrEmpty(createParcelBookingView.SenderEmail) ? "N/A" : createParcelBookingView.SenderEmail,
                            IsTemporaryPass = true,
                            PasswordHash = password,
                            PhoneNo = string.IsNullOrEmpty(createParcelBookingView.SenderMobileNo) ? "N/A" : createParcelBookingView.SenderMobileNo,
                            Address = string.IsNullOrEmpty(createParcelBookingView.SenderAdditionalAddressInfo) ? "N/A" : createParcelBookingView.SenderAdditionalAddressInfo,
                            IsActive = true,
                            CreatorId = createParcelBookingView.CreatorId,
                            CreationDate = DateTime.UtcNow,
                            ModifierId = createParcelBookingView.ModifierId,
                            ModificationDate = DateTime.UtcNow,
                            RoleName = ""
                        };

                        using (HttpClient httpClient = new HttpClient())
                        {
                            try
                            {
                                ////httpClient.BaseAddress = new Uri("https://localhost:7187");

                                httpClient.BaseAddress = new Uri("https://bookingrolesandpermissions.azurewebsites.net");

                                string jsonData = JsonSerializer.Serialize(user);
                                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = await httpClient.PostAsync("/api/UserLogin", content);

                                var httpResult = await response.Content.ReadAsStringAsync();
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }


                   var result = await dbConnection.QueryFirstOrDefaultAsync<CreateParcelBookingOutputView>(
                 "[SP_InsertIntoParcelBooking]", parameters, commandType: CommandType.StoredProcedure);
                    result.Barcode = barcode;
                    result.UserId = userId;
                    result.Password = password;
                    //result.RouteId = createParcelBookingView.RoutingTypeId;



                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IEnumerable<GetTransitionCostOutputView>> GetTransitionCost(GetTransitionCostView transitionCostView)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@RoutingTypeId", transitionCostView.RoutingTypeId, DbType.Int32);
                    parameters.Add("@ParcelLength", transitionCostView.ParcelLength, DbType.Decimal);
                    parameters.Add("@ParcelWidth", transitionCostView.ParcelWidth, DbType.Decimal);
                    parameters.Add("@ParcelHeight", transitionCostView.ParcelHeight, DbType.Decimal);
                    parameters.Add("@ParcelWeight", transitionCostView.ParcelWeight, DbType.Decimal);
                    parameters.Add("@CargoTypeId", transitionCostView.CargoTypeId, DbType.Int32);

                    parameters.Add("@IsExtraPackaging", transitionCostView.IsExtraPackaging, DbType.Boolean);
                    parameters.Add("@IsPickup", transitionCostView.IsPickUp, DbType.Boolean);
                    parameters.Add("@ItemTypeId", transitionCostView.ItemTypeId, DbType.Int32);
                    parameters.Add("@ShipmentArrivalDate", transitionCostView.ShipmentArrivalDate, DbType.DateTime);


                   


                    var result = await dbConnection.QueryAsync<GetTransitionCostOutputView>(
                        "[dbo].[SP_GetCalculateTotalPricing]", parameters, commandType: CommandType.StoredProcedure);

                   
                    return result.ToList();
                }
            }
            catch (Exception ex) {
                throw;
            }
            
        }
            
}
    }
