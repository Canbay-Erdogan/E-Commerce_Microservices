using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;
using System.Reflection.Metadata;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _dapperContext;

        public DiscountService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto coupon)
        {
            string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)";
            var paramaters = new DynamicParameters();
            paramaters.Add("@code", coupon.Code);
            paramaters.Add("@rate", coupon.Rate);
            paramaters.Add("@isActive", coupon.IsActive);
            paramaters.Add("@validDate", coupon.ValidDate);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async Task DeleteDiscountCouponAsync(int couponId)
        {
            string query = "Delete from Coupons where CouponId=@couponId";
            var paramaters = new DynamicParameters();
            paramaters.Add("@couponId", couponId);
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponsAsync()
        {
            string query = "select * from coupons";
            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int CouponId)
        {
            string query = "select * from coupons where CouponId = @couponId";
            var paramaters = new DynamicParameters();
            paramaters.Add("@couponId", CouponId);
            using (var connection = _dapperContext.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query,paramaters);
                return value;
            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto coupon)
        {
            string query = "update coupons set Code=@code,Rate=@rate,IsActive=@isActive,ValidDate=@validDate where CouponId=@couponId";
            var paramaters = new DynamicParameters();

            paramaters.Add("@couponId", coupon.CouponId);
            paramaters.Add("@code", coupon.Code);
            paramaters.Add("@rate", coupon.Rate);
            paramaters.Add("@isActive", coupon.IsActive);
            paramaters.Add("@validDate", coupon.ValidDate);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }
    }
}
