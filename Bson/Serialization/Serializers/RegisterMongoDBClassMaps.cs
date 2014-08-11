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

using System;
using System.Linq;
using System.Reflection;

namespace MongoDB.Bson.Serialization.Serializers
{
    /// <summary>
    /// Register MongoDBClassMaps
    /// </summary>
    public class RegisterMongoDBClassMaps
    {
        /// <summary>
        /// Will create instances of classes that inherit MongoDBClassMap in the specified assemblies
        /// </summary>
        /// <param name="assemblies"></param>
        public static void ForAssemblies(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var classMaps = assembly
                    .GetTypes()
                    .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                t.BaseType.GetGenericTypeDefinition() == typeof(MongoDBClassMap<>));

                foreach (var classMap in classMaps)
                    Activator.CreateInstance(classMap);
            }
        }
    }
}