using Amazon.DynamoDBv2.DataModel;

namespace OM.AWS.Demo.DTL
{
    [DynamoDBTable("om-lam-order")] public class OrderDTO
    {
        public enum StatusEnum { CREATED, PICKED_UP, PAID, SENT }
        [DynamoDBHashKey] public string? Email { get; set; }
        [DynamoDBRangeKey] public string? OrderGUID { get; set; }
        public StatusEnum Status { get; set; }
    }
}