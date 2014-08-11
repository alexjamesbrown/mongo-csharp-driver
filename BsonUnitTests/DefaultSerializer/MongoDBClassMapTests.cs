using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NUnit.Framework;

namespace MongoDB.BsonUnitTests.DefaultSerializer
{
    [TestFixture]
    public class MongoDBClassMapTests
    {
        private static bool __testAlreadyRan;

        public class MyClass
        {
            public ObjectId Id;
            public int X;
        }

        public class MyClassClassMap : MongoDBClassMap<MyClass>
        {
            public override void Map(BsonClassMap<MyClass> cm)
            {
                cm.AutoMap();
            }
        }

        [Test]
        public void BsonClassMapIsClassMapRegisteredWhenMongoDBClassMapIsCreated()
        {
            // test can only be run once
            if (__testAlreadyRan)
            {
                return;
            }
            else
            {
                __testAlreadyRan = true;
            }

            Assert.IsFalse(BsonClassMap.IsClassMapRegistered(typeof(MyClass)));

            //create an instance of the ClassMap
            new MyClassClassMap();

            Assert.IsTrue(BsonClassMap.IsClassMapRegistered(typeof(MyClass)));
        }
    }
}