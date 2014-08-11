/* Copyright 2010-2012 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

namespace MongoDB.Bson.Serialization.Serializers
{
    /// <summary>
    /// Base class for class map that can be registered as separate classes.
    /// </summary>
    public abstract class MongoDBClassMap<T>
    {
        protected MongoDBClassMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
                BsonClassMap.RegisterClassMap<T>(Map);
        }

        /// <summary>
        /// Map method that is overridden in implementations
        /// Takes an instance of BsonClassMap of same type as the MongoDBClassMap
        /// </summary>
        /// <param name="bsonClassMap"></param>
        public abstract void Map(BsonClassMap<T> bsonClassMap);
    }
}