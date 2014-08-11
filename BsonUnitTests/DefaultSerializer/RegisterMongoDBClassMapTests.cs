using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NUnit.Framework;

namespace MongoDB.BsonUnitTests.DefaultSerializer
{
    [TestFixture]
    public class RegisterMongoDBClassMapTests
    {
        private static bool __testAlreadyRan;

        public class MyClass2
        {
            public ObjectId Id;
            public int X;
        }

        public class MyClass2ClassMap : MongoDBClassMap<MyClass2>
        {
            public override void Map(BsonClassMap<MyClass2> cm)
            {
                cm.AutoMap();
            }
        }

        [Test]
        public void RegistersClassMapsInAssembly()
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

            Assert.IsFalse(BsonClassMap.IsClassMapRegistered(typeof(MyClass2)));

            //for this assembly
            RegisterMongoDBClassMaps.ForAssemblies(GetType().Assembly);

            Assert.IsTrue(BsonClassMap.IsClassMapRegistered(typeof(MyClass2)));
        }
    }
}