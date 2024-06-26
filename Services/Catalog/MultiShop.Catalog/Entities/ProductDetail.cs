﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDetailID { get; set; }
        public string ProductDetailDescription { get; set; }
        public string ProductDetailInfo { get; set; }
        [BsonIgnore]
        public Product Product { get; set; }
        public string ProductID { get; set; }

    }
}
