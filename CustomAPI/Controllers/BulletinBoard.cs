using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CustomAPI.Controllers
{

    [Serializable, BsonIgnoreExtraElements]
    public class BulletinBoard
    {
        [BsonId, BsonElement("BoardId"), BsonRepresentation(BsonType.ObjectId)]
        public string BulletinBoardId { get; set; } = string.Empty;

        [BsonElement("user_name"), BsonRepresentation(BsonType.String)]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; } = string.Empty;


        [BsonElement("title"), BsonRepresentation(BsonType.String)]
        public string Title { get; set; } = string.Empty;


        [BsonElement("userId"), BsonRepresentation(BsonType.String)]
        public string UserId { get; set; } = string.Empty;



        [BsonElement("created"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }

        [BsonElement("category"), BsonRepresentation(BsonType.String)]
        public string Category { get; set; } = string.Empty;

        [BsonElement("experience"), BsonRepresentation(BsonType.String)]
        public string Experience { get; set; } = string.Empty;
    }

}
