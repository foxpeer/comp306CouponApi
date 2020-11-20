using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponRestAPI.Models
{
    public class Coupon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string CouponCode { get; set; }
        [BsonRequired]
        public string StoreName { get; set; }
        [BsonRequired]
        public string ItemName { get; set; }
        [BsonRequired]
        public decimal OriginalPrice { get; set; }
        [BsonRequired]
        public decimal DiscountPercentage { get; set; }
     /*   public DateTime ExpiredTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }*/

        //constructor
       /* public Coupon(int id, string couponCode, string storeName, string itemName, decimal originalPrice, decimal discountPercentage, DateTime expiredTime, DateTime createdTime, DateTime updatedTime)
        {
            Id = id;
            CouponCode = couponCode;
            StoreName = storeName;
            ItemName = itemName;
            OriginalPrice = originalPrice;
            DiscountPercentage = discountPercentage;
            ExpiredTime = expiredTime;
            CreatedTime = createdTime;
            UpdatedTime = updatedTime;
        }*/

       /* public Coupon(int id, string couponCode, string storeName) {
            Id = id;
            CouponCode = couponCode;
            StoreName = storeName;
        }
*/
    }
}
