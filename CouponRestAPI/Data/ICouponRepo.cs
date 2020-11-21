using CouponRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CouponRestAPI.Data
{
    public interface ICouponRepo
    {
        
        IEnumerable<Coupon> GetAllCoupons();
        IEnumerable<Coupon> GetCouponByStore(string storename);
        Coupon GetCouponById(string id);
        Coupon CreateCoupon(Coupon coupon);
        void UpdateCoupon(string id, Coupon coupon);
        void DeleteCoupon(Coupon coupon);

    }
}
