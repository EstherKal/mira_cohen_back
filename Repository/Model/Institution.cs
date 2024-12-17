using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Repository;

public class Institution
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("UserId")]
    public string UserId { get; set; }

    [BsonElement("InstitutionName")]
    [BsonIgnoreIfNull]
    public string InstitutionName { get; set; }

    [BsonElement("Symbol")]
    [BsonIgnoreIfNull]
    public string Symbol { get; set; }

    [BsonElement("ManagerName")]
    [BsonIgnoreIfNull]
    public string ManagerName { get; set; }

    [BsonElement("ContactPerson")]
    [BsonIgnoreIfNull]
    public string ContactPerson { get; set; }

    [BsonElement("ContactPhone")]
    [BsonIgnoreIfNull]
    public string ContactPhone { get; set; }

    [BsonElement("ContactEmail")]
    [BsonIgnoreIfNull]
    public string ContactEmail { get; set; }

    [BsonElement("InspectorName")]
    [BsonIgnoreIfNull]
    public string InspectorName { get; set; }

        [BsonElement("Students")]
        [BsonIgnoreIfNull]
        public List<Student>? Students { get; set; }
        public Institution(string inspectorName)
        {
            this.InspectorName = inspectorName;
            Students = new List<Student>();
        }
}
