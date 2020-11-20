using CouponRestAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CouponRestAPI.Data
{

    public class CouponService
    {
        private readonly IMongoCollection<Coupon> _coupons;

        //constructor
        public CouponService(ICouponApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _coupons = database.GetCollection<Coupon>(settings.CouponsCollectionName);
        }

        public List<Coupon> Get() =>
            _coupons.Find(coupon => true).ToList();

        public Coupon Get(string id)
        {
            return _coupons.Find<Coupon>(coupon => coupon.Id == id).FirstOrDefault();
        }

        public Coupon Create(Coupon coupon)
        {
            _coupons.InsertOne(coupon);

            return coupon;
        }

        public void Update(string id, Coupon couponIn)
        {
            _coupons.ReplaceOne(coupon => coupon.Id == id, couponIn);
        }

        public void Remove(Coupon couponIn)
        {
            _coupons.DeleteOne(coupon => coupon.Id == couponIn.Id);
        }

        public void Remove(string id)
        {
            _coupons.DeleteOne(coupon => coupon.Id == id);
        }
    }
}
