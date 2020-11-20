using CouponRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CouponRestAPI.Data
{
    public class MockCouponRepo : ICouponRepo
    {
        public Coupon CreateCommand(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public void DeleteCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coupon> GetAllCoupons()
        {
            /*  var coupons = new List<Coupon>
              {  
                  new Coupon{Id=0, CouponCode="Merry20", StoreName="Walmart"},
                  new Coupon{Id=1, CouponCode="Merry20", StoreName="Walmart"},
                  new Coupon{Id=2, CouponCode="Merry20", StoreName="Walmart"},
              };

              return coupons;*/
            throw new NotImplementedException();
        }


        public Coupon GetCouponById(string id)
        {
            /*  return new Coupon
              {
                  Id = 0,
                  CouponCode = "Merry20",
                  StoreName = "Walmart"
              };*/
            throw new NotImplementedException();
        }

        public IEnumerable<Coupon> GetCouponByStore(string storename)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }


        public void UpdateCoupon(string id, Coupon coupon)
        {
            throw new NotImplementedException();
        }

        Coupon ICouponRepo.CreateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        void ICouponRepo.DeleteCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Coupon> ICouponRepo.GetAllCoupons()
        {
            throw new NotImplementedException();
        }

        Coupon ICouponRepo.GetCouponById(string id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Coupon> ICouponRepo.GetCouponByStore(string storename)
        {
            throw new NotImplementedException();
        }

        void ICouponRepo.UpdateCoupon(string id, Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
