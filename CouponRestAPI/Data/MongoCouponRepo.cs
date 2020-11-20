using CouponRestAPI.Models;
using CouponRestAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CouponRestAPI.Data
{
    public class MongoCouponRepo : ICouponRepo
    {
        private readonly CouponService _service;

        //constructor
        public MongoCouponRepo(CouponService service)
        {
            _service = service;
        }

        public Coupon CreateCoupon(Coupon coupon)
        {
            if (coupon == null)
            {
                throw new ArgumentNullException(nameof(coupon));
            }

            return _service.Create(coupon);
        }

        public void DeleteCoupon(Coupon coupon)
        {
            if (coupon == null)
            {
                throw new ArgumentNullException(nameof(coupon));
            }

            _service.Remove(coupon);
        }

        public IEnumerable<Coupon> GetAllCoupons()
        {
            return _service.Get();
        }

        public Coupon GetCouponById(string id)
        {
            return _service.Get(id);
        }

        public IEnumerable<Coupon> GetCouponByStore(string storename)
        {
            throw new NotImplementedException();
        }

        public void UpdateCoupon(string id, Coupon coupon)
        {
            if (coupon == null)
            {
                throw new ArgumentNullException(nameof(coupon));
            }

            _service.Update(id, coupon);
        }
    }
}
