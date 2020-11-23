﻿using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using CouponRestAPI.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CouponRestAPI.Data
{


    public class CouponService : IDynamoDBServices
    {
        //when use DynamoDb
        private AmazonDynamoDBClient dynamoDBClient { get; set; }
        private string tableName = "Coupons";
        private readonly ILogger<CouponService> _logger;
        //constructor
        public CouponService(
            AmazonDynamoDBClient client, 
            ILogger<CouponService> logger)
        {
            this.dynamoDBClient = client;
            this._logger = logger;
        }

        private async void CreateTable()
        {
            AmazonDynamoDBClient client = dynamoDBClient;
            var tableResponse = await client.ListTablesAsync();
            if (!tableResponse.TableNames.Contains(tableName))
            {
                _logger.LogInformation("Table not found, creating table => " + tableName);
                await client.CreateTableAsync(new CreateTableRequest
                {
                    TableName = tableName,
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 3,
                        WriteCapacityUnits = 1
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "id",
                            KeyType = KeyType.HASH
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition { AttributeName = "id", AttributeType=ScalarAttributeType.S }
                    }
                });

                bool isTableAvailable = false;
                while (!isTableAvailable)
                {
                    _logger.LogInformation("Waiting for table to be active...");
                    Thread.Sleep(5000);
                    var tableStatus = await client.DescribeTableAsync(tableName);
                    isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
                }
                _logger.LogInformation("DynamoDB Table Created Successfully!");
            }
        }

        public async Task<Coupon> Create(Coupon coupon)
        {
            CreateTable();
            DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
            // Add a unique id for the primary key.
            coupon.Id = System.Guid.NewGuid().ToString();
           
            await context.SaveAsync(coupon, default(System.Threading.CancellationToken));
            return await context.LoadAsync<Coupon>(coupon.Id, default(System.Threading.CancellationToken));
        }

        public async Task<Coupon> Update(string id, Coupon coupon)
        {
            CreateTable();
            Coupon found = await Get(coupon.Id);
            found.CouponCode = coupon.CouponCode;
            found.StoreName = coupon.StoreName;
            found.OriginalPrice = coupon.OriginalPrice;
            found.DiscountPercentage = coupon.DiscountPercentage;
            found.ItemName = coupon.ItemName;
            //found.Updated = DateTime.UtcNow;
            DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
            await context.SaveAsync(found, default(System.Threading.CancellationToken));
            return await context.LoadAsync<Coupon>(found.Id, default(System.Threading.CancellationToken));
        }

        public async Task Delete(Coupon coupon)
        {
            CreateTable();
            DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
            await context.DeleteAsync<Coupon>(coupon.Id, default(System.Threading.CancellationToken));

        }

        public async Task<Coupon> Get(string Id)
        {
            CreateTable();
            DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
            Coupon foundObject = await context.LoadAsync<Coupon>(Id, default(System.Threading.CancellationToken));
            return foundObject;
        }

        public async Task<List<Coupon>> Get()
        {
            CreateTable();
            ScanFilter scanFilter = new ScanFilter();
            //scanFilter.AddCondition("StoreName", ScanOperator.Equal, storeName);

            ScanOperationConfig soc = new ScanOperationConfig()
            {
                Filter = scanFilter
            };
            DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
            AsyncSearch<Coupon> search = context.FromScanAsync<Coupon>(soc, null);
            List<Coupon> documentList = new List<Coupon>();
            do
            {
                documentList = await search.GetNextSetAsync(default(System.Threading.CancellationToken));
            } while (!search.IsDone);

            return documentList;
        }

        public async Task<List<Coupon>> GetCouponByStore(String storeName)
        {
            CreateTable();
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("StoreName", ScanOperator.Equal, storeName);

            ScanOperationConfig soc = new ScanOperationConfig()
            {
                Filter = scanFilter
            };
            DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
            AsyncSearch<Coupon> search = context.FromScanAsync<Coupon>(soc, null);
            List<Coupon> documentList = new List<Coupon>();
            do
            {
                documentList = await search.GetNextSetAsync(default(System.Threading.CancellationToken));
            } while (!search.IsDone);

            return documentList;
        }


        //when use mongodb
        /*private readonly IMongoCollection<Coupon> _coupons;  
        //constructor
        public CouponService(ICouponApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);           
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
        }*/
    }
}
