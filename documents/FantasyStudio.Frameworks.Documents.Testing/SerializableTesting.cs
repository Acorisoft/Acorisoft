using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acorisoft.FantasyStudio.Documents;
using Acorisoft.FantasyStudio.Documents.Characters;
using LiteDB;

namespace Acorisoft.FantasyStudio.Testing
{
    [TestClass]
    public class SerializableTesting
    {
        [TestMethod]
        public void AdvancedGenderDefinition_Serializable()
        {
            var array = new CharacterAdvancedGenderDefinition[]
            {
                new CharacterAdvancedGenderDefinition
                {
                    SexualCognitive = SexualCognitive.Male,
                    SexualOrientation = SexualOrientation.Female
                },
                new CharacterAdvancedGenderDefinition
                {
                    SexualCognitive = SexualCognitive.Male,
                    SexualOrientation = SexualOrientation.Female
                },
                new CharacterAdvancedGenderDefinition
                {
                    SexualCognitive = SexualCognitive.Male,
                    SexualOrientation = SexualOrientation.Female
                },

            };
            var bson = BsonMapper.Global.ToDocument(array);
            var oppsite = BsonMapper.Global.ToObject<CharacterAdvancedGenderDefinition[]>(bson);
            Assert.IsTrue(oppsite is not null);
        }
    }
}
