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

        public async Task<List<CreateParcelBookingOutputView>> CreateParcelBookingAsync(List<CreateParcelBookingView> createParcelBookingViews)
        {
            var results = new List<CreateParcelBookingOutputView>();

            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            foreach (var booking in createParcelBookingViews)
            {

                string barcode = Guid.NewGuid().ToString("N").Substring(0, 8) + Guid.NewGuid().ToString("N").Substring(24, 8);

                string userId = null;
                string password = null;


                //if (!booking.IsAgent)
                //{
                //    userId = booking.SenderName;
                //    password = PasswordGenerator.GenerateRandomPassword();

                //    await CreateClientUserInSecurityAPI(booking, userId, password);
                //}

                var headerParams = new DynamicParameters();

                headerParams.Add("@CompanyId", booking.CompanyId, DbType.Int32);


                headerParams.Add("@CompanyId", booking.CompanyId, DbType.Int32);

                headerParams.Add("@SenderName", booking.SenderName, DbType.String);
                headerParams.Add("@SenderEmail", booking.SenderEmail, DbType.String);
                headerParams.Add("@SenderMobileNo", booking.SenderMobileNo, DbType.String);
                headerParams.Add("@SenderCountryId", booking.SenderCountryId, DbType.Int32);
                headerParams.Add("@SenderCityId", booking.SenderCityId, DbType.Int32);
                headerParams.Add("@SenderLandmark", booking.SenderLandMark, DbType.String);
                headerParams.Add("@SenderAdditionalAddressInfo", booking.SenderAdditionalAddressInfo, DbType.String);
                headerParams.Add("@SenderPostCode", booking.SenderPostCode, DbType.String);

                headerParams.Add("@ReceiverName", booking.ReceiverName, DbType.String);
                headerParams.Add("@ReceiverEmail", booking.ReceiverEmail, DbType.String);
                headerParams.Add("@ReceiverMobileNo", booking.ReceiverMobileNo, DbType.String);
                headerParams.Add("@ReceiverCountryId", booking.ReceiverCountryId, DbType.Int32);
                headerParams.Add("@ReceiverCityId", booking.ReceiverCityId, DbType.Int32);
                headerParams.Add("@ReceiverLandmark", booking.ReceiverLandMark, DbType.String);
                headerParams.Add("@ReceiverAdditionalAddressInfo", booking.ReceiverAdditionalAddressInfo, DbType.String);
                headerParams.Add("@ReceiverPostCode", booking.ReceiverPostCode, DbType.String);

                headerParams.Add("@IsPickup", booking.IsPickup, DbType.Boolean);
                headerParams.Add("@FromTime", booking.FromTime, DbType.String);
                headerParams.Add("@ToTime", booking.ToTime, DbType.String);
                headerParams.Add("@PickUpCountryId", booking.PickUpCountryId, DbType.Int32);
                headerParams.Add("@PickUpCityId", booking.PickUpCityId, DbType.Int32);
                headerParams.Add("@PickUpAdditionalAddressInfo", booking.PickUpAdditionalAddressInfo, DbType.String);

                headerParams.Add("@SubTotal", booking.SubTotal, DbType.Decimal);

                headerParams.Add("@VAT_TaxAmount", booking.VAT_TaxAmount, DbType.Decimal);
                headerParams.Add("@OrderPayableAmount", booking.OrderPayableAmount, DbType.Decimal);



                headerParams.Add("@DiscountPercentage", booking.DiscountPercentage, DbType.Decimal);
                headerParams.Add("@DiscountedOfferId", booking.DiscountedOfferId, DbType.Int32);
                headerParams.Add("@DiscountAmount", booking.DiscountAmount, DbType.Decimal);

                headerParams.Add("@CreatorId", booking.CreatorId, DbType.String);
                headerParams.Add("@ModifierId", booking.ModifierId, DbType.String);
                headerParams.Add("@IsActive", booking.IsActive, DbType.Boolean);
                headerParams.Add("@Barcode", barcode, DbType.String);

                headerParams.Add("@IsAgent", booking.IsAgent, DbType.Boolean);
                headerParams.Add("@AgentId", booking.AgentId, DbType.String);

                headerParams.Add("@BranchId", booking.BranchId, DbType.Int32);
                headerParams.Add("@BookedById", booking.BookedById, DbType.String);
                headerParams.Add("@PaymentTypeMethodId", booking.PaymentTypeMethodId, DbType.Int32);
                headerParams.Add("@CurrencyTypeId", booking.CurrencyTypeId, DbType.Int32);


                var headerResult = await dbConnection.QueryFirstOrDefaultAsync<CreateParcelBookingOutputView>(
                    "SP_CreateParcelBooking",
                    headerParams,
                    commandType: CommandType.StoredProcedure);

                if (headerResult == null || headerResult.ParcelId <= 0)
                    throw new Exception("Parcel Header Create Failed.");

                int parcelId = headerResult.ParcelId;


                foreach (var product in booking.Products)
                {
                    DynamicParameters detailParams = new();

                    detailParams.Add("@CompanyId", booking.CompanyId, DbType.Int32);
                    detailParams.Add("@ParcelId", parcelId, DbType.Int32);

                    detailParams.Add("@RouteId", booking.RoutingTypeId, DbType.Int32);
                    detailParams.Add("@RouteCost", booking.RouteCost, DbType.Decimal);

                    detailParams.Add("@ShippingServiceId", booking.ShippingServiceId, DbType.Int32);
                    detailParams.Add("@ShippingServicePercentage", booking.ShippingServicePercentage, DbType.Decimal);
                    detailParams.Add("@ShippingServiceAmount", booking.ShippingServiceAmount, DbType.Decimal);

                    detailParams.Add("@DiscountedOfferId", booking.DiscountedOfferId, DbType.Int32);
                    detailParams.Add("@DiscountPercentage", booking.DiscountPercentage, DbType.Decimal);
                    detailParams.Add("@DiscountAmount", booking.DiscountAmount, DbType.Decimal);

                    detailParams.Add("@IsPickup", booking.IsPickup, DbType.Boolean);
                    detailParams.Add("@PickUpCost", booking.PickUpCost, DbType.Decimal);

                    detailParams.Add("@ItemCategoryId", product.ItemCategoryId, DbType.Int32);
                    detailParams.Add("@ItemCategoryCost", product.ItemCategoryCost, DbType.Decimal);

                    detailParams.Add("@ItemTypeId", product.ItemTypeId, DbType.Int32);
                    detailParams.Add("@ItemTypeCost", product.ItemTypeCost, DbType.Decimal);

                    detailParams.Add("@DimensionId", product.DimensionId, DbType.Int32);
                    detailParams.Add("@DimensionCost", product.DimensionCost, DbType.Decimal);

                    detailParams.Add("@WeightId", product.WeightId, DbType.Int32);
                    detailParams.Add("@WeightCost", product.WeightCost, DbType.Decimal);

                    detailParams.Add("@CargoTypeId", product.CargoTypeId, DbType.Int32);
                    detailParams.Add("@CargoCost", product.CargoCost, DbType.Decimal);

                    detailParams.Add("@UniqItemDescription", product.UniqItemDescription, DbType.String);
                    detailParams.Add("@ItemValue", product.ItemValue, DbType.Decimal);
                    detailParams.Add("@ItemQty", product.ProductQty, DbType.Int32);

                    detailParams.Add("@IsExtraPackaging", product.IsExtraPackaging, DbType.Boolean);
                    detailParams.Add("@ExtraPackagingCost", product.ExtraPackagingCost, DbType.Decimal);

                    detailParams.Add("@ParcelAdditionalInfo", product.ParcelAdditionalInfo, DbType.String);

                    detailParams.Add("@BranchId", booking.BranchId, DbType.Int32);
                    detailParams.Add("@PaymentTypeMethodId", booking.PaymentTypeMethodId, DbType.Int32);


                    await dbConnection.ExecuteAsync(
                        "SP_InsertParcelProductDetails",
                        detailParams,
                        commandType: CommandType.StoredProcedure);
                }

                // Final output
                //headerResult.Barcode = barcode;
                //headerResult.UserId = userId;
                //headerResult.Password = password;
                headerResult.Message = "Parcel booking successful";

                results.Add(headerResult);
            }

            return results;
        }

        // Create client user in your security API
        private async Task CreateClientUserInSecurityAPI(CreateParcelBookingView booking, string userId, string password)
        {
            var user = new
            {
                Id = 0,
                RoleId = 3,
                UserName = userId,
                IsEmailConfirmed = false,
                UserEmail = string.IsNullOrEmpty(booking.SenderEmail) ? "N/A" : booking.SenderEmail,
                IsTemporaryPass = true,
                PasswordHash = password,
                PhoneNo = string.IsNullOrEmpty(booking.SenderMobileNo) ? "N/A" : booking.SenderMobileNo,
                Address = string.IsNullOrEmpty(booking.SenderAdditionalAddressInfo) ? "N/A" : booking.SenderAdditionalAddressInfo,
                IsActive = true,
                CreatorId = string.IsNullOrEmpty(booking.CreatorId) ? "N/A" : booking.CreatorId,
                CreationDate = DateTime.UtcNow,
                ModifierId = string.IsNullOrEmpty(booking.ModifierId) ? "N/A" : booking.ModifierId,
                ModificationDate = DateTime.UtcNow,
                RoleName = ""
            };

            using HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.apibk.ps-env.com");

            string jsonData = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("/api/UserLogin", content);
            string result = await response.Content.ReadAsStringAsync();

            // Optional: validate response
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Security API User Create Failed: {result}");
        }





        //public async Task<List<CreateParcelBookingOutputView>> CreateParcelBookingAsync(List<CreateParcelBookingView> createParcelBookingViews)
        //{
        //    var results = new List<CreateParcelBookingOutputView>();

        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            foreach (var createParcelBookingView in createParcelBookingViews)
        //            {
        //                string barcode = Guid.NewGuid().ToString("N").Substring(0, 8) + Guid.NewGuid().ToString("N").Substring(24, 8);

        //                string userId = null;
        //                string password = null;

        //                DynamicParameters parameters = new();
        //                parameters.Add("@CompanyId", createParcelBookingView.CompanyId, DbType.Int32);
        //                parameters.Add("@RoutingTypeId", createParcelBookingView.RoutingTypeId, DbType.Int32);
        //                parameters.Add("@RouteCost", createParcelBookingView.RouteCost, DbType.Decimal);

        //                parameters.Add("@SenderName", createParcelBookingView.SenderName, DbType.String);
        //                parameters.Add("@SenderEmail", createParcelBookingView.SenderEmail, DbType.String);
        //                parameters.Add("@SenderMobileNo", createParcelBookingView.SenderMobileNo, DbType.String);
        //                parameters.Add("@SenderCountryId", createParcelBookingView.SenderCountryId, DbType.Int32);
        //                parameters.Add("@SenderCityId", createParcelBookingView.SenderCityId, DbType.Int32);
        //                parameters.Add("@SenderLandmark", createParcelBookingView.SenderLandMark, DbType.String);
        //                parameters.Add("@SenderPostCode", createParcelBookingView.SenderPostCode, DbType.String);  //Newly Added - Yeameen
        //                parameters.Add("@SenderAdditionalAddressInfo", createParcelBookingView.SenderAdditionalAddressInfo, DbType.String);

        //                parameters.Add("@ReceiverName", createParcelBookingView.ReceiverName, DbType.String);
        //                parameters.Add("@ReceiverEmail", createParcelBookingView.ReceiverEmail, DbType.String);
        //                parameters.Add("@ReceiverMobileNo", createParcelBookingView.ReceiverMobileNo, DbType.String);
        //                parameters.Add("@ReceiverCountryId", createParcelBookingView.ReceiverCountryId, DbType.Int32);
        //                parameters.Add("@ReceiverCityId", createParcelBookingView.ReceiverCityId, DbType.Int32);
        //                parameters.Add("@ReceiverLandmark", createParcelBookingView.ReceiverLandMark, DbType.String);
        //                parameters.Add("@ReceiverPostCode", createParcelBookingView.ReceiverPostCode, DbType.String);  //Newly Added - Yeameen
        //                parameters.Add("@ReceiverAdditionalAddressInfo", createParcelBookingView.ReceiverAdditionalAddressInfo, DbType.String);

        //                parameters.Add("@ItemCategoryId", createParcelBookingView.ItemCategoryId, DbType.Int32);
        //                parameters.Add("@ItemCategoryCost", createParcelBookingView.ItemCategoryCost, DbType.Decimal);
        //                parameters.Add("@ItemTypeId", createParcelBookingView.ItemTypeId, DbType.Int32);
        //                parameters.Add("@ItemTypeCost", createParcelBookingView.ItemTypeCost, DbType.Decimal);

        //                parameters.Add("@DimensionId", createParcelBookingView.DimensionId, DbType.Int32);
        //                parameters.Add("@DimensionCost", createParcelBookingView.DimensionCost, DbType.Decimal);

        //                parameters.Add("@WeightId", createParcelBookingView.WeightId, DbType.Int32);
        //                parameters.Add("@WeightCost", createParcelBookingView.WeightCost, DbType.Decimal);

        //                parameters.Add("@UniqItemDescription", createParcelBookingView.UniqItemDescription, DbType.String);
        //                parameters.Add("@ItemValue", createParcelBookingView.ItemValue, DbType.Decimal);
        //                parameters.Add("@IsExtraPackaging", createParcelBookingView.IsExtraPackaging, DbType.Boolean);
        //                parameters.Add("@ExtraPackagingCost", createParcelBookingView.ExtraPackagingCost, DbType.Decimal);
        //                parameters.Add("@ParcelAdditionalInfo", createParcelBookingView.ParcelAdditionalInfo, DbType.String);

        //                parameters.Add("@IsPickup", createParcelBookingView.IsPickup, DbType.Boolean);
        //                parameters.Add("@PickupDate", createParcelBookingView.PickupDate, DbType.DateTime);
        //                parameters.Add("@FromTime", createParcelBookingView.FromTime, DbType.String);
        //                parameters.Add("@ToTime", createParcelBookingView.ToTime, DbType.String);
        //                parameters.Add("@PickUpCountryId", createParcelBookingView.PickUpCountryId, DbType.Int32);
        //                parameters.Add("@PickUpCityId", createParcelBookingView.PickUpCityId, DbType.Int32);
        //                parameters.Add("@PickUpLandMark", createParcelBookingView.PickUpLandMark, DbType.String);
        //                parameters.Add("@PickUpAdditionalAddressInfo", createParcelBookingView.PickUpAdditionalAddressInfo, DbType.String);
        //                parameters.Add("@ParcelPickUpInstructions", createParcelBookingView.ParcelPickUpInstractions, DbType.String);
        //                parameters.Add("@PickUpCost", createParcelBookingView.PickupCost, DbType.Decimal);

        //                parameters.Add("@CargoTypeId", createParcelBookingView.CargoTypeId, DbType.Decimal);
        //                parameters.Add("@CargoCost", createParcelBookingView.CargoCost, DbType.Decimal);

        //                parameters.Add("@SubTotal", createParcelBookingView.SubTotal, DbType.Decimal);
        //                parameters.Add("@VAT_TaxPercentage", createParcelBookingView.VAT_TaxParcentage, DbType.Decimal);
        //                parameters.Add("@VAT_TaxAmount", createParcelBookingView.VAT_TaxAmount, DbType.Decimal);
        //                parameters.Add("@OrderPayableAmount", createParcelBookingView.OrderPayableAmount, DbType.Decimal);

        //                parameters.Add("@ShippingServiceId", createParcelBookingView.ShippingServiceId, DbType.Int32);
        //                parameters.Add("@ShippingServicePercentage", createParcelBookingView.ShippingServicePercentage, DbType.Decimal);
        //                parameters.Add("@ShippingServiceAmount", createParcelBookingView.ShippingServiceAmount, DbType.Decimal);

        //                parameters.Add("@DiscountPercentage", createParcelBookingView.DiscountPercentage, DbType.Decimal);
        //                parameters.Add("@DiscountedOfferId", createParcelBookingView.DiscountedOfferId, DbType.Int32);
        //                parameters.Add("@DiscountAmount", createParcelBookingView.DiscountAmount, DbType.Decimal);

        //                parameters.Add("@CreatorId", createParcelBookingView.CreatorId, DbType.String);
        //                parameters.Add("@ModifierId", createParcelBookingView.ModifierId, DbType.String);
        //                parameters.Add("@IsActive", createParcelBookingView.IsActive, DbType.Boolean);
        //                parameters.Add("@Barcode", barcode, DbType.String);

        //                parameters.Add("@IsAgent", createParcelBookingView.IsAgent, DbType.Boolean);
        //                parameters.Add("@AgentId", createParcelBookingView.AgentId, DbType.String);

        //                parameters.Add("@BranchId", createParcelBookingView.BranchId, DbType.Int32);
        //                parameters.Add("@RecordSerialNo", createParcelBookingView.RecordSerialNo, DbType.String);
        //                parameters.Add("@PaymentTypeMethodId", createParcelBookingView.PaymentTypeMethodId, DbType.Int32);
        //                parameters.Add("@BookedById", createParcelBookingView.BookedById, DbType.String);

        //                parameters.Add("@ProductQty", createParcelBookingView.ProductQty, DbType.Int32);
        //                parameters.Add("@CurrencyTypeId", createParcelBookingView.CurrencyTypeId, DbType.Int32);
                       
        //                if (!createParcelBookingView.IsAgent)
        //                {
        //                    userId = createParcelBookingView.SenderName;
        //                    password = PasswordGenerator.GenerateRandomPassword();

        //                    var user = new
        //                    {
        //                        Id = 0,
        //                        RoleId = 3,
        //                        UserName = userId,
        //                        IsEmailConfirmed = false,
        //                        UserEmail = string.IsNullOrEmpty(createParcelBookingView.SenderEmail) ? "N/A" : createParcelBookingView.SenderEmail,
        //                        IsTemporaryPass = true,
        //                        PasswordHash = password,
        //                        PhoneNo = string.IsNullOrEmpty(createParcelBookingView.SenderMobileNo) ? "N/A" : createParcelBookingView.SenderMobileNo,
        //                        Address = string.IsNullOrEmpty(createParcelBookingView.SenderAdditionalAddressInfo) ? "N/A" : createParcelBookingView.SenderAdditionalAddressInfo,
        //                        IsActive = true,
        //                        CreatorId = string.IsNullOrEmpty(createParcelBookingView.CreatorId) ? "N/A" : createParcelBookingView.CreatorId,
        //                        CreationDate = DateTime.UtcNow,
        //                        ModifierId = string.IsNullOrEmpty(createParcelBookingView.ModifierId) ? "N/A" : createParcelBookingView.ModifierId,
        //                        ModificationDate = DateTime.UtcNow,
        //                        RoleName = ""
        //                    };

        //                    using (HttpClient httpClient = new HttpClient())
        //                    {
        //                        try
        //                        {
        //                            httpClient.BaseAddress = new Uri("https://www.apibk.ps-env.com");
        //                            string jsonData = JsonSerializer.Serialize(user);
        //                            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //                            HttpResponseMessage response = await httpClient.PostAsync("/api/UserLogin", content);
        //                            var httpResult = await response.Content.ReadAsStringAsync();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            throw;
        //                        }
        //                    }
        //                }

        //                var result = await dbConnection.QueryFirstOrDefaultAsync<CreateParcelBookingOutputView>(
        //                    "[SP_InsertIntoParcelBooking]", parameters, commandType: CommandType.StoredProcedure);

        //                result.Barcode = barcode;
        //                result.UserId = userId;
        //                result.Password = password;
                        
        //                results.Add(result);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return results;
        //}

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IEnumerable<GetTransitionCostOutputView>> GetTransitionCost( List<GetTransitionCostView> transitionCostViews)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var results = new List<GetTransitionCostOutputView>();

                    foreach(var transitionCostView in transitionCostViews) { 
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
                    parameters.Add("@ItemCategoryId", transitionCostView.ItemCategoryId, DbType.Int32);
                    parameters.Add("@ShipmentArrivalDate", transitionCostView.ShipmentArrivalDate, DbType.DateTime);
                    parameters.Add("@ProductQty", transitionCostView.ProductQty, DbType.Int32);

                    var result = await dbConnection.QueryAsync<GetTransitionCostOutputView>(
                        "[dbo].[SP_GetCalculateTotalPricing]", parameters, commandType: CommandType.StoredProcedure);

                        results.AddRange(result);
                    }
                    return results;
                }
            }
            catch (Exception ex) {
                throw;
            }
            
        }
            
}
    }
